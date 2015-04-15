using System;
using System.IO;

namespace Genesis.Shared.Entities.Base
{
    public class GraphicsPhysicsBase : ClonedObjectBase
    {
        public GraphicsBase CVOGGraphicsBase = new GraphicsBase();
        public PhysicsBase CVOGPhysicsBase = new PhysicsBase();

        public override void Unserialize(BinaryReader br, UInt32 mapVersion)
        {
            CVOGGraphicsBase.UnserializeCreateEffect(br, mapVersion);
            CVOGGraphicsBase.UnserializeTooltip(br, mapVersion);
            ReadTriggerEvents(br, mapVersion);
            CVOGPhysicsBase.UnSerialize(br, mapVersion);
        }

        public override void WriteToCreatePacket(Packet packet, Boolean extended = false)
        {
            throw new NotSupportedException();
        }
    }
}
