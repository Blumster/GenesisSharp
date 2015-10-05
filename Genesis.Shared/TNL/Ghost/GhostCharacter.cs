using TNL.NET.Entities;
using TNL.NET.Types;
using TNL.NET.Utils;

namespace Genesis.Shared.TNL.Ghost
{
    public class GhostCharacter : GhostObject
    {
        private static NetClassRepInstance<GhostCharacter> _dynClassRep;

        private object Phantom;
        private float MapScale;
        private int CbidPet;

        public new static void RegisterNetClassReps()
        {
            ImplementNetObject(out _dynClassRep);
        }

        public override NetClassRep GetClassRep()
        {
            return _dynClassRep;
        }

        public GhostCharacter()
        {
            UpdatePriorityScalar = 1.0f;
        }

        public override void CreatePacket()
        {
            
        }

        public override void RecreateForExisting()
        {
            
        }

        public override ulong PackUpdate(GhostConnection connection, ulong updateMask, BitStream stream)
        {
            return 0UL;
        }

        public override void UnpackUpdate(GhostConnection connection, BitStream stream)
        {
            
        }

        public override void PerformScopeQuery(GhostConnection connection)
        {
            
        }
    }
}
