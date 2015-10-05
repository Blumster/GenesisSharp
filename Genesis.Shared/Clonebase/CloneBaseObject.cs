using System.IO;
using System.Linq;

namespace Genesis.Shared.Clonebase
{
    using Structures.Specifics;

    public class CloneBaseObject : CloneBase
    {
        public SimpleObjectSpecific SimpleObjectSpecific;

        public CloneBaseObject(BinaryReader br)
            : base(br)
        {
            SimpleObjectSpecific = SimpleObjectSpecific.Read(br);
        }

        public int GetRecipeSize()
        {
            return SimpleObjectSpecific.Ingredients.Count(i => i != -1);
        }
    }
}
