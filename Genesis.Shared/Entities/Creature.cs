using System;
using System.Collections.Generic;

namespace Genesis.Shared.Entities
{
    using Structures;
    using TNL.Ghost;

    public class Creature : SimpleObject
    {
        #region Declaration
        protected Boolean Encountered;
        protected Boolean IsMoving;
        protected Boolean StoppedToAttack;
        protected Boolean FixatedOnTarget;
        protected Boolean Transforming;
        protected Boolean Falling;
        protected Boolean CanGiveXPAndLoot;
        protected Boolean GivesSharedXPAndLoot;
        public Boolean IsElite { get; private set; }
        protected Boolean IsUsingVehicle;
        protected Single TetherRange;
        protected Boolean PacketOverride;
        protected Boolean CanUsePowerDump;
        protected UInt32 StatusEffectInvincibilityBitField;
        protected Int32[] StatusEffectInvincibility;
        protected Int32[] StatusEffectCountingMutexCancelable;
        protected Int32[] StatusEffectCountingMutexNonCancelable;
        protected Int32 TurretBoneId;
        protected Int32 AttackingAnimation;
        protected Byte TreasureRolls;
        protected TFID Possessor;
        protected Single DrivingTerrain;
        protected Single TurretDirection;
        protected Single FlyingHeight;
        protected Single PreferredAttackRange;
        public Int32 EnhancementId { get; private set; }
        public UInt16 Mana { get; private set; }
        protected UInt16 MaxMana;
        protected Single ManaModifier;
        protected Single ManaSkillScalar;
        protected Int32 ManaSkillAdd;
        public Int32 AttribTech { get; private set; }
        public Int32 AttribCombat { get; private set; }
        public Int32 AttribTheory { get; private set; }
        public Int32 AttribPerception { get; private set; }
        protected Int32 AttribTechModified;
        protected Int32 AttribCombatModified;
        protected Int32 AttribTheoryModified;
        protected Int32 AttribPerceptionModified;
        protected Int32 AngularDirection;
        public UInt32 Level { get; private set; }
        protected Int32 Analyzed;
        protected Single[] Accucary;
        protected Int16[] DamageAddMax;
        protected Int16[] DamageAddEquippedMax;
        protected Int16[] DamageAddMin;
        protected Int16[] DamageAddEquippedMin;
        protected Int16[] Resists;
        protected List<TFID> SummonedCreature;
        protected List<TFID> SummonedCreatureNoCount;
        protected Single CriticalHitOffenseCreature;
        protected Single CriticalHitOffenseVehicle;
        protected Single CriticalHitDefenseCreature;
        protected Single CriticalHitDefenseVehicle;
        protected Single RefireRateModifier;
        protected Single AggroRadiusModifier;
        protected Single CreatureSpeed;
        protected Single Boost;
        protected Single CreatureTurningRate;
        protected UInt32 AIFlags;
        protected Boolean ForceSpawned;
        public Boolean DoesntCountAsSummon { get; private set; }
        protected Boolean SummonerIsCharacter;
        protected Boolean Sleeping;
        protected Int32 ActivationCounter;
        public Int64 CurrentVehicleId { get; protected set; }
        public Int64 DynamicOnUseTrigger { get; private set; }
        public Int64 DynamicOnUseReaction { get; private set; }
        protected TFID SummonOwner;
        protected Vector4 MoveToTarget;
        protected Single DefensiveBonus;
        protected Single OffensiveBonus;
        protected Single Penetration;
        protected Single Deflection;
        public Byte AIState { get; private set; }
        protected Single SummonDistance;
        protected Single SummonBaseDistance;
        protected Byte SummonMode;
        protected Boolean Wandering;
        protected Int64 CurrentTrailerCoid;
        #endregion Declaration

