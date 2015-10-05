using System.Collections.Generic;

namespace Genesis.Shared.Entities
{
    using Structures;
    using TNL.Ghost;

    public class Creature : SimpleObject
    {
        #region Declaration
        protected bool Encountered;
        protected bool IsMoving;
        protected bool StoppedToAttack;
        protected bool FixatedOnTarget;
        protected bool Transforming;
        protected bool Falling;
        protected bool CanGiveXPAndLoot;
        protected bool GivesSharedXPAndLoot;
        public bool IsElite { get; }
        protected bool IsUsingVehicle;
        protected float TetherRange;
        protected bool PacketOverride;
        protected bool CanUsePowerDump;
        protected uint StatusEffectInvincibilityBitField;
        protected int[] StatusEffectInvincibility;
        protected int[] StatusEffectCountingMutexCancelable;
        protected int[] StatusEffectCountingMutexNonCancelable;
        protected int TurretBoneId;
        protected int AttackingAnimation;
        protected byte TreasureRolls;
        protected TFID Possessor;
        protected float DrivingTerrain;
        protected float TurretDirection;
        protected float FlyingHeight;
        protected float PreferredAttackRange;
        public int EnhancementId { get; }
        public ushort Mana { get; private set; }
        protected ushort MaxMana;
        protected float ManaModifier;
        protected float ManaSkillScalar;
        protected int ManaSkillAdd;
        public int AttribTech { get; private set; }
        public int AttribCombat { get; private set; }
        public int AttribTheory { get; private set; }
        public int AttribPerception { get; private set; }
        protected int AttribTechModified;
        protected int AttribCombatModified;
        protected int AttribTheoryModified;
        protected int AttribPerceptionModified;
        protected int AngularDirection;
        public uint Level { get; }
        protected int Analyzed;
        protected float[] Accucary;
        protected short[] DamageAddMax;
        protected short[] DamageAddEquippedMax;
        protected short[] DamageAddMin;
        protected short[] DamageAddEquippedMin;
        protected short[] Resists;
        protected List<TFID> SummonedCreature;
        protected List<TFID> SummonedCreatureNoCount;
        protected float CriticalHitOffenseCreature;
        protected float CriticalHitOffenseVehicle;
        protected float CriticalHitDefenseCreature;
        protected float CriticalHitDefenseVehicle;
        protected float RefireRateModifier;
        protected float AggroRadiusModifier;
        protected float CreatureSpeed;
        protected float Boost;
        protected float CreatureTurningRate;
        protected uint AIFlags;
        protected bool ForceSpawned;
        public bool DoesntCountAsSummon { get; }
        protected bool SummonerIsCharacter;
        protected bool Sleeping;
        protected int ActivationCounter;
        public long CurrentVehicleId { get; protected set; }
        public long DynamicOnUseTrigger { get; private set; }
        public long DynamicOnUseReaction { get; private set; }
        protected TFID SummonOwner;
        protected Vector4 MoveToTarget;
        protected float DefensiveBonus;
        protected float OffensiveBonus;
        protected float Penetration;
        protected float Deflection;
        public byte AIState { get; }
        protected float SummonDistance;
        protected float SummonBaseDistance;
        protected byte SummonMode;
        protected bool Wandering;
        protected long CurrentTrailerCoid;
        #endregion Declaration

        public Creature()
        {
            TreasureRolls = 1;
            Possessor = new TFID
            {
                Coid = -1L,
                Global = false
            };
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
            };
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

            Accucary = new float[6];
            DamageAddMax = new short[6];
            DamageAddEquippedMax = new short[6];
            DamageAddMin = new short[6];
            DamageAddEquippedMin = new short[6];
            Resists = new short[6];
            StatusEffectInvincibility = new int[8];
            StatusEffectCountingMutexCancelable = new int[8];
            StatusEffectCountingMutexNonCancelable = new int[8];
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

        public void SetAttribCombat(int combat)
        {
            AttribCombat = combat;
        }

        public void SetAttribPerception(int perception)
        {
            AttribPerception = perception;
        }

        public void SetAttribTech(int tech)
        {
            AttribTech = tech;
        }

        public void SetAttribTheory(int theory)
        {
            AttribTheory = theory;
        }

        public override Character GetSuperCharacter(bool inclSummons)
        {
            return null;
        }

        public override void WriteToCreatePacket(Packet packet, bool extended = false)
        {
            base.WriteToCreatePacket(packet, extended);

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
