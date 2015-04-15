using System;
using System.IO;

namespace Genesis.Shared.Entities
{
    public class RiverNode : RoadNode
    {
        public Int32 ReflectColor;
        public Int32 RefractColor;
        public Single WaterDepth;

        public override void UnSerialize(BinaryReader br, UInt32 mapVersion)
        {
            base.UnSerialize(br, mapVersion);

            WaterDepth = br.ReadSingle();

            if (mapVersion < 12)
                return;

            ReflectColor = br.ReadInt32();
            RefractColor = br.ReadInt32();
        }
    }
}
