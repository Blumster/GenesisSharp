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
        public ObjectType Type { get { return CloneBaseObject.Type; } }
        protected Int32 CBID { get { return CloneBaseObject.CloneBaseSpecific.CloneBaseId; } }

        protected Int32 Faction;
        protected UInt32 Bf380;
        protected UInt32 Bf388;
        protected UInt32 LastServerUpdate;
        protected UInt32 TimeOfDeath;
        protected Byte HbOnReceiveHit;
        protected Byte HbOnDoHit;
        protected Byte HbCollision;
        protected Byte HbDeath;
        protected Byte HbKill;
        protected Byte HbStealth;
        protected Byte HbCancelSkills;
        protected Int64[] TriggerEvents;
        protected TFID Murderer;
        protected TFID LastMurderer;
        protected Single DamageByMurderer;
        public Vector3 Position { get; set; }
        public Vector4 Rotation { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 AngularVelocity { get; set; }
        protected SectorMap Map;
        public CloneBaseObject CloneBaseObject;
        public ClonedObjectBase Owner { get; private set; }
        protected UInt32 StatusBitField;
        protected Single Scale;
        protected Single TerrainOffset;
        protected Single GameMass;
        protected Int32 Value;
        protected Int32 CustomValue;
        protected DeathType DeathType;
        protected Single HPSkillScalar;
        protected Int32 HPSkillAdd;
        protected Int32 RequiredLevelPrefixOffset;
        protected Int32 RequiredLevel;
        protected Int32 RequiredCombat;
        protected Int32 RequiredPerception;
        protected Int32 RequiredTech;
        protected Int32 RequiredTheory;
        protected Int32 StoredRequiredLevel;
        protected Int64 CoidCustomizedBy;
        protected Boolean MadeFromMemory;
        protected String CustomizedName;
        protected Int32 DistanceDrawOverride;
        protected Single OverheadOffset;
        protected UInt32 DamageState;
        protected String MangledName;
        protected TFID COID { get; set; }
        protected UInt64 CoidAssignedTo;
        public Byte Layer { get; set; }
        protected UnkFlags UnkFlags;
        protected UInt16 UsesLeft;
        protected GhostObject GhostObject { get; private set; }
        protected ClonedObjectBase TargetObject { get; private set; }

        //asd
        protected Int32 BareTeamFaction;
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

        public void ReadTriggerEvents(BinaryReader br, UInt32 mapVersion)
        {
            TriggerEvents = br.Read<Int64>(3);
        }

        public void SetCOID(Int64 coid, Boolean global = true)
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

        public Int64 GetCOID()
        {
            return COID.Coid;
        }

        public Int32 GetCBID()
        {
            return CBID;
        }

        public UInt16 GetMaxStackSize()
        {
            return CloneBaseObject.Type == ObjectType.QuestObject ? (UInt16)16959 : CloneBaseObject.SimpleObjectSpecific.StackSize;
        }

        public Boolean CanAddQuantitiy(UInt16 quantity)
        {
            return GetQuantity() + quantity <= GetMaxStackSize();
        }

        public Int32 GetMemorizeCraftValue()
        {
            return (Int32)Math.Ceiling(GetSellValue() * 0.85000002D);
        }

        public Int32 GetSellValue()
        {
            if (IsSellable())
            {
                var price = (CloneBaseObject.CloneBaseSpecific.BaseValue + GetModifierValue()) * 0.10000002D;

                if ((UnkFlags & UnkFlags.IsKit) != 0)
                    price *= 0.5D;

                if (price <= 0.0D)
                    price = 1.0D;

                return (Int32)price;
            }

            return 0;
        }

        public Single GetModifierValue()
        {
            return 0.0f; // TODO: Based on Prefixes
        }

        public Int16 GetRequiredLevel()
        {
            var lvl = (Int16)(RequiredLevelPrefixOffset + RequiredLevel); // TODO: MOAR
            if (lvl > 80)
                lvl = 80;

            return lvl;
        }

        public void SetRequiredLevel(Int16 level)
        {
            RequiredLevel = level <= 80 ? level : 80;
        }

        public TFID GetTFIDMurderer()
        {
            return Murderer;
        }

        public Int32 GetOriginalFaction()
        {
            return CloneBaseObject.SimpleObjectSpecific.Faction;
        }

        public void SetDamageByMurderer(Single damage)
        {
            if (Owner != null)
                Owner.SetDamageByMurderer(damage);
            else
                DamageByMurderer = damage;
        }

        public Int64 GetSouvenirValue()
        {
            var val = 3L * GetValue() / 200L;
            if (val < 0L)
                val = 1L;

            return val;
        }

        public Int32 GetApproximateLevelWorth()
        {
            var val = (Double)Value; // TODO: MOAR
            if (val <= 0.0D)
                return 0;

            val = Math.Sqrt(val) * 0.2857143D;
            if (val <= 0.0D)
                return 0;

            return (Int32)val;
        }

        public Int32 GetIDFaction()
        {
            return Owner != null ? Owner.GetIDFaction() : Faction;
        }

        public void SetDirtyState()
        {
            UnkFlags |= UnkFlags.Dirty;

            if (Owner != null)
                Owner.SetDirtyState();
        }

        public Boolean GetIsDirtyState()
        {
            return (UnkFlags & UnkFlags.Dirty) != 0;
        }

        public void ClearDirtyState()
        {
            UnkFlags &= ~UnkFlags.Dirty;
        }

        public Boolean IsPickupable(Boolean checkOwner)
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

        public Boolean IsSellable()
        {
            return CloneBaseObject.CloneBaseSpecific.IsSellable;
        }

        public Boolean CanUseTinkeringKitOnObject(TinkeringKit kit)
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

        public Boolean GetCanBeKit()
        {
            return CloneBaseObject.GetRecipeSize() > 0;
        }

        public Boolean GetCanBeEnhanced()
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

        public void SetIsKit(Boolean kit)
        {
            if (((UnkFlags & UnkFlags.IsKit) != 0) != kit)
                SetDirtyState();

            UnkFlags ^= (UnkFlags)((UInt32)UnkFlags ^ ((kit ? 1U : 0U) << 19)) & UnkFlags.IsKit;

            //if ((_unkFlags & UnkFlags.IsKit) != 0)
            //SetMaximumNumberOfGadgets();
        }

        public void SetIsBound(Boolean bound)
        {
            if (((UnkFlags & UnkFlags.IsBound) != 0) != bound)
                SetDirtyState();

            UnkFlags ^= (UnkFlags)((UInt32)UnkFlags ^ ((bound ? 1U : 0U) << 20)) & UnkFlags.IsBound;
        }

        public Boolean GetIsStackable()
        {
            return CloneBaseObject.Type == ObjectType.QuestObject || ((CloneBaseObject.SimpleObjectSpecific.Flags & 0x40) != 0 && (UnkFlags & UnkFlags.IsKit) == 0);
        }

        public Boolean WillObjectCostumize(ClonedObjectBase other)
        {
            if (other == null)
                return false;

            return false; // TODO: MOAR
        }

        public Int32 GetValue()
        {
            if (Value == -1)
                return 0;

            var val = Value + (Double)GetModifierValue();

            if ((UnkFlags & UnkFlags.IsKit) != 0)
                val = Math.Floor(Value * 0.079999998D);

            if (val <= 0.0D)
                val = 1.0D;

            return (Int32)val;
        }

        public SectorMap GetMap()
        {
            return Map;
        }
    }
}
