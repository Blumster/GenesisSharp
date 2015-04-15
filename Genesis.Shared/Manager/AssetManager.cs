using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Genesis.Shared.Manager
{
    using Clonebase;
    using Constant;
    using Map;
    using Mission;
    using Prefix;
    using Skill;
    using Structures.Asset;
    using Structures.XML;
    using Utils;

    public static class AssetManager
    {
        #region Declarations
        public static AssetContainer AssetContainer { get; private set; }
        #endregion

        static AssetManager()
        {
            AssetContainer = new AssetContainer();
        }

        public static void Initialize(String assetPath)
        {
            Logger.WriteLog("+++ Initializing Asset Manager", LogType.Initialize);

            ReadEntries(assetPath);
            ReadXML(assetPath);
            ReadWAD(assetPath);

            //LoadMaps();
        }

        private static void ReadEntries(String path)
        {
            AssetContainer.AccessLock.EnterWriteLock();
            AssetContainer.FileEntries.Clear();
            AssetContainer.DuplicatedFileEntries.Clear();
            AssetContainer.AccessLock.ExitWriteLock();

            if (!Directory.Exists(path))
                throw new NullReferenceException("Path is invalid!");

            Directory.GetFiles(path, "*.glm", SearchOption.AllDirectories).ToList().ForEach(ReadFile);
        }

        private static void ReadFile(String fileName)
        {
            using (var br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
            {
                br.BaseStream.Seek(br.BaseStream.Length - 4, SeekOrigin.Begin);

                var headerOff = br.ReadInt32();
                br.BaseStream.Seek(headerOff, SeekOrigin.Begin);

                var strHeader = Encoding.UTF8.GetString(br.ReadBytes(4));
                Debug.Assert(strHeader == "CHNK");

                var opts = br.ReadBytes(4);
                Debug.Assert(opts[0] == 66); // No support for text reading yet!
                Debug.Assert(opts[1] == 76); // As in client

                var strTableOff = br.ReadInt32();
                var strTableSize = br.ReadInt32();
                var entryCount = br.ReadInt32();

                var currPos = br.BaseStream.Position;

                br.BaseStream.Seek(strTableOff, SeekOrigin.Begin);

                var stringTable = br.ReadBytes(strTableSize);
                var fileEntries = CreateEntriesByStringTable(stringTable);

                Debug.Assert(fileEntries.Count == entryCount);

                br.BaseStream.Position = currPos;

                AssetContainer.AccessLock.EnterWriteLock();

                foreach (var entry in fileEntries)
                {
                    entry.Read(br, fileName);

                    if (!AssetContainer.FileEntries.ContainsKey(entry.Name))
                        AssetContainer.FileEntries.Add(entry.Name, entry);
                    else
                        AssetContainer.DuplicatedFileEntries.Add(entry.Name, entry);
                }
                AssetContainer.AccessLock.ExitWriteLock();
            }
        }

        private static List<FileEntry> CreateEntriesByStringTable(IEnumerable<Byte> data)
        {
            var sList = new List<FileEntry>();

            var sb = new StringBuilder();

            foreach (var t in data)
            {
                if (t != 0)
                    sb.Append((Char)t);
                else
                {
                    sList.Add(new FileEntry { Name = sb.ToString() });
                    sb.Clear();
                }
            }
            return sList;
        }

        public static FileEntry GetFileEntryByName(String name, String source = "")
        {
            AssetContainer.AccessLock.EnterReadLock();

            FileEntry ret = null;

            if (AssetContainer.FileEntries.ContainsKey(name))
                if (source == "" || AssetContainer.FileEntries[name].FileName.EndsWith(source))
                    ret = AssetContainer.FileEntries[name];

            if (AssetContainer.DuplicatedFileEntries.ContainsKey(name))
                if (source == "" || AssetContainer.DuplicatedFileEntries[name].FileName.EndsWith(source))
                    ret = AssetContainer.DuplicatedFileEntries[name];

            AssetContainer.AccessLock.ExitReadLock();

            return ret;
        }

        public static BinaryReader GetReaderByName(String name)
        {
            return GetReaderByEntry(GetFileEntryByName(name));
        }

        public static BinaryReader GetReaderByEntry(FileEntry entry)
        {
            return new BinaryReader(GetStreamByEntry(entry));
        }

        public static MemoryStream GetStreamByName(String name, String source = "")
        {
            return GetStreamByEntry(GetFileEntryByName(name, source));
        }

        public static MemoryStream GetStreamByEntry(FileEntry entry)
        {
            if (entry == null)
                return null;

            using (var br = new BinaryReader(new FileStream(entry.FileName, FileMode.Open, FileAccess.Read)))
            {
                br.BaseStream.Position = entry.Offset;

                return new MemoryStream(br.ReadBytes((Int32)entry.Size));
            }
        }

        public static List<FileEntry> GetFileEntriesByName(String name)
        {
            AssetContainer.AccessLock.EnterReadLock();

            var ret = (from e in AssetContainer.FileEntries where e.Key.StartsWith(name) select e.Value).ToList();
            ret.AddRange(from e in AssetContainer.DuplicatedFileEntries where e.Key.StartsWith(name) select e.Value);

            AssetContainer.AccessLock.ExitReadLock();

            return ret;
        }

        /*case "fat": // Map written in text mode, actually it doesn't exist in the packed files
        case "fam": // Map
        case "xml": // Xml
        case "txt": // Text
        case "ndw":
        // Not needed for the server (yet or anyways)
        case "cat": // Catalog
        case "dds":
        case "DDS":
        case "png": // Png
        case "tga": // Tga
        case "pgm":
        case "bak": // Backup
        case "anm": // Animation
        case "fx":
        case "fxh":
        case "fxi":
        case "geo":
        case "geo01":
        case "ogg": // Ogg
        // Unknown yet
        case "tec":
        case "sha":
        case "spt":
        case "scc":
        case "cache":
        case "tk":
        case "lnk":*/

        public static void ReadWAD(String path)
        {
            using (var br = new BinaryReader(new FileStream(String.Format("{0}clonebase.wad", path), FileMode.Open, FileAccess.Read)))
            {
                var version = br.ReadUInt32();
                Debug.Assert(version == 27);

                #region CloneBaseObjects
                var objectCount = br.ReadUInt32();
                for (var i = 0U; i < objectCount; ++i)
                {
                    var type = br.ReadUInt32();

                    CloneBaseObject cb;

                    switch ((ObjectType) type)
                    {
                        case ObjectType.Object:
                        case ObjectType.ObjectGraphicsPhysics:
                        case ObjectType.QuestObject:
                        case ObjectType.Item:
                        case ObjectType.Store:
                        case ObjectType.EnterPoint:
                        case ObjectType.ExitPoint:
                        case ObjectType.ContinentObject:
                        case ObjectType.Convoy:
                        case ObjectType.SpawnPoint:
                        case ObjectType.Trigger:
                        case ObjectType.Reaction:
                        case ObjectType.MapModulePlacement:
                        case ObjectType.MapPath:
                        case ObjectType.Money:
                        case ObjectType.Outpost:
                            cb = new CloneBaseObject(br);
                            break;

                        case ObjectType.Commodity:
                            cb = new CloneBaseCommodity(br);
                            break;

                        case ObjectType.Character:
                            cb = new CloneBaseCharacter(br);
                            break;

                        case ObjectType.Weapon:
                        case ObjectType.Bullet:
                            cb = new CloneBaseWeapon(br);
                            break;

                        case ObjectType.Gadget:
                            cb = new CloneBaseGadget(br);
                            break;

                        case ObjectType.TinkeringKit:
                            cb = new CloneBaseTinkeringKit(br);
                            break;

                        case ObjectType.Vehicle:
                            cb = new CloneBaseVehicle(br);
                            break;

                        case ObjectType.PowerPlant:
                            cb = new CloneBasePowerPlant(br);
                            break;

                        case ObjectType.WheelSet:
                            cb = new CloneBaseWheelSet(br);
                            break;

                        case ObjectType.Creature:
                            cb = new CloneBaseCreature(br);
                            break;

                        case ObjectType.Armor:
                            cb = new CloneBaseArmor(br);
                            break;

                        default:
                            throw new Exception("asd");
                    }

                    AssetContainer.AccessLock.EnterWriteLock();
                    AssetContainer.CloneBaseObjects.Add(cb.CloneBaseSpecific.CloneBaseId, cb);
                    AssetContainer.AccessLock.ExitWriteLock();
                }
                #endregion

                var missionCount = br.ReadUInt32();
                for (var i = 0U; i < missionCount; ++i)
                {
                    var q = Mission.Read(br);

                    AssetContainer.AccessLock.EnterWriteLock();
                    AssetContainer.Missions.Add(q.Id, q);
                    AssetContainer.AccessLock.ExitWriteLock();
                }

                var skillCount = br.ReadUInt32();
                for (var i = 0U; i < skillCount; ++i)
                {
                    var s = Skill.Read(br);

                    AssetContainer.AccessLock.EnterWriteLock();
                    AssetContainer.Skills.Add(s.Id, s);
                    AssetContainer.AccessLock.ExitWriteLock();
                }

                var armorPrefCount = br.ReadInt32();
                for (var i = 0; i < armorPrefCount; ++i)
                {
                    var pa = new PrefixArmor(br);

                    AssetContainer.AccessLock.EnterWriteLock();
                    AssetContainer.ArmorPrefixes.Add(pa.Id, pa);
                    AssetContainer.AccessLock.ExitWriteLock();
                }

                var powerPlantPrefCount = br.ReadInt32();
                for (var i = 0; i < powerPlantPrefCount; ++i)
                {
                    var ppp = new PrefixPowerPlant(br);

                    AssetContainer.AccessLock.EnterWriteLock();
                    AssetContainer.PowerPlantPrefixes.Add(ppp.Id, ppp);
                    AssetContainer.AccessLock.ExitWriteLock();
                }

                var weaponPrefCount = br.ReadInt32();
                for (var i = 0; i < weaponPrefCount; ++i)
                {
                    var pw = new PrefixWeapon(br);

                    AssetContainer.AccessLock.EnterWriteLock();
                    AssetContainer.WeaponPrefixes.Add(pw.Id, pw);
                    AssetContainer.AccessLock.ExitWriteLock();
                }

                var vehiclePrefCount = br.ReadInt32();
                for (var i = 0; i < vehiclePrefCount; ++i)
                {
                    var pv = new PrefixVehicle(br);

                    AssetContainer.AccessLock.EnterWriteLock();
                    AssetContainer.VehiclePrefixes.Add(pv.Id, pv);
                    AssetContainer.AccessLock.ExitWriteLock();
                }

                var ornamentPrefCount = br.ReadInt32();
                for (var i = 0; i < ornamentPrefCount; ++i)
                {
                    var po = new PrefixOrnament(br);

                    AssetContainer.AccessLock.EnterWriteLock();
                    AssetContainer.OrnamentPrefixes.Add(po.Id, po);
                    AssetContainer.AccessLock.ExitWriteLock();
                }

                var raceItemPrefCount = br.ReadInt32();
                for (var i = 0U; i < raceItemPrefCount; ++i)
                {
                    var pri = new PrefixRaceItem(br);

                    AssetContainer.AccessLock.EnterWriteLock();
                    AssetContainer.RaceItemPrefixes.Add(pri.Id, pri);
                    AssetContainer.AccessLock.ExitWriteLock();
                }

                Debug.Assert(br.BaseStream.Position == br.BaseStream.Length);
            }
        }

        public static void ReadXML(String path) // TODO: Analyze data, what will be needed
        {
            var des = new XmlSerializer(typeof(DataHolder));

            using (var sr = new StreamReader(String.Format(@"{0}\wad.xml", path)))
            {
                var dh = des.Deserialize(sr) as DataHolder;
                if (dh == null)
                    return;

                foreach (var c in dh.ConfigNewCharacters)
                    c.OnDeserialization();

                foreach (var c in dh.ContinentObjects)
                    c.OnDeserialization();
            }
        }

        public static MapEntry LoadMap(String name)
        {
            AssetContainer.AccessLock.EnterReadLock();

            if (AssetContainer.MapEntries.ContainsKey(name))
            {
                var me = AssetContainer.MapEntries[name];

                AssetContainer.AccessLock.ExitReadLock();

                return me;
            }

            var mapEntry = AssetContainer.FileEntries.SingleOrDefault(fEntry => fEntry.Key == name);

            AssetContainer.AccessLock.ExitReadLock();

            using (var br = GetReaderByEntry(mapEntry.Value))
            {
                var me = MapEntry.Read(mapEntry.Value, br);

                AssetContainer.AddMapEntry(mapEntry.Key, me);

                return me;
            }
        }

        /*private void LoadMaps()
        {
            AssetContainer.AccessLock.EnterReadLock();

            var query = AssetContainer.FileEntries.Where(fEntry => fEntry.Key.EndsWith(".fam")).Concat(AssetContainer.DuplicatedFileEntries.Where(fe => fe.Key.EndsWith(".fam"))); // .AsParalalell();

            AssetContainer.AccessLock.ExitReadLock();

            //var s1 = Stopwatch.StartNew();

            foreach (var fEntry in query)
            {
                using (var br = GetReaderByEntry(fEntry.Value))
                {
                    var me = MapEntry.Read(fEntry.Value, br);

                    AssetContainer.AddMapEntry(fEntry.Key, me);
                }
            }

            //s1.Stop();
            //Console.WriteLine((s1.Elapsed.TotalMilliseconds * 1000000.0D).ToString("0.00000 ns"));
        }*/
    }
}
