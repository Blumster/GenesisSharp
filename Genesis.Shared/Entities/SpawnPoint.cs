using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Entities
{
    using Base;
    using Structures;
    using Utils.Extensions;

    public class SpawnPoint : GraphicsBase // stabil pont: -352 == activation range | off: 80 h
    {
        public float ActivationRange;
        public byte ChampionChance;
        public bool FactionDirty;
        public bool HasChampion;
        public float InitialPatrolDistance;
        public Vector4 Location;
        public int Loot;
        public float LootChance;
        public float LootPercent;
        public ulong MapPathCOID;
        public uint OriginalFaction;
        public Vector4 Quaternion;
        public float Radius;
        public byte RandomlyOffsetSpawnPosition;
        public float RespawnTime;
        public byte SpawnChance;
        public List<SpawnList> SpawnLists = new List<SpawnList>();
        public bool UseGenerator;

        public override void Unserialize(BinaryReader br, uint mapVersion)
        {
            ReadTriggerEvents(br, mapVersion);

            Location = Vector4.Read(br);
            Quaternion = Vector4.Read(br);
            Radius = br.ReadSingle();
            RespawnTime = br.ReadSingle();
            ActivationRange = br.ReadSingle();

            UseGenerator = br.ReadBoolean();
            HasChampion = br.ReadBoolean();
            ChampionChance = br.ReadByte();
            SpawnChance = br.ReadByte();
            var j = br.ReadByte();

            if (mapVersion >= 31)
                RandomlyOffsetSpawnPosition = br.ReadByte();

            if (mapVersion >= 29)
                for (var i = 0; i < 12; ++i)
                    SpawnLists.Add(SpawnList.Read(br));

            Loot = br.ReadInt32();
            LootPercent = br.ReadSingle();
            MapPathCOID = br.ReadUInt64();
            InitialPatrolDistance = br.ReadSingle();

            if (mapVersion >= 15)
            {
                FactionDirty = br.ReadBoolean();
                OriginalFaction = br.ReadUInt32();
            }

            if (mapVersion >= 24)
                LootChance = br.ReadSingle();

            if (mapVersion >= 32)
            {
                var str = br.ReadLengthedString();
            }
        }

        public override SpawnPoint GetAsSpawnPoint()
        {
            return this;
        }
    }
}
