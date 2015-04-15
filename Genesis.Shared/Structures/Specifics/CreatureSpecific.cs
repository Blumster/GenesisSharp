using System;
using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public class CreatureSpecific
    {
        public Int32 AIBehavior;
        public Int16 AttributeCombat;
        public Int16 AttributePerception;
        public Int16 AttributeTech;
        public Int16 AttributeTheory;
        public Int16 BaseLevel;
        public Byte BaseLootChance;
        public Byte BossType;
        public Int32 Color1;
        public Int32 Color2;
        public Int32 Color3;
        public Int16 DefensiveBonus;
        public Int16 DifficultyAdjust;
        public Byte Flags;
        public Single FlyingHeight;
        public Int32 HasTurret;
        public Single HearingRange;
        public Int32 IsNPC;
        public Int32 LootTableId;
        public Int32 NPCCanGamble;
        public String NPCIntro;
        public Int16 OffensiveBonus;
        public Single PhysicsScale;
        public Single RotationSpeed;
        public Dictionary<Byte, List<SkillSet>> Skills;
        public Single Speed;
        public Int16 TransformTime;
        public Single VisionArc;
        public Single VisionRange;
        public Single XPPercent;

        public static CreatureSpecific Read(BinaryReader br)
        {
            var c = new CreatureSpecific
            {
                Speed = br.ReadSingle(),
                VisionArc = br.ReadSingle(),
                VisionRange = br.ReadSingle(),
                HearingRange = br.ReadSingle(),
                RotationSpeed = br.ReadSingle(),
                PhysicsScale = br.ReadSingle(),
                FlyingHeight = br.ReadSingle(),
                AIBehavior = br.ReadInt32(),
                IsNPC = br.ReadInt32(),
                NPCCanGamble = br.ReadInt32(),
                HasTurret = br.ReadInt32(),
                TransformTime = br.ReadInt16(),
                BaseLevel = br.ReadInt16(),
                AttributeCombat = br.ReadInt16(),
                AttributeTech = br.ReadInt16(),
                AttributeTheory = br.ReadInt16(),
                AttributePerception = br.ReadInt16(),
                Flags = br.ReadByte(),
                BossType = br.ReadByte(),
                DifficultyAdjust = br.ReadInt16(),
                BaseLootChance = br.ReadByte(),
                Skills = new Dictionary<Byte, List<SkillSet>>()
            };

            br.ReadBytes(3);

            c.XPPercent = br.ReadSingle();
            c.Color1 = br.ReadInt32();
            c.Color2 = br.ReadInt32();
            c.Color3 = br.ReadInt32();
            c.OffensiveBonus = br.ReadInt16();
            c.DefensiveBonus = br.ReadInt16();
            c.LootTableId = br.ReadInt32();

            var asd = br.ReadInt32();
            //if (asd != 0)
            //Debugger.Break();

            var introLen = br.ReadInt32();
            c.NPCIntro = br.ReadUnicodeString(introLen);

            var aiCount = br.ReadInt32();
            for (var i = 0; i < aiCount; ++i)
            {
                var b = br.ReadByte();

                c.Skills.Add(b, new List<SkillSet>(br.ReadInt32()));

                for (var j = 0; j < c.Skills[b].Capacity; ++j)
                    c.Skills[b].Add(SkillSet.Read(br));
            }
            return c;
        }
    }
}
