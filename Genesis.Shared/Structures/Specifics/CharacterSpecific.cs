using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    public struct CharacterSpecific
    {
        public byte Race;
        public byte Class;
        public bool IsMale;
        public byte Flags;
        public short HPFactor;
        public short HPStart;

        public static CharacterSpecific Read(BinaryReader br)
        {
            var cs = new CharacterSpecific
            {
                IsMale = br.ReadInt32() != 0,
                HPStart = br.ReadInt16(),
                HPFactor = br.ReadInt16(),
                Flags = br.ReadByte(),
                Class = br.ReadByte(),
                Race = br.ReadByte()
            };

            br.ReadByte();

            return cs;
        }
    }
}
