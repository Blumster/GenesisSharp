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
        public virtual void InitializeFromCBID(int cbid, SectorMap map)
        {
            CloneBaseObject = AssetManager.AssetContainer.GetCloneBaseObjectForCBID(cbid);

            InitializeBaseData();
            InitializeSkillTree();
            SetMap(map);
        }

        public virtual int GetQuantity()
        {
            return 0;
        }

        public virtual uint GetAvatarCurrentHP()
        {
            return GetCurrentHP();
        }

        public virtual uint GetAvatarMaximumHP()
        {
            return GetMaximumHP();
        }

        public virtual uint GetCurrentHP()
        {
            return 0;
        }

        public virtual uint GetMaximumHP()
        {
            return 0;
        }

        public virtual void SetMaximumHP(uint maximumHP)
        {

        }

        public virtual void SetCurrentHP(uint currentHP)
        {

        }

        public virtual void AdjustHPSkillScalar(float scalar)
        {
            HPSkillScalar += scalar;
        }

        public virtual void AdjustHPSkillAdd(int hp)
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

        public virtual void SetGhosted(bool ghosted)
        {
            Bf380 ^= (Bf380 ^ 8U * (ghosted ? 1U : 0U)) & 8U;
        }

        public virtual short GetArmor(int damageType)
        {
            Debug.Assert(damageType < 6);
            return CloneBaseObject.SimpleObjectSpecific.DamageArmor.Damage[damageType];
        }

        public virtual long CalculateTotalValue()
        {
            return Value;
        }

        public virtual float CalculateWorth()
        {
            return 1.0f;
        }

        public virtual bool GetIsCorpse()
        {
            return (UnkFlags & UnkFlags.IsCorpse) != 0;
        }

        public virtual bool HasOnKill()
        {
            return HbKill > 0;
        }

        public virtual void InitializeFromGlobalCOID(long coid)
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

        public virtual void SetIsInvincible(bool invincible)
        {
            UnkFlags ^= (UnkFlags)((uint)UnkFlags ^ ((invincible ? 1U : 0U) << 10)) & UnkFlags.IsInvincible;
        }

        public virtual void SetIDFaction(int faction)
        {
            Faction = faction;

            if (Owner != null)
                Owner.SetIDFaction(faction);
        }

        public virtual bool GetIsIncapacitated()
        {
            return Owner != null ? Owner.GetIsIncapacitated() : GetIsCorpse();
        }

        public virtual void InitializeBaseData()
        {
            Value = CloneBaseObject.CloneBaseSpecific.BaseValue;
            GameMass = CloneBaseObject.SimpleObjectSpecific.Mass;
            UnkFlags ^= (UnkFlags ^ (UnkFlags)(((uint)UnkFlags >> 12) << 10)) & UnkFlags.IsInvincible;
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

        public virtual Character GetSuperCharacter(bool inclSummons)
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

        public virtual void SetScale(float scale)
        {
            Scale = scale;
        }

        public virtual void SetMap(SectorMap map)
        {
            Map = map;
        }

        public virtual int GetBareTeamFaction()
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

        public virtual void SetBareTeamFaction(int faction)
        {
            BareTeamFaction = faction;
        }

        public virtual ushort GetPrefix(uint position)
        {
            return 0;
        }

        public virtual float DeriveOffensiveModifier(ClonedObjectBase target)
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

        public virtual void ClearGhost(bool a)
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
