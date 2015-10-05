using System.IO;

namespace Genesis.Shared.Structures
{
    public struct TriggerConditional
    {
        public uint LeftId;
        public uint RightId;
        public byte Type;

        public static TriggerConditional Read(BinaryReader br)
        {
            var cond = new TriggerConditional
            {
                LeftId = br.ReadUInt32(),
                RightId = br.ReadUInt32(),
                Type = br.ReadByte()
            };

            br.ReadBytes(3);

            return cond;
        }
    }
}
