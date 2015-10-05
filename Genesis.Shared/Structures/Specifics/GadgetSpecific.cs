using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    public struct GadgetSpecific
    {
        public uint ObjectType;
        public int Prefix;

        public static GadgetSpecific Read(BinaryReader br)
        {
            return new GadgetSpecific
            {
                Prefix = br.ReadInt32(),
                ObjectType = br.ReadUInt32()
            };
        }
    }
}
