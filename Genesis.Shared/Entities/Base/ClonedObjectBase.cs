using System;
using System.IO;

namespace Genesis.Shared.Entities.Base
{
    using Clonebase;
    using Constant;
    using Map;
    using Structures;
    using TNL.Ghost;
    using Utils.Extensions;

    public abstract partial class ClonedObjectBase
    {
        #region Declaration
        public ObjectType Type => CloneBaseObject.Type;
        protected int CBID => CloneBaseObject.CloneBaseSpecific.CloneBaseId;

        protected int Faction;
        protected uint Bf380;
        protected uint Bf388;
        protected uint LastServerUpdate;
        protected uint TimeOfDeath;
        protected byte HbOnReceiveHit;
        protected byte HbOnDoHit;
        protected byte HbCollision;
        protected byte HbDeath;
        protected byte HbKill;
        protected byte HbStealth;
        protected byte HbCancelSkills;
        protected long[] TriggerEvents;
        protected TFID Murderer;
        protected TFID LastMurderer;
        protected float DamageByMurderer;
        public Vector3 Position { get; set; }
        public Vector4 Rotation { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 AngularVelocity { get; set; }
        protected SectorMap Map;
        public CloneBaseObject CloneBaseObject;
        public ClonedObjectBase Owner { get; private set; }
        protected uint StatusBitField;
        protected float Scale;
        protected float TerrainOffset;
        protected float GameMass;
        protected int Value;
        protected int CustomValue;
        protected DeathType DeathType;
        protected float HPSkillScalar;
        protected int HPSkillAdd;
        protected int RequiredLevelPrefixOffset;
        protected int RequiredLevel;
        protected int RequiredCombat;
        protected int RequiredPerception;
        protected int RequiredTech;
        protected int RequiredTheory;
        protected int StoredRequiredLevel;
        protected long CoidCustomizedBy;
        protected bool MadeFromMemory;
        protected string CustomizedName;
        protected int DistanceDrawOverride;
        protected float OverheadOffset;
        protected uint DamageState;
        protected string MangledName;
        protected TFID COID { get; set; }
        protected ulong CoidAssignedTo;
        public byte Layer { get; set; }
        protected UnkFlags UnkFlags;
        protected ushort UsesLeft;
        protected GhostObject GhostObject { get; private set; }
        protected ClonedObjectBase TargetObject { get; private set; }
        protected int BareTeamFaction;
        #endregion

        protected ClonedObjectBase()
        {
            DamageByMurderer = 0.0f;
            Faction = -1;
            LastServerUpdate = 0;
            TimeOfDeath = 0;
            HbOnReceiveHit = 0;
            HbOnDoHit = 0;
            HbCollision = 0;
            HbDeath = 0;
            HbKill = 0;
            HbStealth = 0;
            HbCancelSkills = 0;
            StatusBitField = 0;
            Scale = 1.0f;
            GameMass = 0.0f;
            Value = 0;
            CustomValue = -1;
            DeathType = DeathType.Silent;
            HPSkillScalar = 0.0f;
            HPSkillAdd = 0;
            RequiredLevelPrefixOffset = 0;
            RequiredLevel = -1;
            CoidCustomizedBy = -1L;
            MadeFromMemory = false;
            DistanceDrawOverride = 0;
            OverheadOffset = 0.0f;
            DamageState = 0;
            MangledName = "";
            COID = new TFID
            {
                Coid = -1L,
                Global = false
            };
            Bf388 &= 0xFFFFFFF0U;
            Bf380 = (Bf380 & 0xFFE22210U) | 0x22210U;
            Position = new Vector3();
            Rotation = new Vector4();
            Velocity = new Vector3();
            AngularVelocity = new Vector3();

            SetDirtyState();

            TriggerEvents = new[] { -1L, -1L, -1L };

            TerrainOffset = 0.0f;
            Murderer = new TFID
            {
                Coid = -1L,
                Global = false
            };
        }

        public void SetGhost(GhostObject ghost)
        {
            GhostObject = ghost;
        }

        public void ReadTriggerEvents(BinaryReader br, uint mapVersion)
        {
            TriggerEvents = br.Read<long>(3);
        }

        public void SetCOID(long coid, bool global = true)
        {
            COID = new TFID
            {
                Coid = coid,
                Global = global
            };
        }

        public TFID GetTFID()
        {
            return COID;
        }

        public long GetCOID()
        {
            return COID.Coid;
        }

        public int GetCBID()
        {
            return CBID;
        }

        public ushort GetMaxStackSize()
        {
            return CloneBaseObject.Type == ObjectType.QuestObject ? (ushort)16959 : CloneBaseObject.SimpleObjectSpecific.StackSize;
        }

        public bool CanAddQuantitiy(ushort quantity)
        {
            return GetQuantity() + quantity <= GetMaxStackSize();
        }

        public int GetMemorizeCraftValue()
        {
            return (int)Math.Ceiling(GetSellValue() * 0.85000002D);
        }

        public int GetSellValue()
        {
            if (IsSellable())
            {
                var price = (CloneBaseObject.CloneBaseSpecific.BaseValue + GetModifierValue()) * 0.10000002D;

                if ((UnkFlags & UnkFlags.IsKit) != 0)
                    price *= 0.5D;

                if (price <= 0.0D)
                    price = 1.0D;

                return (int)price;
            }

            return 0;
        }

        public float GetModifierValue()
        {
            return 0.0f; // TODO: Based on Prefixes
        }

        public short GetRequiredLevel()
        {
            var lvl = (short)(RequiredLevelPrefixOffset + RequiredLevel); // TODO: MOAR
            if (lvl > 80)
                lvl = 80;

            return lvl;
        }

        public void SetRequiredLevel(short level)
        {
            RequiredLevel = level <= 80 ? level : 80;
        }

        public TFID GetTFIDMurderer()
        {
            return Murderer;
        }

        public int GetOriginalFaction()
        {
            return CloneBaseObject.SimpleObjectSpecific.Faction;
        }

        public void SetDamageByMurderer(float damage)
        {
            if (Owner != null)
                Owner.SetDamageByMurderer(damage);
            else
                DamageByMurderer = damage;
        }

        public long GetSouvenirValue()
        {
            var val = 3L * GetValue() / 200L;
            if (val < 0L)
                val = 1L;

            return val;
        }

        public int GetApproximateLevelWorth()
        {
            var val = (double)Value; // TODO: MOAR
            if (val <= 0.0D)
                return 0;

            val = Math.Sqrt(val) * 0.2857143D;
            if (val <= 0.0D)
                return 0;

            return (int)val;
        }

        public int GetIDFaction()
        {
            return Owner?.GetIDFaction() ?? Faction;
        }

        public void SetDirtyState()
        {
            UnkFlags |= UnkFlags.Dirty;

            Owner?.SetDirtyState();
        }

        public bool GetIsDirtyState()
        {
            return (UnkFlags & UnkFlags.Dirty) != 0;
        }

        public void ClearDirtyState()
        {
            UnkFlags &= ~UnkFlags.Dirty;
        }

        public bool IsPickupable(bool checkOwner)
        {
            if (checkOwner && Owner != null)
                return false;

            switch (Type)
            {
                case ObjectType.Object:
                    return (CloneBaseObject.SimpleObjectSpecific.Flags & 0x80) != 0;

                case ObjectType.QuestObject:
                case ObjectType.Item:
                case ObjectType.Gadget:
                case ObjectType.PowerPlant:
                case ObjectType.Weapon:
                case ObjectType.WheelSet:
                case ObjectType.Commodity:
                case ObjectType.Armor:
                case ObjectType.TinkeringKit:
                case ObjectType.Accessory:
                case ObjectType.Money:
                    return true;

                case ObjectType.Vehicle:
                    return false; // TODO: asdasdasd, m_bIsInverntory?

                default:
                    return false;
            }
        }

        public bool IsSellable()
        {
            return CloneBaseObject.CloneBaseSpecific.IsSellable;
        }

        public bool CanUseTinkeringKitOnObject(TinkeringKit kit)
        {
            if (kit.Type == CloneBaseObject.Type)
                return true;

            if (kit.Type == ObjectType.Ornament)
                return CloneBaseObject.SimpleObjectSpecific.SubType == 10;

            if (kit.Type == ObjectType.RaceItem)
                return CloneBaseObject.SimpleObjectSpecific.SubType == 11;

            return false;
        }

        public DeathType GetDeathType()
        {
            return DeathType;
        }

        public bool GetCanBeKit()
        {
            return CloneBaseObject.GetRecipeSize() > 0;
        }

        public bool GetCanBeEnhanced()
        {
            switch (CloneBaseObject.Type)
            {
                case ObjectType.PowerPlant:
                case ObjectType.Weapon:
                case ObjectType.Vehicle:
                case ObjectType.WheelSet:
                case ObjectType.Armor:
                    return true;

                case ObjectType.Item:
                    return CloneBaseObject.SimpleObjectSpecific.SubType == 10 ||
                           CloneBaseObject.SimpleObjectSpecific.SubType == 11;

                default:
                    return false;
            }
        }

        public void SetIsKit(bool kit)
        {
            if (((UnkFlags & UnkFlags.IsKit) != 0) != kit)
                SetDirtyState();

            UnkFlags ^= (UnkFlags)((uint)UnkFlags ^ ((kit ? 1U : 0U) << 19)) & UnkFlags.IsKit;

            //if ((_unkFlags & UnkFlags.IsKit) != 0)
            //SetMaximumNumberOfGadgets();
        }

        public void SetIsBound(bool bound)
        {
            if (((UnkFlags & UnkFlags.IsBound) != 0) != bound)
                SetDirtyState();

            UnkFlags ^= (UnkFlags)((uint)UnkFlags ^ ((bound ? 1U : 0U) << 20)) & UnkFlags.IsBound;
        }

        public bool GetIsStackable()
        {
            return CloneBaseObject.Type == ObjectType.QuestObject || ((CloneBaseObject.SimpleObjectSpecific.Flags & 0x40) != 0 && (UnkFlags & UnkFlags.IsKit) == 0);
        }

        public bool WillObjectCostumize(ClonedObjectBase other)
        {
            if (other == null)
                return false;

            return false; // TODO: MOAR
        }

        public int GetValue()
        {
            if (Value == -1)
                return 0;

            var val = Value + (double)GetModifierValue();

            if ((UnkFlags & UnkFlags.IsKit) != 0)
                val = Math.Floor(Value * 0.079999998D);

            if (val <= 0.0D)
                val = 1.0D;

            return (int)val;
        }

        public SectorMap GetMap()
        {
            return Map;
        }
    }
}
