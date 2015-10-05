using System;
using System.IO;

namespace Genesis.Shared.Entities.Base
{
    public class GraphicsPhysicsBase : ClonedObjectBase
    {
        public GraphicsBase CVOGGraphicsBase = new GraphicsBase();
        public PhysicsBase CVOGPhysicsBase = new PhysicsBase();

        public override void Unserialize(BinaryReader br, uint mapVersion)
        {
            CVOGGraphicsBase.UnserializeCreateEffect(br, mapVersion);
            CVOGGraphicsBase.UnserializeTooltip(br, mapVersion);
            ReadTriggerEvents(br, mapVersion);
            CVOGPhysicsBase.UnSerialize(br, mapVersion);
        }

        public override void WriteToCreatePacket(Packet packet, bool extended = false)
        {
            throw new NotSupportedException();
        }
    }
}
