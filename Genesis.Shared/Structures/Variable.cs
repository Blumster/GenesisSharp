using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct Variable
    {
        public uint Id;
        public float InitialValue;
        public List<ulong> Triggers;
        public byte Type;
        public bool UniqueForImport;
        public float Value;

        public static Variable Read(BinaryReader br, uint mapVersion)
        {
            return new Variable
            {
                Id = br.ReadUInt32(),
                Type = br.ReadByte(),
                Value = br.ReadSingle(),
                InitialValue = br.ReadSingle(),
                UniqueForImport = mapVersion >= 46 && br.ReadBoolean(),
                Triggers = new List<ulong>(br.Read<ulong>(8))
            };
        }
    }
}
