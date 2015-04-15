using System;

using TNL.NET.Entities;
using TNL.NET.Structs;
using TNL.NET.Types;
using TNL.NET.Utils;

namespace Genesis.Shared.TNL.Ghost
{
    using Constant;
    using Entities.Base;

    public class GhostObject : NetObject
    {
        private static NetClassRepInstance<GhostObject> _dynClassRep;

        protected ClonedObjectBase Parent;
        protected Boolean WaitingForParent;
        protected Single UpdatePriorityScalar;
        protected Object MsgCreate;
        protected Object MsgCreateOwner;

        public TFID Guid { get; private set; }

        public override NetClassRep GetClassRep()
        {
            return _dynClassRep;
        }

        public static void RegisterNetClassReps()
        {
            ImplementNetObject(out _dynClassRep);
        }

        public GhostObject()
        {
            Guid = new TFID();
            WaitingForParent = true;
            UpdatePriorityScalar = 0.1f;
            NetFlags = new BitSet();
            NetFlags.Set((UInt32) NetFlag.Ghostable);
        }

        public Boolean IsGhostVIsibleToMe(NetObject ghost)
        {
            return OwningConnection != null && OwningConnection.GetGhostIndex(ghost) != -1;
        }

        public void SetParent(ClonedObjectBase parent)
        {
            WaitingForParent = false;
            Parent = parent;
        }

        public override bool OnGhostAdd(GhostConnection theConnection)
        {
            if (Parent != null)
                Parent.SetGhost(this);

            return true;
        }

        public TNLConnection GetOwningConnection()
        {
            return OwningConnection as TNLConnection;
        }

        public void CleanupCreate()
        {
            MsgCreate = null;
            MsgCreateOwner = null;
        }

        public override void OnGhostRemove()
        {
            if (Parent != null)
                Parent.ClearGhost(false);
        }

        public virtual void CreatePacket()
        {
            MsgCreate = new Object();
        }

        public virtual void RecreateForExisting()
        {
            if (MsgCreate != null && Parent != null)
            {
                
            }
        }

        public override Single GetUpdatePriority(NetObject scopeObject, UInt64 updateMask, Int32 updateSkips)
        {
            if (Parent == null || !(scopeObject is GhostObject) || (scopeObject as GhostObject).Parent == null)
                return updateSkips * 0.02f;

            var otherParent = (scopeObject as GhostObject).Parent;

            if (otherParent.GetTargetObject() != Parent && otherParent != Parent && otherParent != Parent.Owner && (Parent.GetAsCreature() == null ||
                    (Parent.GetAsCreature().GetSummonOwner() != otherParent.GetTFID())))
            {
                var otherAvPos = otherParent.GetAvatarPosition();
                var thisAvPos = Parent.GetAvatarPosition();

                var val = (Single) Math.Sqrt((otherAvPos.X - thisAvPos.X) * (otherAvPos.X - thisAvPos.X) + (otherAvPos.Y - thisAvPos.Y) * (otherAvPos.Y - thisAvPos.Y));
                return UpdatePriorityScalar *
                        (((1.0F -
                            (val / ((otherParent.GetMap().GetNumberOfTerrainGridsPerObjectGrid() * 100.0F) * 1.2F))) *
                            0.5F) + (updateSkips * 0.001F));
            }

            return 1.0f;
        }

        public void PackCommon(BitStream stream)
        {
            
        }

        public void UnpackCommon(BitStream stream, Object msgCreate)
        {
            
        }

        public void PackSingleSkill(BitStream stream, Object skill, Int32 size, Int32 skillTargetType)
        {
            
        }

        public void UnpackSingleSkill(BitStream stream, Object skill, Int32 size)
        {
            
        }

        public override void UnpackUpdate(GhostConnection connection, BitStream stream)
        {
            
        }

        public override UInt64 PackUpdate(GhostConnection connection, UInt64 updateMask, BitStream stream)
        {
            return 0UL;
        }

        public Int32 GetCreatePacketSize(Int32 cbidObject)
        {
            return 0;
        }

        public void UnpackSkills(BitStream stream, Boolean isOwner)
        {
            
        }

        public Object MallocCreatePacket(Int32 cbidObject, out Int32 size)
        {
            size = 0;
            return null;
        }

        public void PackSkills(BitStream stream, Object pBase)
        {
            
        }

        public static GhostObject CreateObject(TFID id, GhostType type)
        {
            Console.WriteLine("GhostObject::CreateObject");
            GhostObject obj;

            switch (type)
            {
                case GhostType.Creature:
                    obj = new GhostCreature();
                    break;

                case GhostType.Vehicle:
                    obj = new GhostVehicle();
                    break;

                case GhostType.Character:
                    obj = new GhostCharacter();
                    break;

                case GhostType.Object:
                    obj = new GhostObject();
                    break;

                default:
                    throw new ArgumentException("Could not create GhostObject for not existing type!", "type");
            }

            obj.Guid = id;
            return obj;
        }
    }
}
