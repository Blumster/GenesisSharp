using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    public struct TriggerConditional
    {
        public UInt32 LeftId;
        public UInt32 RightId;
        public Byte Type;

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
