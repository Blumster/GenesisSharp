using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    public struct SpawnList
    {
        public Boolean IsTemplate;
        public Byte LevelOffset;
        public Byte LowerNumberOfSpawns;
        public Int32 SpawnType;
        public Byte UpperNumberOfSpawns;

        public static SpawnList Read(BinaryReader br)
        {
            var sl = new SpawnList { LowerNumberOfSpawns = br.ReadByte(), UpperNumberOfSpawns = br.ReadByte() };

            br.ReadBytes(2);

            sl.SpawnType = br.ReadInt32();
            sl.LevelOffset = br.ReadByte();
            sl.IsTemplate = br.ReadBoolean();

            br.ReadBytes(2);

            return sl;
        }
    }
}
