using System;
using System.IO;

namespace Genesis.Shared.Entities
{
    using Structures;

    public class EnterPoint : SimpleObject
    {
        public Vector4 Location;
        public UInt32 MapTransferData;
        public Byte MapTransferType;
        public Vector4 Quaternion;
        public Int32 Unk;

        public override void Unserialize(BinaryReader br, uint mapVersion)
        {
            Location = Vector4.Read(br);
            Quaternion = Vector4.Read(br);
            MapTransferType = br.ReadByte();
            MapTransferData = br.ReadUInt32();

            if (mapVersion >= 7)
                Unk = br.ReadInt32();
        }
    }
}
