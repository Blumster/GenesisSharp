using System;
using System.Diagnostics;

namespace Genesis.Shared.Entities.Base
{
    using Constant;
    using Manager;
    using Map;
    using Structures;
    using TNL.Ghost;

    public abstract partial class ClonedObjectBase
    {
        public virtual void InitializeFromCBID(Int32 cbid, SectorMap map)
        {
            CloneBaseObject = AssetManager.AssetContainer.GetCloneBaseObjectForCBID(cbid);

            InitializeBaseData();
            InitializeSkillTree();
            SetMap(map);
        }

        public virtual Int32 GetQuantity()
        {
            return 0;
        }

        public virtual UInt32 GetAvatarCurrentHP()
        {
            return GetCurrentHP();
        }

        public virtual UInt32 GetAvatarMaximumHP()
        {
            return GetMaximumHP();
        }

        public virtual UInt32 GetCurrentHP()
        {
            return 0;
        }

        public virtual UInt32 GetMaximumHP()
        {
            return 0;
        }

        public virtual void SetMaximumHP(UInt32 maximumHP)
        {

        }

        public virtual void SetCurrentHP(UInt32 currentHP)
        {

        }

        public virtual void AdjustHPSkillScalar(Single scalar)
        {
            HPSkillScalar += scalar;
        }

        public virtual void AdjustHPSkillAdd(Int32 hp)
        {
            HPSkillAdd += hp;
        }

        public virtual void CreateGhost()
        {
            SetGhosted(false);

            var g = new GhostObject();
            g.SetParent(this);
            SetGhost(g);

            //GhostingManager.Instance.CreateGhost(this, g, EnumGhostType.GObject);

            SetGhosted(true);
        }

        public virtual void SetGhosted(Boolean ghosted)
        {
            Bf380 ^= (Bf380 ^ 8U * (ghosted ? 1U : 0U)) & 8U;
        }

        public virtual Int16 GetArmor(Int32 damageType)
        {
            Debug.Assert(damageType < 6);
            return CloneBaseObject.SimpleObjectSpecific.DamageArmor.Damage[damageType];
        }

        public virtual Int64 CalculateTotalValue()
        {
            return Value;
        }

        public virtual Single CalculateWorth()
        {
            return 1.0f;
        }

        public virtual Boolean GetIsCorpse()
        {
            return (UnkFlags & UnkFlags.IsCorpse) != 0;
        }

        public virtual Boolean HasOnKill()
        {
            return HbKill > 0;
        }

        public virtual void InitializeFromGlobalCOID(Int64 coid)
        {
            // TODO: MOAR
        }

        public virtual void InitializeExtended()
        {
            // TODO: MOAR
        }

        public virtual void StoreVariedValues()
        {
            StoredRequiredLevel = RequiredLevel;
        }

        public virtual void RestoreVariedValues()
        {
            RequiredLevel = StoredRequiredLevel;
        }

        public virtual void SetIsInvincible(Boolean invincible)
        {
            UnkFlags ^= (UnkFlags)((UInt32)UnkFlags ^ ((invincible ? 1U : 0U) << 10)) & UnkFlags.IsInvincible;
        }

        public virtual void SetIDFaction(Int32 faction)
        {
            Faction = faction;

            if (Owner != null)
                Owner.SetIDFaction(faction);
        }

        public virtual Boolean GetIsIncapacitated()
        {
            return Owner != null ? Owner.GetIsIncapacitated() : GetIsCorpse();
        }

        public virtual void InitializeBaseData()
        {
            Value = CloneBaseObject.CloneBaseSpecific.BaseValue;
            GameMass = CloneBaseObject.SimpleObjectSpecific.Mass;
            UnkFlags ^= (UnkFlags ^ (UnkFlags)(((UInt32)UnkFlags >> 12) << 10)) & UnkFlags.IsInvincible;
            RequiredLevel = CloneBaseObject.SimpleObjectSpecific.RequiredLevel;
            RequiredCombat = CloneBaseObject.SimpleObjectSpecific.RequiredCombat;
            RequiredPerception = CloneBaseObject.SimpleObjectSpecific.RequiredPerception;
            RequiredTech = CloneBaseObject.SimpleObjectSpecific.RequiredTech;
            RequiredTheory = CloneBaseObject.SimpleObjectSpecific.RequiredTheory;
            UsesLeft = CloneBaseObject.SimpleObjectSpecific.MaxUses;
        }

