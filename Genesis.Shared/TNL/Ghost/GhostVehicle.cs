using TNL.NET.Entities;
using TNL.NET.Types;
using TNL.NET.Utils;

namespace Genesis.Shared.TNL.Ghost
{
    public class GhostVehicle : GhostObject
    {
        private static NetClassRepInstance<GhostVehicle> _dynClassRep;

        private byte ArmorFlags;
        private int MaxShields;
        private int CbidPet;
        private int MaxHp;
        private int CurrHp;
        private int Combat;
        private int Perception;
        private int Tech;
        private int Theory;
        private string VehicleName;
        private string ClanName;
        private int ClanId;
        private int ClanRank;

        public override NetClassRep GetClassRep()
        {
            return _dynClassRep;
        }

        public new static void RegisterNetClassReps()
        {
            ImplementNetObject(out _dynClassRep);
        }

        public GhostVehicle()
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

        public void AddEquip(object createMsg, TFID id, int packetSize)
        {
            
        }

        public void AddEquip2(object createMsg, TFID id, int packetSize)
        {
            
        }
    }
}
