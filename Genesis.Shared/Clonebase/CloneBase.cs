using System.IO;

namespace Genesis.Shared.Clonebase
{
    using Constant;
    using Structures.Specifics;

    public class CloneBase
    {
        public CloneBaseSpecific CloneBaseSpecific;

        public CloneBase(BinaryReader br)
        {
            CloneBaseSpecific = CloneBaseSpecific.Read(br);
        }

        public ObjectType Type { get { return (ObjectType)CloneBaseSpecific.Type; } }
    }
}
