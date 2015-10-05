using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public class CreatureSpecific
    {
        public int AIBehavior;
        public short AttributeCombat;
        public short AttributePerception;
        public short AttributeTech;
        public short AttributeTheory;
        public short BaseLevel;
        public byte BaseLootChance;
        public byte BossType;
        public int Color1;
        public int Color2;
        public int Color3;
        public short DefensiveBonus;
        public short DifficultyAdjust;
        public byte Flags;
        public float FlyingHeight;
        public int HasTurret;
        public float HearingRange;
        public int IsNPC;
        public int LootTableId;
        public int NPCCanGamble;
        public string NPCIntro;
        public short OffensiveBonus;
        public float PhysicsScale;
        public float RotationSpeed;
        public Dictionary<byte, List<SkillSet>> Skills;
        public float Speed;
        public short TransformTime;
        public float VisionArc;
        public float VisionRange;
        public float XPPercent;

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
                Skills = new Dictionary<byte, List<SkillSet>>()
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
