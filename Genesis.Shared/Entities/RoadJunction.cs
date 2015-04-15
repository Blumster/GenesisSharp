using System;
using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Entities
{
    using Base;
    using Structures;

    public class RoadJunction : RoadNodeBase
    {
        public List<Vector3> Directions = new List<Vector3>();
        public List<Vector3> Positions = new List<Vector3>();
        public Single Rotation;

        public override void UnSerialize(BinaryReader br, UInt32 mapVersion)
        {
            base.UnSerialize(br, mapVersion);

            Rotation = br.ReadSingle();

            if (mapVersion < 28)
                return;

            for (var i = 0; i < 6; ++i)
            {
                Positions.Add(Vector3.Read(br));
                Directions.Add(Vector3.Read(br));
            }
        }
    }
}
