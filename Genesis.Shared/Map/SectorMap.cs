using System;
using System.Collections.Generic;
using System.Linq;

using TNL.NET.Entities;

namespace Genesis.Shared.Map
{
    using Constant;
    using Entities;
    using Entities.Base;
    using Manager;
    using Structures.XML;
    using Utils;

    public class SectorMap
    {
        #region Declaration
        private float _outpostTakenChance;
        private ContinentObject _continentObject;
        private long[] _skillTriggers;
        private int _playerCount;
        private readonly int[] _playerByRaceCount = { 0, 0, 0 };
        private readonly Dictionary<TFID, ClonedObjectBase> _coList;
        #endregion

        public SectorMap(uint continentId)
        {
            _outpostTakenChance = 0.0f;
            ContinentId = continentId;
            _continentObject = AssetManager.AssetContainer.GetContinentObjectById(ContinentId);
            _coList = new Dictionary<TFID, ClonedObjectBase>();

            MapEntry = AssetManager.LoadMap($"{_continentObject.MapFileName}.fam");
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

        public long SkillTrigger(uint x)
        {
            return x < _skillTriggers.Length ? _skillTriggers[x] : 0L;
        }

        public void ActivateAllSpawns()
        {
            foreach (var obj in _coList.Where(obj => obj.Value.GetAsSpawnPoint() != null))
                obj.Value.Activate(null);
        }

        public float GetCriticalHitMultiplier(float level)
        {
            return level * 0.01200000047683716f;
        }

        public float GetOutpostTokenChance()
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

        public uint ContinentId { get; }

        public MapEntry MapEntry { get; }

        public int GetNumberOfTerrainGridsPerObjectGrid()
        {
            return 1;
        }

        public float GetGridSize()
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
            packet.WriteUtf8StringOn($"{_continentObject.MapFileName}.fam", 65); // Map Name
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
            foreach (var pair in from pair in _coList where pair.Value is Character let dX = pair.Value.Position.X - source.Position.X let dY = pair.Value.Position.X - source.Position.Y let dist = Math.Sqrt(dX * dX + dY * dY) select pair)
            {
                switch (type)
                {
                    default:
                        Logger.WriteLog("Unhandled ChatType in BroadcastChat: {0}", LogType.Error, type);
                        break;
                }
                
                (pair.Value as Character)?.Connection.SendPacket(packet, RPCGuaranteeType.RPCGuaranteedOrdered);
            }
        }
    }
}
