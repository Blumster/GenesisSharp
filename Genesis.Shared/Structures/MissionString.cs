using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct MissionString
    {
        public uint OwnerId;
        public uint StringId;
        public string Text;
        public byte Type;

        public static MissionString Read(BinaryReader br, uint mapVersion)
        {
            return new MissionString
            {
                StringId = br.ReadUInt32(),
                OwnerId = br.ReadUInt32(),
                Type = mapVersion >= 18 ? br.ReadByte() : (byte) 0,
                Text = br.ReadLengthedString()
            };
        }
    }
}
