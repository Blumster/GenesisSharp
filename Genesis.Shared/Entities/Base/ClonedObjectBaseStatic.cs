using System;
using System.Diagnostics;

namespace Genesis.Shared.Entities.Base
{
    using Constant;
    using Manager;

    public abstract partial class ClonedObjectBase
    {
        // Constant datas
        public const Single BaseHP = 60.0f;
        public const Single HPPerTech = 3.0f;
        public static readonly Single[] HPPerLevel = { 9.5f, 8.5f, 8.0f, 8.0f };
        public static readonly Single[] HPRaceMultiplier = { 1.0f, 1.1f, 1.2f };

        public const Single PowerPerTheory = 2.0f;
        public static readonly Single[] PowerPerLevel = { 0.60000002f, 1.0f, 1.0f, 0.75f };

        public const Single HeatPerTech = 0.5f;
        public static readonly Single[] HeatPerLevel = { 1.0f, 1.0f, 1.0f, 1.0f };
        public static readonly Single[] HeatRaceMultiplier = { 1.0f, 1.0f, 1.0f };

        public const Single MaxCombatModeBoost = 0.2f;
        public const Single MaxCombatModeOffense = 0.2f;
        public const Single MaxCombatModeDefense = 0.2f;

        public static readonly Int32[] DisciplinesPerDifficulty = { 4, 2, 1 };
        public static readonly Int32[] DisciplineDifficultyLevel = { 0, 0, 0 };

        public static ClonedObjectBase AllocateNewObjectFromCBID(Int32 cbid)
        {
            var clonebaseobj = AssetManager.AssetContainer.GetCloneBaseObjectForCBID(cbid);
            if (clonebaseobj == null)
                return null;

            switch (clonebaseobj.Type)
            {
                case ObjectType.Reaction:
                    return new Reaction();

                case ObjectType.Trigger:
                    return new Trigger();

                case ObjectType.SpawnPoint:
                    return new SpawnPoint();

                case ObjectType.Store:
                    return new Store();

                case ObjectType.MapPath:
                    return new MapPath();

                case ObjectType.EnterPoint:
                    return new EnterPoint();

                case ObjectType.Outpost:
                    return new OutPost();

                case ObjectType.Character:
                    return new Character();

                case ObjectType.Vehicle:
                    return new Vehicle();

                case ObjectType.PowerPlant:
                    return new PowerPlant();

                case ObjectType.Armor:
                    return new Armor();

                case ObjectType.WheelSet:
                    return new WheelSet();

                case ObjectType.Weapon:
                    return new Weapon();

                case ObjectType.Creature:
                    return new Creature();

                case ObjectType.Object:
                case ObjectType.ObjectGraphicsPhysics:
                    return null;

                case ObjectType.Item:
                case ObjectType.Commodity:
                    return new SimpleObject();

                // Skip cases
                case ObjectType.QuestObject:
                    return null;

                default:
                    Console.WriteLine(clonebaseobj.Type);
                    Debug.Assert(false, "Unreachable code reached!");
                    break;
            }

            return null;
        }

        public static Int32 GetMoneyCBIDFromCredits(Int64 credits)
        {
            if (credits >= 0x3B9ACA00) // Money Orb
                return 2827;

            if (credits >= 0xF4240) // Money Bars
                return 2825;

            if (credits >= 0x3E8) // Money Script
                return 2828;

            if (credits >= 1) // Money Clink
                return 2826;

            return -1;
        }
    }
}
