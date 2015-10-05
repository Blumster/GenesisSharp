using System.IO;

namespace Genesis.Shared.Entities
{
    public class RiverNode : RoadNode
    {
        public int ReflectColor;
        public int RefractColor;
        public float WaterDepth;

        public override void UnSerialize(BinaryReader br, uint mapVersion)
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