        public virtual void SetOwner(ClonedObjectBase owner)
        {
            Owner = owner;

            if (owner == null)
                return;

            Faction = owner.GetIDFaction();
            owner.SetIDFaction(Faction);

            SetBareTeamFaction(owner.GetBareTeamFaction());
        }

        public virtual ClonedObjectBase GetOwner()
        {
            return Owner;
        }

        public virtual Vector4 GetAvatarPosition()
        {
            return default(Vector4);
        }

        public virtual Character GetSuperCharacter(Boolean inclSummons)
        {
            return Owner != null ? Owner.GetSuperCharacter(inclSummons) : null;
        }

        public virtual Creature GetSuperCreature()
        {
            return Owner != null ? Owner.GetSuperCreature() : null;
        }

        public virtual GadgetApplyError CanApplyGadget(Gadget gadget)
        {
            return GadgetApplyError.Success;
        }

        public virtual void ReloadObject()
        {
            //RemovePrefixesAndGadgets();
            InitializeBaseData();
            UnkFlags |= UnkFlags.UnkClean;
            SetDirtyState();
            //AssembleFromPrefixes();
        }

        public virtual ClonedObjectBase CreateClone()
        {
            var cob = AllocateNewObjectFromCBID(CloneBaseObject.CloneBaseSpecific.CloneBaseId);
            if (cob != null)
            {
                cob.InitializeFromCBID(CloneBaseObject.CloneBaseSpecific.CloneBaseId, Map);
                cob.SetScale(Scale);
                cob.Value = Value;
            }

            return cob;
        }

        public virtual void InitializeSkillTree()
        {

        }

        public virtual void SetScale(Single scale)
        {
            Scale = scale;
        }

        public virtual void SetMap(SectorMap map)
        {
            Map = map;
        }

        public virtual Int32 GetBareTeamFaction()
        {
            return BareTeamFaction;
        }

        public virtual void SetTargetObject(ClonedObjectBase target)
        {
            if (target != TargetObject)
            {
                if (GhostObject != null)
                    GhostObject.SetMaskBits(4UL);

                TargetObject = target;
            }
        }

        public virtual ClonedObjectBase GetTargetObject()
        {
            return TargetObject;
        }

        public virtual void SetBareTeamFaction(Int32 faction)
        {
            BareTeamFaction = faction;
        }

        public virtual UInt16 GetPrefix(UInt32 position)
        {
            return 0;
        }

        public virtual Single DeriveOffensiveModifier(ClonedObjectBase target)
        {
            return 1.0f;
        }

        public virtual void Enable()
        {
            UnkFlags |= UnkFlags.UnkEnabled;
        }

        public virtual void Disable()
        {
            UnkFlags &= ~UnkFlags.UnkEnabled;
        }

        public virtual void ClearGhost(Boolean a)
        {
            if (GhostObject != null)
            {
                GhostObject.SetParent(null);
                GhostObject = null;
            }

            LastServerUpdate = 0; // todo
        }

        public virtual SpawnPoint GetAsSpawnPoint()
        {
            return null;
        }

        public virtual void Activate(ClonedObjectBase obj)
        {

        }

        public virtual void NotifyOfImpendingDeletion()
        {

        }

        public virtual Character GetAsCharacter()
        {
            return null;
        }

        public virtual Vehicle GetAsVehicle()
        {
            return null;
        }

        public virtual Armor GetAsArmor()
        {
            return null;
        }

        public virtual PowerPlant GetAsPowerPlant()
        {
            return null;
        }

        public virtual Weapon GetAsWeapon()
        {
            return null;
        }

        public virtual WheelSet GetAsWheelSet()
        {
            return null;
        }

        public virtual SimpleObject GetAsSimpleObject()
        {
            return null;
        }

        public virtual Creature GetAsCreature()
        {
            return null;
        }
    }
}