        public Creature()
        {
            TreasureRolls = 1;
            Possessor = new TFID
            {
                Coid = -1L,
                Global = false
            }; ;
            AttribTech = 1;
            AttribCombat = 1;
            AttribTheory = 1;
            AttribPerception = 1;
            PreferredAttackRange = 15.0f;
            DrivingTerrain = 0.0f;
            FlyingHeight = 0.0f;
            EnhancementId = -1;
            Mana = 0;
            MaxMana = 0;
            ManaModifier = 1.0f;
            ManaSkillScalar = 0.0f;
            ManaSkillAdd = 0;
            AttribTechModified = 0;
            AttribCombatModified = 0;
            AttribTheoryModified = 0;
            AttribPerceptionModified = 0;
            AngularDirection = 0;
            Level = 0;
            Analyzed = 0;
            CriticalHitOffenseCreature = 0.0f;
            CriticalHitOffenseVehicle = 0.0f;
            CriticalHitDefenseCreature = 0.0f;
            CriticalHitDefenseVehicle = 0.0f;
            RefireRateModifier = 1.0f;
            AggroRadiusModifier = 0.0f;
            Boost = 0.0f;
            AIFlags = 0;
            ForceSpawned = false;
            DoesntCountAsSummon = false;
            SummonerIsCharacter = false;
            Sleeping = false;
            ActivationCounter = 0;
            CurrentVehicleId = -1L;
            DynamicOnUseTrigger = -1L;
            DynamicOnUseReaction = -1L;
            SummonOwner = new TFID
            {
                Coid = -1L,
                Global = false
            }; ;
            DefensiveBonus = 0;
            OffensiveBonus = 0;
            Penetration = 0;
            Deflection = 0;
            AIState = 0;
            SummonDistance = 50.0f;
            SummonBaseDistance = 50.0f;
            SummonMode = 0;
            Wandering = false;
            IsMoving = false;
            StoppedToAttack = false;
            FixatedOnTarget = false;
            Transforming = false;
            Falling = false;
            CanGiveXPAndLoot = true;
            GivesSharedXPAndLoot = false;
            IsElite = false;
            IsUsingVehicle = false;
            TetherRange = 120.0f;
            PacketOverride = false;
            CanUsePowerDump = false;
            StatusEffectInvincibilityBitField = 0;
            MoveToTarget.X = 0.0f;
            MoveToTarget.Y = 0.0f;
            MoveToTarget.Z = 0.0f;
            MoveToTarget.W = 0.0f;

            Accucary = new Single[6];
            DamageAddMax = new Int16[6];
            DamageAddEquippedMax = new Int16[6];
            DamageAddMin = new Int16[6];
            DamageAddEquippedMin = new Int16[6];
            Resists = new Int16[6];
            StatusEffectInvincibility = new Int32[8];
            StatusEffectCountingMutexCancelable = new Int32[8];
            StatusEffectCountingMutexNonCancelable = new Int32[8];
            AttackingAnimation = -1;
            TurretDirection = 0.0f;
            Encountered = false;
            CurrentTrailerCoid = -1L;
        }

        public TFID GetSummonOwner()
        {
            return SummonOwner;
        }

        public override Creature GetAsCreature()
        {
            return this;
        }

        public void SetAttribCombat(Int32 combat)
        {
            AttribCombat = combat;
        }

        public void SetAttribPerception(Int32 perception)
        {
            AttribPerception = perception;
        }

        public void SetAttribTech(Int32 tech)
        {
            AttribTech = tech;
        }

        public void SetAttribTheory(Int32 theory)
        {
            AttribTheory = theory;
        }

        public override Character GetSuperCharacter(Boolean inclSummons)
        {
            return null;
        }

        public override void WriteToCreatePacket(Packet packet, Boolean extended = false)
        {
            base.WriteToCreatePacket(packet);

            if (extended || this is Character) // only chars and vehicles can be extended
                return;

            packet.WriteInteger(EnhancementId);

            packet.WritePadding(4);

            packet.WriteTFID(SummonOwner);
            packet.WriteBoolean(DoesntCountAsSummon);

            packet.WritePadding(7);

            packet.WriteLong(CurrentVehicleId);
            packet.WriteLong(CurrentTrailerCoid);
            packet.WriteInteger(-1); // current spawn owner
            packet.WriteByte(0); // number skills

            packet.WritePadding(3);

            packet.WriteInteger(Analyzed);
            packet.WriteInteger(Level);
            packet.WriteInteger(0); // current path id
            packet.WriteInteger(0); // extra path id
            packet.WriteSingle(0.0f); // patrol distance
            packet.WriteBoolean(false); // path is reversing
            packet.WriteBoolean(false); // path is road
            packet.WriteBoolean(IsElite); // iselite
            packet.WriteByte(AIState); // ai state
            packet.WriteInteger(0); // on use trigger
            packet.WriteInteger(0); // on use reaction
            packet.WriteLong(0); // murderer coid

            for (var i = 0; i < 255; ++i)
            {
                packet.WriteInteger(0); // skill id
                packet.WriteShort(0); // skill level
                packet.WritePadding(2);
            }
        }

        public override void CreateGhost()
        {
            SetGhosted(false);

            var g = new GhostCreature();
            g.SetParent(this);
            SetGhost(g);

            //GhostingManager.Instance.CreateGhost(this, g, EnumGhostType.GCreature);

            SetGhosted(true);
        }

        public void UpdateVehicleStatus()
        {

        }
    }
}
