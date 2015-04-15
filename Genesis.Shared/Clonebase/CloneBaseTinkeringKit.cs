using System.IO;

namespace Genesis.Shared.Clonebase
{
    using Structures.Specifics;

    public class CloneBaseTinkeringKit : CloneBaseObject
    {
        public TinkeringKitSpecific TinkeringKitSpecific;

        public CloneBaseTinkeringKit(BinaryReader br)
            : base(br)
        {
            TinkeringKitSpecific = TinkeringKitSpecific.Read(br);
        }
    }
}
