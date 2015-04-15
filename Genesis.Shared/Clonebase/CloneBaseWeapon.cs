using System.IO;

namespace Genesis.Shared.Clonebase
{
    using Structures.Specifics;

    public class CloneBaseWeapon : CloneBaseObject
    {
        public WeaponSpecific WeaponSpecific;

        public CloneBaseWeapon(BinaryReader br)
            : base(br)
        {
            WeaponSpecific = WeaponSpecific.Read(br);
        }
    }
}
