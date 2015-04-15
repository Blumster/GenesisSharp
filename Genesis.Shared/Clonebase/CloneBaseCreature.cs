using System.IO;

namespace Genesis.Shared.Clonebase
{
    using Structures.Specifics;

    public class CloneBaseCreature : CloneBaseObject
    {
        public CreatureSpecific CreatureSpecific;

        public CloneBaseCreature(BinaryReader br)
            : base(br)
        {
            CreatureSpecific = CreatureSpecific.Read(br);
        }
    }
}
