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
        protected bool WaitingForParent;
        protected float UpdatePriorityScalar;
        protected object MsgCreate;
        protected object MsgCreateOwner;

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
            NetFlags.Set((uint) NetFlag.Ghostable);
        }

        public bool IsGhostVIsibleToMe(NetObject ghost)
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
            MsgCreate = new object();
        }

        public virtual void RecreateForExisting()
        {
            if (MsgCreate != null && Parent != null)
            {
                
            }
        }

        public override float GetUpdatePriority(NetObject scopeObject, ulong updateMask, int updateSkips)
        {
            if (Parent == null || !(scopeObject is GhostObject) || (scopeObject as GhostObject).Parent == null)
                return updateSkips * 0.02f;

            var otherParent = (scopeObject as GhostObject).Parent;

            if (otherParent.GetTargetObject() != Parent && otherParent != Parent && otherParent != Parent.Owner && (Parent.GetAsCreature() == null ||
                    (Parent.GetAsCreature().GetSummonOwner() != otherParent.GetTFID())))
            {
                var otherAvPos = otherParent.GetAvatarPosition();
                var thisAvPos = Parent.GetAvatarPosition();

                var val = (float) Math.Sqrt((otherAvPos.X - thisAvPos.X) * (otherAvPos.X - thisAvPos.X) + (otherAvPos.Y - thisAvPos.Y) * (otherAvPos.Y - thisAvPos.Y));
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

        public void UnpackCommon(BitStream stream, object msgCreate)
        {
            
        }

        public void PackSingleSkill(BitStream stream, object skill, int size, int skillTargetType)
        {
            
        }

        public void UnpackSingleSkill(BitStream stream, object skill, int size)
        {
            
        }

        public override void UnpackUpdate(GhostConnection connection, BitStream stream)
        {
            
        }

        public override ulong PackUpdate(GhostConnection connection, ulong updateMask, BitStream stream)
        {
            return 0UL;
        }

        public int GetCreatePacketSize(int cbidObject)
        {
            return 0;
        }

        public void UnpackSkills(BitStream stream, bool isOwner)
        {
            
        }

        public object MallocCreatePacket(int cbidObject, out int size)
        {
            size = 0;
            return null;
        }

        public void PackSkills(BitStream stream, object pBase)
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
