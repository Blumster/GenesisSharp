using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    public struct ItemType
    {
        public Int32 CBID;
        public Byte TypeOfItem;
        public Single Percentage;
        public Byte Unlimited;
        public Int32 Value;

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
