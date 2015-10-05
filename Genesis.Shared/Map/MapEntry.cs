using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace Genesis.Shared.Map
{
    using Constant;
    using Entities;
    using Entities.Base;
    using Manager;
    using Structures;
    using Structures.Asset;
    using Utils.Extensions;

    public class MapEntry
    {
        #region Declarations
        public uint HighestCoid;
        public long CreatorLoadTrigger;
        public float CullingStyle;
        public float EntryW;
        public float EntryX;
        public float EntryY;
        public float EntryZ;
        public string FileName;
        public uint Flags;
        public float GridSize;
        public uint IterationVersion;
        public long LastTeamTrigger;
        public string MapFileName;
        public uint MapVersion;
        public ushort[] Music;
        public int NumModulePlacements;
        public int NumOfClientVOGOs;
        public uint NumOfImports;
        public int NumOfVOGOs;
        public long OnKillTrigger;
        public long PerPlayerLoadTrigger;
        public SeaPlane SeaPlane;
        public byte TileSet;
        public bool UseClouds;
        public bool UseRoad;
        public bool UseTimeOfDay;
        public string WeatherStrEffect;

        public uint ContinentId;
        public string Description;
        public string Name;

        public IDictionary<uint, MissionString> MissionStrings = new Dictionary<uint, MissionString>();
        public IDictionary<ObjectType, IList<ClonedObjectBase>> ObjectBasesByType = new Dictionary<ObjectType, IList<ClonedObjectBase>>();
        public IDictionary<uint, Variable> Variables = new Dictionary<uint, Variable>();
        public IDictionary<uint, VisualWaypoint> VisualWaypoints = new Dictionary<uint, VisualWaypoint>();
        public IDictionary<byte, WeatherContainer> WeatherInfos = new Dictionary<byte, WeatherContainer>();
        public IList<RoadNodeBase> Roads = new List<RoadNodeBase>();
        #endregion

        public static MapEntry Read(FileEntry entry, BinaryReader br)
        {
            var t = new MapEntry { MapVersion = br.ReadUInt32(), MapFileName = entry.Name }; // 60 < MapVersion < 62

            #region Header
            if (t.MapVersion < 4 || t.MapVersion > 62)
                return null;

            if (t.MapVersion >= 27)
                t.IterationVersion = br.ReadUInt32();

            br.ReadBytes(8);

            t.GridSize = br.ReadSingle();
            t.TileSet = br.ReadByte();
            t.UseRoad = br.ReadBoolean();
            t.Music = br.Read<ushort>(3);

            if (t.MapVersion >= 11)
            {
                t.UseClouds = br.ReadBoolean();
                t.UseTimeOfDay = br.ReadBoolean();
                t.FileName = br.ReadLengthedString();
            }

            if (t.MapVersion >= 36)
                t.CullingStyle = br.ReadSingle();

            if (t.MapVersion >= 45)
                t.NumOfImports = br.ReadUInt32();
            #endregion

            #region Common Data
            t.EntryX = br.ReadSingle();
            t.EntryY = br.ReadSingle();
            t.EntryZ = br.ReadSingle();
            t.EntryW = br.ReadSingle();
            t.NumModulePlacements = br.ReadInt32();
            t.NumOfVOGOs = br.ReadInt32();
            t.NumOfClientVOGOs = br.ReadInt32();
            t.HighestCoid = br.ReadUInt32();
            t.PerPlayerLoadTrigger = br.ReadInt64();
            t.CreatorLoadTrigger = br.ReadInt64();

            if (t.MapVersion >= 33)
                t.OnKillTrigger = br.ReadInt64();

            if (t.MapVersion >= 34)
                t.LastTeamTrigger = br.ReadInt64();

            var missC = br.ReadUInt32();
            for (var i = 0U; i < missC; ++i)
            {
                var ms = MissionString.Read(br, t.MapVersion);
                t.MissionStrings.Add(ms.StringId, ms);
            }

            var wpC = br.ReadUInt32();
            for (var i = 0U; i < wpC; ++i)
            {
                var wp = VisualWaypoint.Read(br, t.MapVersion);
                t.VisualWaypoints.Add(wp.Id, wp);
            }

            var varC = br.ReadUInt32();
            for (var i = 0U; i < varC; ++i)
            {
                var v = Variable.Read(br, t.MapVersion);
                t.Variables.Add(v.Id, v);
            }

            if (t.MapVersion >= 47)
            {
                t.WeatherStrEffect = br.ReadLengthedString();

                var regionCount = br.ReadUInt32();
                for (var i = 0U; i < regionCount; ++i)
                {
                    var regionId = br.ReadByte();

                    if (!t.WeatherInfos.ContainsKey(regionId))
                        t.WeatherInfos.Add(regionId, new WeatherContainer());

                    var weatherCount = br.ReadUInt32();
                    for (var j = 0U; j < weatherCount; ++j)
                    {
                        t.WeatherInfos[regionId].Weathers.Add(new WeatherInfo
                        {
                            SpecialType = br.ReadUInt32(),
                            Type = br.ReadUInt32(),
                            PercentChance = br.ReadSingle(),
                            SpecialEventSkill = br.ReadInt32(),
                            EventTimesPerMinute = br.ReadByte(),
                            MinTimeToLive = br.ReadUInt32(),
                            MaxTimeToLive = br.ReadUInt32(),
                            LayerBits = t.MapVersion >= 54 ? br.ReadUInt32() : 1,
                            FxName = br.ReadLengthedString()
                        });
                    }

                    t.WeatherInfos[regionId].Effect = br.ReadLengthedString();

                    for (var j = 0; j < 4; ++j)
                        t.WeatherInfos[regionId].Environments.Add(br.ReadLengthedString());
                }
            }

            if (t.MapVersion >= 38)
            {
                // Sea Plane Data
                if (t.MapVersion >= 35)
                {
                    if (br.ReadByte() != 0)
                    {
                        var planeCount = br.ReadInt32();

                        t.SeaPlane.Coords = Vector4.Read(br);
                        t.SeaPlane.CoordsList = new List<Vector4>(planeCount);

                        for (var i = 0; i < planeCount; ++i)
                            t.SeaPlane.CoordsList.Add(Vector4.Read(br));
                    }
                }
            }

            #endregion

            Debug.Assert(t.NumModulePlacements == 0, "WTF Happened?!");

            #region VOGOs
            for (var i = 0; i < t.NumOfClientVOGOs + t.NumOfVOGOs; ++i)
            {
                byte layer = 0;
                if (t.MapVersion > 5)
                    layer = br.ReadByte();

                var cbid = br.ReadInt32();
                var coid = br.ReadInt32();

                var skipBytes = br.ReadInt32(); // skip this many bytes, if client already loaded this clone base object

                var obj = ClonedObjectBase.AllocateNewObjectFromCBID(cbid);
                if (obj == null)
                {
                    br.BaseStream.Seek(skipBytes, SeekOrigin.Current);
                    continue;
                }

                obj.InitializeFromCBID(cbid, null);
                obj.Layer = layer;
                obj.SetCOID(coid);

                var pos = br.BaseStream.Position;

                try
                {
                    obj.Unserialize(br, t.MapVersion);
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("EOS?!?!");
                }

                if (!t.ObjectBasesByType.ContainsKey(obj.Type))
                    t.ObjectBasesByType.Add(obj.Type, new List<ClonedObjectBase>());

                t.ObjectBasesByType[obj.Type].Add(obj);

                if (pos + skipBytes == br.BaseStream.Position)
                    continue;

                Console.WriteLine("COID: {0} ({1}) | {2} reading | Read size: {3} | Total size: {4} | Diff: {5}", coid, obj.Type, (pos + skipBytes > br.BaseStream.Position ? "under" : "over"), Math.Abs(br.BaseStream.Position - pos), skipBytes, skipBytes - Math.Abs(br.BaseStream.Position - pos));
                br.BaseStream.Position = pos + skipBytes;
            }

            #endregion

            #region Roads
            if (t.MapVersion >= 43)
                t.Flags = br.ReadUInt32();

            var numRoads = br.ReadUInt32();
            for (var i = 0U; i < numRoads; ++i)
            {
                /*var unk = */
                br.ReadUInt32();
                var type = br.ReadByte();

                RoadNodeBase roadNodeBase;

                switch (type)
                {
                    case 0:
                        roadNodeBase = new RoadNode();
                        break;

                    case 1:
                        roadNodeBase = new RoadJunction();
                        break;

                    case 2:
                        roadNodeBase = new RiverNode();
                        break;

                    default:
                        throw new InvalidDataException("Invalid road node base type!");
                }

                roadNodeBase.UnSerialize(br, t.MapVersion);

                t.Roads.Add(roadNodeBase);
            }
            #endregion
            #region Music
            if (t.MapVersion >= 42)
            {
                var numMusicRegion = br.ReadUInt32();
                for (var i = 0U; i < numMusicRegion; ++i)
                {
                    /*var unk = */
                    br.ReadUInt32();
                    if (t.MapVersion < 42)
                        continue;

                    /*var musicName = */
                    br.ReadLengthedString();
                    /*var looping = */
                    br.ReadBoolean();
                    /*var silenceatmaxradius = */
                    br.ReadBoolean();
                    /*var durationForRepeat = */
                    br.ReadSingle();
                    /*var fadeintime = */
                    br.ReadSingle();
                    /*var fadeouttime = */
                    br.ReadSingle();
                    /*var maxradius = */
                    br.ReadSingle();
                    /*var x = */
                    br.ReadSingle();
                    /*var y = */
                    br.ReadSingle();
                    /*var z = */
                    br.ReadSingle();
                    /*var musicType = */
                    br.ReadUInt32();
                }
            }
            #endregion
            #region MapFlair
            if (t.MapVersion >= 30)
            {
                /*var streamVer = */
                br.ReadUInt32();
                /*var width = */
                br.ReadInt32();
                /*var height = */
                br.ReadInt32();
                /*var maxobjcountperglomsector = */
                br.ReadInt32();

                var objectcount = br.ReadInt32();
                for (var i = 0; i < objectcount; ++i)
                {
                    /*var streamVer2 = */
                    br.ReadUInt32();
                    /*var cbidVisual = */
                    br.ReadInt32();
                    /*var drawSizeMin = */
                    br.ReadSingle();
                    /*var drawSizeVariance = */
                    br.ReadSingle();
                    /*var thresholdMin = */
                    br.ReadByte();
                    /*var thresholdMax = */
                    br.ReadByte();
                    /*var layerMask = */
                    br.ReadByte();
                    /*var placeWithGroundNormal = */
                    br.ReadByte();
                }

                var inlineDensityMap = br.ReadBoolean();
                Debug.Assert(!inlineDensityMap, "I HOPE THIS IS UNREACHABLE!");
            }
            #endregion

            var stream = AssetManager.GetStreamByName(entry.Name.ToLower().Replace(".fam", ".xml"));
            if (stream != null)
            {
                using (stream)
                {
                    var doc = XDocument.Load(stream);
                    Debug.Assert(doc != null);

                    var element = doc.Element("Map");
                    if (element == null)
                        return t;

                    t.ContinentId = (uint)element.Attribute("continentObjectID");
                    t.Name = (string)element.Element("Name");
                    t.Description = (string)element.Element("Description");
                }
            }
            else
            {
                Debug.Assert(false, "Nincs a mapnak info-ja ?!?!?!");
            }

            return t;
        }
    }
}
