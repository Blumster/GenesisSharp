using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementKill : ObjectiveRequirement
    {
        public Boolean TargetIsTemplateVehicle;
        public Boolean TrackDamage;
        public Int32 NumToKill;
        public Boolean NegativeKill;
        public Boolean TargetIsPlayer;
        public Boolean LevelRestriction;
        public Boolean TargetIsFaction;
        public Int32 TargetCBID;
        public Int32 ContinentId;
        public Int32 AllowedType;
        public Int32 AllowedClass;
        public Int32 MinLevel;
        public Int32 MaxLevel;
        public Single MaxEscortDistance;

        public ObjectiveRequirementKill(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Kill;
            TargetCBID = -1;
            ContinentId = -1;
            AllowedType = -1;
            AllowedClass = -1;
            MinLevel = -1;
            MaxLevel = -1;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var levelMin = elem.Element("ReqireLevelMin");
            if (levelMin != null && !levelMin.IsEmpty)
            {
                MinLevel = (Int32)levelMin;
                LevelRestriction = true;
            }

            var levelMax = elem.Element("RequireLevelMax");
            if (levelMax != null && !levelMax.IsEmpty)
            {
                MaxLevel = (Int32)levelMax;
                LevelRestriction = true;
            }

            var allowedClass = elem.Element("AllowedClass");
            if (allowedClass != null && !allowedClass.IsEmpty)
                AllowedClass = (Int32)allowedClass;

            var allowedType = elem.Element("AllowedType");
            if (allowedType != null && !allowedType.IsEmpty)
                AllowedType = (Int32)allowedType;

            var trackDamage = elem.Element("TrackDamage");
            if (trackDamage != null && !trackDamage.IsEmpty)
                TrackDamage = (Int32)trackDamage != 0;

            var tIsTemplVeh = elem.Element("TargetIsTemplateVehicle");
            if (tIsTemplVeh != null && !tIsTemplVeh.IsEmpty)
                TargetIsTemplateVehicle = (Int32)tIsTemplVeh != 0;

            var tIsFaction = elem.Element("TargetIsFaction");
            if (tIsFaction != null && !tIsFaction.IsEmpty)
                TargetIsFaction = (Int32)tIsFaction != 0;

            var tIsPlayer = elem.Element("TargetIsPlayer");
            if (tIsPlayer != null && !tIsPlayer.IsEmpty)
                TargetIsPlayer = (Int32)tIsPlayer != 0;

            var maxEscortDist = elem.Element("MaxEscortDistance");
            if (maxEscortDist != null && !maxEscortDist.IsEmpty)
                MaxEscortDistance = (Single)maxEscortDist;

            var negativeKill = elem.Element("NegativeKill");
            if (negativeKill != null && !negativeKill.IsEmpty)
                NegativeKill = (Int32)negativeKill != 0;

            var numToKill = elem.Element("NumToKill");
            if (numToKill != null && !numToKill.IsEmpty)
                NumToKill = (Int32)numToKill;

            var cbid = elem.Element("CBID");
            if (cbid != null && !cbid.IsEmpty)
                TargetCBID = (Int32)cbid;

            var contCBID = elem.Element("ContinentCBID");
            if (contCBID != null && !contCBID.IsEmpty)
                ContinentId = (Int32)contCBID;
        }
    }
}
