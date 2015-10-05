using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    class ObjectiveRequirementCollect : ObjectiveRequirement
    {
        public int ItemCBID;
        public int ContinentId;
        public int AllowedType;
        public int AllowedClass;
        public int MinLevel;
        public int MaxLevel;
        public bool TargetIsPlayer;
        public bool TargetIsTemplateVehicle;
        public bool LevelRestriction;
        public bool TakeItems;
        public bool GiveToAllConvoyMembers;
        public int TargetCount;
        public int NumToCollect;
        public float OptionalDropPercent;
        public int[] OptinonalTargets;

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
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var levelMin = elem.Element("ReqireLevelMin");
            if (levelMin != null && !levelMin.IsEmpty)
            {
                MinLevel = (int)levelMin;
                LevelRestriction = true;
            }

            var levelMax = elem.Element("RequireLevelMax");
            if (levelMax != null && !levelMax.IsEmpty)
            {
                MaxLevel = (int)levelMax;
                LevelRestriction = true;
            }

            var allowedClass = elem.Element("AllowedClass");
            if (allowedClass != null && !allowedClass.IsEmpty)
                AllowedClass = (int)allowedClass;

            var allowedType = elem.Element("AllowedType");
            if (allowedType != null && !allowedType.IsEmpty)
                AllowedType = (int)allowedType;

            var cbid = elem.Element("CBID");
            if (cbid != null && !cbid.IsEmpty)
                ItemCBID = (int)cbid;

            var contCBID = elem.Element("ContinentCBID");
            if (contCBID != null && !contCBID.IsEmpty)
                ContinentId = (int)contCBID;

            var tIsTemplVeh = elem.Element("TargetIsTemplateVehicle");
            if (tIsTemplVeh != null && !tIsTemplVeh.IsEmpty)
                TargetIsTemplateVehicle = (int)tIsTemplVeh != 0;

            var tIsPlayer = elem.Element("TargetIsPlayer");
            if (tIsPlayer != null && !tIsPlayer.IsEmpty)
                TargetIsPlayer = (int)tIsPlayer != 0;

            var numToCollect = elem.Element("NumToCollect");
            if (numToCollect != null && !numToCollect.IsEmpty)
                NumToCollect = (int)numToCollect;

            var optionalDropPct = elem.Element("OptionalDropPercent");
            if (optionalDropPct != null && !optionalDropPct.IsEmpty)
                OptionalDropPercent = (float)optionalDropPct;

            var takeItems = elem.Element("TakeAllItems");
            if (takeItems != null && !takeItems.IsEmpty)
                TakeItems = (int)takeItems != 0;

            var giveToConvMems = elem.Element("GiveToAllConvoyMembers");
            if (giveToConvMems != null && !giveToConvMems.IsEmpty)
                GiveToAllConvoyMembers = (int)giveToConvMems != 0;

            foreach (var el in elem.Elements("OptionalTargetCBID"))
                if (TargetCount < 10)
                    OptinonalTargets[TargetCount++] = (int)el;
        }
    }
}
