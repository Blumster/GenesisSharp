using System;
using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Entities
{
    using Base;
    using Structures;
    using Utils.Extensions;

    public class MapPath : ClonedObjectBase
    {
        public UInt32 DefaultMapPath;
        public List<MapPathPoint> MapPathPoints = new List<MapPathPoint>();
        public String PathName;
        public Boolean ReverseDirection;

        public override void Unserialize(BinaryReader br, UInt32 mapVersion)
        {
            DefaultMapPath = br.ReadUInt32();
            ReverseDirection = br.ReadBoolean();
            PathName = br.ReadUtf8StringOn(64);

            var pointCount = br.ReadUInt32();
            for (var i = 0U; i < pointCount; ++i)
                MapPathPoints.Add(MapPathPoint.Read(br));
        }

        public override void WriteToCreatePacket(Packet packet, Boolean extended = false)
        {
            throw new NotSupportedException();
        }
    }
}
