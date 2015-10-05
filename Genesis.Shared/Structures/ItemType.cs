using System.IO;

namespace Genesis.Shared.Structures
{
    public struct ItemType
    {
        public int CBID;
        public byte TypeOfItem;
        public float Percentage;
        public byte Unlimited;
        public int Value;

        public static ItemType Read(BinaryReader br)
        {
            return new ItemType
            {
                TypeOfItem = br.ReadByte(),
                Percentage = br.ReadSingle(),
                Value = br.ReadInt32(),
                Unlimited = br.ReadByte(),
                CBID = br.ReadInt32()
            };
        }
    }
}
