using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    public struct TinkeringKitSpecific
    {
        public short MaxSlotLevel;
        public uint ObjectTypeRestriction;

        public static TinkeringKitSpecific Read(BinaryReader br)
        {
            var tks = new TinkeringKitSpecific
            {
                MaxSlotLevel = br.ReadInt16()
            };

            br.ReadInt16();

            tks.ObjectTypeRestriction = br.ReadUInt32();

            return tks;
        }
    }
}
