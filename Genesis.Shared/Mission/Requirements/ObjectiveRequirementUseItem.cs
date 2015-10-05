using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementUseItem : ObjectiveRequirement
    {
        public long PrimaryItem;
        public int PrimaryCBID;
        public bool PrimaryDestroy;
        public bool PrimaryInWorld;
        public string PrimaryUseText;
        public bool PrimaryGiveAtStart;
        public bool PrimaryMultipleUse;
        public bool PrimaryExplode;
        public int PrimaryCompletedItem;
        public int SecondaryCBID;
        public bool SecondaryDestroy;
        public bool SecondaryGiveAtStart;
        public bool SecondaryMultipleUse;
        public int ProgressTime;
        public string ProgressText;
        public bool ProgressInterruptable;
        public string ProgressInterruptText;
        public string CompleteText;
        public int CompletedItem;
        public int CompletedMission;
        public int RepeatCount;
        public int ContinentID;

        public ObjectiveRequirementUseItem(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.UseItem;
            PrimaryItem = -1L;
            PrimaryCBID = -1;
            PrimaryCompletedItem = -1;
            SecondaryCBID = -1;
            CompletedItem = -1;
            CompletedMission = -1;
            RepeatCount = 1;
            ContinentID = -1;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var primItem = elem.Element("PrimaryCOID");
            if (primItem != null && !primItem.IsEmpty)
                PrimaryItem = (long)primItem;

            var primCBID = elem.Element("PrimaryCBID");
            if (primCBID != null && !primCBID.IsEmpty)
                PrimaryCBID = (int)primCBID;

            var primDestroy = elem.Element("TargetIsPlayer");
            if (primDestroy != null && !primDestroy.IsEmpty)
                PrimaryDestroy = (int)primDestroy != 0;

            var primInWorld = elem.Element("TargetIsPlayer");
            if (primInWorld != null && !primInWorld.IsEmpty)
                PrimaryInWorld = (int)primInWorld != 0;

            var primUseText = elem.Element("PrimaryUseText");
            if (primUseText != null && !primUseText.IsEmpty)
                PrimaryUseText = (string)primUseText;

            var primGiveStart = elem.Element("PrimaryGiveAtStart");
            if (primGiveStart != null && !primGiveStart.IsEmpty)
                PrimaryGiveAtStart = (int)primGiveStart != 0;

            var primMultiUse = elem.Element("PrimaryMultipleUse");
            if (primMultiUse != null && !primMultiUse.IsEmpty)
                PrimaryMultipleUse = (int)primMultiUse != 0;

            var primExplode = elem.Element("PrimaryExplode");
            if (primExplode != null && !primExplode.IsEmpty)
                PrimaryExplode = (int)primExplode != 0;

            var primCompItem = elem.Element("PrimaryCompletedItem");
            if (primCompItem != null && !primCompItem.IsEmpty)
                PrimaryCompletedItem = (int)primCompItem;

            var secItem = elem.Element("SecondaryCBID");
            if (secItem != null && !secItem.IsEmpty)
                SecondaryCBID = (int)secItem;

            var secDestroy = elem.Element("SecondaryDestroy");
            if (secDestroy != null && !secDestroy.IsEmpty)
                PrimaryDestroy = (int)secDestroy != 0;

            var secGiveStart = elem.Element("SecondaryGiveAtStart");
            if (secGiveStart != null && !secGiveStart.IsEmpty)
                PrimaryDestroy = (int)secGiveStart != 0;

            var secMultiUse = elem.Element("SecondaryMultipleUse");
            if (secMultiUse != null && !secMultiUse.IsEmpty)
                PrimaryDestroy = (int)secMultiUse != 0;

            var progTime = elem.Element("ProgressTime");
            if (progTime != null && !progTime.IsEmpty)
                ProgressTime = (int)progTime;

            var progText = elem.Element("ProgressText");
            if (progText != null && !progText.IsEmpty)
                ProgressText = (string)progText;

            var progInterruptable = elem.Element("ProgressInterruptable");
            if (progInterruptable != null && !progInterruptable.IsEmpty)
                PrimaryDestroy = (int)progInterruptable != 0;

            var progInterrText = elem.Element("ProgressInterruptText");
            if (progInterrText != null && !progInterrText.IsEmpty)
                ProgressInterruptText = (string)progInterrText;

            var completeText = elem.Element("CompleteText");
            if (completeText != null && !completeText.IsEmpty)
                CompleteText = (string)completeText;

            var compItem = elem.Element("CompleteItem");
            if (compItem != null && !compItem.IsEmpty)
                CompletedItem = (int)compItem;

            var compMission = elem.Element("CompletedMission");
            if (compMission != null && !compMission.IsEmpty)
                CompletedMission = (int)compMission;

            var repCount = elem.Element("RepeatCount");
            if (repCount != null && !repCount.IsEmpty)
                RepeatCount = (int)repCount;

            var contId = elem.Element("ContinentID");
            if (contId != null && !contId.IsEmpty)
                ContinentID = (int)contId;
        }
    }
}
