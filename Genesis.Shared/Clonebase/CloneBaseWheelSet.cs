using System.IO;

namespace Genesis.Shared.Clonebase
{
    using Structures.Specifics;

    public class CloneBaseWheelSet : CloneBaseObject
    {
        public WheelSetSpecific WheelSetSpecific;

        public CloneBaseWheelSet(BinaryReader br)
            : base(br)
        {
            WheelSetSpecific = WheelSetSpecific.Read(br);
        }
    }
}
