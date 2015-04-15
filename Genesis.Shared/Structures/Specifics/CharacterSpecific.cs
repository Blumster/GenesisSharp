using System;
using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    public struct CharacterSpecific
    {
        public Byte Race;
        public Byte Class;
        public Boolean IsMale;
        public Byte Flags;
        public Int16 HPFactor;
        public Int16 HPStart;

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
