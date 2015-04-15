using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    public struct TextParam
    {
        public Single CachedValue;
        public UInt32 Id;
        public Byte Type;

        public static TextParam Read(BinaryReader br, UInt32 mapVersion)
        {
            var tParam = new TextParam { Type = br.ReadByte() };

            br.ReadBytes(3);

            tParam.Id = br.ReadUInt32();
            tParam.CachedValue = mapVersion < 14 ? 0.0f : br.ReadSingle();

            return tParam;
        }
    }
}
