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
        public uint DefaultMapPath;
        public List<MapPathPoint> MapPathPoints = new List<MapPathPoint>();
        public string PathName;
        public bool ReverseDirection;

        public override void Unserialize(BinaryReader br, uint mapVersion)
        {
            DefaultMapPath = br.ReadUInt32();
            ReverseDirection = br.ReadBoolean();
            PathName = br.ReadUtf8StringOn(64);

            var pointCount = br.ReadUInt32();
            for (var i = 0U; i < pointCount; ++i)
                MapPathPoints.Add(MapPathPoint.Read(br));
        }

        public override void WriteToCreatePacket(Packet packet, bool extended = false)
        {
            throw new NotSupportedException();
        }
    }
}
