using System;

using TNL.NET.Entities;
using TNL.NET.Types;
using TNL.NET.Utils;

namespace Genesis.Shared.TNL.Ghost
{
    public class GhostVehicle : GhostObject
    {
        private static NetClassRepInstance<GhostVehicle> _dynClassRep;

        private Byte ArmorFlags;
        private Int32 MaxShields;
        private Int32 CbidPet;
        private Int32 MaxHp;
        private Int32 CurrHp;
        private Int32 Combat;
        private Int32 Perception;
        private Int32 Tech;
        private Int32 Theory;
        private String VehicleName;
        private String ClanName;
        private Int32 ClanId;
        private Int32 ClanRank;

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

        public override UInt64 PackUpdate(GhostConnection connection, UInt64 updateMask, BitStream stream)
        {
            return 0UL;
        }

        public override void UnpackUpdate(GhostConnection connection, BitStream stream)
        {
            
        }

        public void AddEquip(Object createMsg, TFID id, Int32 packetSize)
        {
            
        }

        public void AddEquip2(Object createMsg, TFID id, Int32 packetSize)
        {
            
        }
    }
}
