using System.IO;

namespace Genesis.Shared.Clonebase
{
    using Structures.Specifics;

    public class CloneBaseGadget : CloneBaseObject
    {
        public GadgetSpecific GadgetSpecific;

        public CloneBaseGadget(BinaryReader br)
            : base(br)
        {
            GadgetSpecific = GadgetSpecific.Read(br);
        }
    }
}
