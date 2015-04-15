using System;
using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Entities.Base
{
    using Structures;
    using Utils.Extensions;

    public class RoadNodeBase
    {
        public String FileName;
        public List<Int32> NodeIds = new List<Int32>();
        public Vector3 Position;
        public UInt32 UniqueId;

        public virtual void UnSerialize(BinaryReader br, UInt32 mapVersion)
        {
            UniqueId = br.ReadUInt32();
            Position = Vector3.Read(br);
            FileName = br.ReadUtf8StringOn(260);

            var nodeCount = br.ReadUInt32();
            for (var i = 0; i < nodeCount; ++i)
                NodeIds.Add(br.ReadInt32());
        }
    }
}
