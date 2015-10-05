using System.IO;

namespace Genesis.Shared.Structures
{
    public struct SpawnList
    {
        public bool IsTemplate;
        public byte LevelOffset;
        public byte LowerNumberOfSpawns;
        public int SpawnType;
        public byte UpperNumberOfSpawns;

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
