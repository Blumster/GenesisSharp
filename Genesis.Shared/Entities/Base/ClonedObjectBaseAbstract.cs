using System;
using System.IO;

namespace Genesis.Shared.Entities.Base
{
    public abstract partial class ClonedObjectBase
    {
        public abstract void Unserialize(BinaryReader br, UInt32 mapVersion);

        public abstract void WriteToCreatePacket(Packet packet, Boolean extended = false);
    }
}
