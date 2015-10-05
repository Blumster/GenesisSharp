using System;
using System.IO;

namespace Genesis.Shared.Entities.Base
{
    using Utils.Extensions;

    public class GraphicsBase : ClonedObjectBase
    {
        public string FxCreateExtraName;
        public string ToolTip;
        public uint Unk;
        public byte UnkFlagByte;

        public void UnserializeCreateEffect(BinaryReader br, uint mapVersion)
        {
            if (mapVersion >= 21)
                FxCreateExtraName = br.ReadLengthedString();

            if (mapVersion >= 48)
                UnkFlagByte = br.ReadByte();

            if (mapVersion >= 62)
                Unk = br.ReadUInt32();
        }

        public void UnserializeTooltip(BinaryReader br, uint mapVersion)
        {
            if (mapVersion >= 22)
                ToolTip = br.ReadLengthedString();
        }

        public override void Unserialize(BinaryReader br, uint mapVersion)
        {

        }

        public override void WriteToCreatePacket(Packet packet, bool extended = false)
        {
            throw new NotSupportedException();
        }
    }
}
