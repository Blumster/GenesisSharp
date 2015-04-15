using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Genesis.Utils;
using TNL.NET.Entities;

namespace Genesis.Shared.Map
{
    using Constant;
    using Entities;
    using Entities.Base;
    using Manager;
    using Structures.XML;

    public class SectorMap
    {
        #region Declaration
        private Single _outpostTakenChance;
        private ContinentObject _continentObject;
        private Int64[] _skillTriggers;
        private Int32 _playerCount;
        private readonly Int32[] _playerByRaceCount = { 0, 0, 0 };
        private readonly Dictionary<TFID, ClonedObjectBase> _coList;
        #endregion

        public SectorMap(UInt32 continentId)
        {
            _outpostTakenChance = 0.0f;
            ContinentId = continentId;
            _continentObject = AssetManager.AssetContainer.GetContinentObjectById(ContinentId);
            _coList = new Dictionary<TFID, ClonedObjectBase>();

            MapEntry = AssetManager.LoadMap(String.Format("{0}.fam", _continentObject.MapFileName));
        }

        public void AddObjectToMap(ClonedObjectBase obj)
        {
            if (_coList.ContainsKey(obj.GetTFID()))
                _coList[obj.GetTFID()] = obj;
            else
                _coList.Add(obj.GetTFID(), obj);
        }

        public void RemoveFromMap(TFID tfid)
        {
            if (_coList.ContainsKey(tfid))
                _coList.Remove(tfid);
        }

        public ClonedObjectBase GetObject(TFID tfid)
        {
            return _coList.ContainsKey(tfid) ? _coList[tfid] : null;
        }

        public IDictionary<TFID, ClonedObjectBase> GetObjects()
        {
            return _coList;
        }

        public Int64 SkillTrigger(UInt32 x)
        {
            return x < _skillTriggers.Length ? _skillTriggers[x] : 0L;
        }

        public void ActivateAllSpawns()
        {
            foreach (var obj in _coList.Where(obj => obj.Value.GetAsSpawnPoint() != null))
                obj.Value.Activate(null);
        }

        public Single GetCriticalHitMultiplier(Single level)
        {
            return level * 0.01200000047683716f;
        }

        public Single GetOutpostTokenChance()
        {
            return _outpostTakenChance;
        }

        public void IncementPlayerCount(Character character, bool scale)
        {
            ++_playerCount;
            if (scale)
            {
                //scale ai for players
            }
            ++_playerByRaceCount[character.GetRace()]; // race
        }

        public UInt32 ContinentId { get; private set; }

        public MapEntry MapEntry { get; private set; }

        public Int32 GetNumberOfTerrainGridsPerObjectGrid()
        {
            return 1;
        }

        public Single GetGridSize()
        {
            return 1.0f;
        }

        public void WritePacket(Packet packet)
        {
            //packet.WritePadding(4);

            // SVOG header begin -->
            packet.WriteInteger(0); // Region Id
            packet.WriteInteger(0); // Region Type
            packet.WriteByte(1); // Region Level

            packet.WritePadding(3);

            packet.WriteInteger(0); // Layer Id
            packet.WriteInteger(_continentObject.Objective); // Objective Index
            packet.WriteUtf8StringOn(String.Format("{0}.fam", _continentObject.MapFileName), 65); // Map Name
            packet.WriteBoolean(_continentObject.IsTown); // Is Town
            packet.WriteBoolean(_continentObject.IsArena); // Is Arena

            packet.WritePadding(1);

            packet.WriteInteger(_continentObject.OwningFaction); // Race Faction
            packet.WriteInteger(ContinentId); // Continent Object ID
            packet.WriteBoolean(_continentObject.IsPersistent); // Is Persistent

            packet.WritePadding(3);

            packet.WriteInteger(MapEntry.IterationVersion); // Map Iteration Version
            packet.WriteInteger(_continentObject.ContestedMission); // Contested Mission Id

            packet.WritePadding(4);

            packet.WriteLong(ContinentId); // COID Map
            // SVOG header end   <--

            packet.WriteInteger(123456789); // Temporal Random Seed
            packet.WriteLong(ContinentId); // COID Map
            packet.WriteShort(0); // Number of Module Selections

            // for NumberOfModuleSelections { 24 byte }

            // CND Unaligned Vector 3 begin -->
            packet.WriteSingle(0.0f);
            packet.WriteSingle(0.0f);
            packet.WriteSingle(0.0f);
            // CND Unaligned Vector 3 end <--

            packet.WriteShort(0);
            /*packet.WriteShort(36); // Weather Count

            // Weather Update begin -->
            packet.WriteInteger(0x2069);
            packet.WriteLong(0);
            packet.WriteLong(0);
            packet.WriteLong(0);
            packet.WriteLong(0);*/
            // Weather Update end <--
        }

        public void BroadcastChat(ChatType type, Packet packet, ClonedObjectBase source)
        {
            foreach (var pair in _coList)
            {
                if (!(pair.Value is Character))
                    continue;

                var dX = pair.Value.Position.X - source.Position.X;
                var dY = pair.Value.Position.X - source.Position.Y;

                var dist = Math.Sqrt(dX * dX + dY * dY);

                switch (type)
                {
                    default:
                        Logger.WriteLog("Unhandled ChatType in BroadcastChat: {0}", LogType.Error, type);
                        break;
                }
                
                (pair.Value as Character).Connection.SendPacket(packet, RPCGuaranteeType.RPCGuaranteedOrdered);
            }
        }
    }
}
