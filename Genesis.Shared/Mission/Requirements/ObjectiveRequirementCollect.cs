using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    class ObjectiveRequirementCollect : ObjectiveRequirement
    {
        public Int32 ItemCBID;
        public Int32 ContinentId;
        public Int32 AllowedType;
        public Int32 AllowedClass;
        public Int32 MinLevel;
        public Int32 MaxLevel;
        public Boolean TargetIsPlayer;
        public Boolean TargetIsTemplateVehicle;
        public Boolean LevelRestriction;
        public Boolean TakeItems;
        public Boolean GiveToAllConvoyMembers;
        public Int32 TargetCount;
        public Int32 NumToCollect;
        public Single OptionalDropPercent;
        public Int32[] OptinonalTargets;

        public ObjectiveRequirementCollect(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Collect;
            TargetCount = 0;
            ItemCBID = -1;
            ContinentId = -1;
            AllowedType = -1;
            AllowedClass = -1;
            MinLevel = -1;
            MaxLevel = -1;
            OptinonalTargets = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
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

            var cbid = elem.Element("CBID");
            if (cbid != null && !cbid.IsEmpty)
                ItemCBID = (Int32)cbid;

            var contCBID = elem.Element("ContinentCBID");
            if (contCBID != null && !contCBID.IsEmpty)
                ContinentId = (Int32)contCBID;

            var tIsTemplVeh = elem.Element("TargetIsTemplateVehicle");
            if (tIsTemplVeh != null && !tIsTemplVeh.IsEmpty)
                TargetIsTemplateVehicle = (Int32)tIsTemplVeh != 0;

            var tIsPlayer = elem.Element("TargetIsPlayer");
            if (tIsPlayer != null && !tIsPlayer.IsEmpty)
                TargetIsPlayer = (Int32)tIsPlayer != 0;

            var numToCollect = elem.Element("NumToCollect");
            if (numToCollect != null && !numToCollect.IsEmpty)
                NumToCollect = (Int32)numToCollect;

            var optionalDropPct = elem.Element("OptionalDropPercent");
            if (optionalDropPct != null && !optionalDropPct.IsEmpty)
                OptionalDropPercent = (Single)optionalDropPct;

            var takeItems = elem.Element("TakeAllItems");
            if (takeItems != null && !takeItems.IsEmpty)
                TakeItems = (Int32)takeItems != 0;

            var giveToConvMems = elem.Element("GiveToAllConvoyMembers");
            if (giveToConvMems != null && !giveToConvMems.IsEmpty)
                GiveToAllConvoyMembers = (Int32)giveToConvMems != 0;

            foreach (var el in elem.Elements("OptionalTargetCBID"))
                if (TargetCount < 10)
                    OptinonalTargets[TargetCount++] = (Int32)el;
        }
    }
}
