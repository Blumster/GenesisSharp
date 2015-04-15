using System;
using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct Variable
    {
        public UInt32 Id;
        public Single InitialValue;
        public List<UInt64> Triggers;
        public Byte Type;
        public Boolean UniqueForImport;
        public Single Value;

        public static Variable Read(BinaryReader br, UInt32 mapVersion)
        {
            return new Variable
            {
                Id = br.ReadUInt32(),
                Type = br.ReadByte(),
                Value = br.ReadSingle(),
                InitialValue = br.ReadSingle(),
                UniqueForImport = mapVersion >= 46 && br.ReadBoolean(),
                Triggers = new List<UInt64>(br.Read<UInt64>(8))
            };
        }
    }
}
