using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct MissionString
    {
        public UInt32 OwnerId;
        public UInt32 StringId;
        public String Text;
        public Byte Type;

        public static MissionString Read(BinaryReader br, UInt32 mapVersion)
        {
            return new MissionString
            {
                StringId = br.ReadUInt32(),
                OwnerId = br.ReadUInt32(),
                Type = mapVersion >= 18 ? br.ReadByte() : (Byte) 0,
                Text = br.ReadLengthedString()
            };
        }
    }
}
