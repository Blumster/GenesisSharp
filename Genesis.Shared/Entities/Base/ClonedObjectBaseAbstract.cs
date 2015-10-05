using System.IO;

namespace Genesis.Shared.Entities.Base
{
    public abstract partial class ClonedObjectBase
    {
        public abstract void Unserialize(BinaryReader br, uint mapVersion);

        public abstract void WriteToCreatePacket(Packet packet, bool extended = false);
    }
}
