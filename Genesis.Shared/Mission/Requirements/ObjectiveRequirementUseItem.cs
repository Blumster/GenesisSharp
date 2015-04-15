using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementUseItem : ObjectiveRequirement
    {
        public Int64 PrimaryItem;
        public Int32 PrimaryCBID;
        public Boolean PrimaryDestroy;
        public Boolean PrimaryInWorld;
        public String PrimaryUseText;
        public Boolean PrimaryGiveAtStart;
        public Boolean PrimaryMultipleUse;
        public Boolean PrimaryExplode;
        public Int32 PrimaryCompletedItem;
        public Int32 SecondaryCBID;
        public Boolean SecondaryDestroy;
        public Boolean SecondaryGiveAtStart;
        public Boolean SecondaryMultipleUse;
        public Int32 ProgressTime;
        public String ProgressText;
        public Boolean ProgressInterruptable;
        public String ProgressInterruptText;
        public String CompleteText;
        public Int32 CompletedItem;
        public Int32 CompletedMission;
        public Int32 RepeatCount;
        public Int32 ContinentID;

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
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var primItem = elem.Element("PrimaryCOID");
            if (primItem != null && !primItem.IsEmpty)
                PrimaryItem = (Int64)primItem;

            var primCBID = elem.Element("PrimaryCBID");
            if (primCBID != null && !primCBID.IsEmpty)
                PrimaryCBID = (Int32)primCBID;

            var primDestroy = elem.Element("TargetIsPlayer");
            if (primDestroy != null && !primDestroy.IsEmpty)
                PrimaryDestroy = (Int32)primDestroy != 0;

            var primInWorld = elem.Element("TargetIsPlayer");
            if (primInWorld != null && !primInWorld.IsEmpty)
                PrimaryInWorld = (Int32)primInWorld != 0;

            var primUseText = elem.Element("PrimaryUseText");
            if (primUseText != null && !primUseText.IsEmpty)
                PrimaryUseText = (String)primUseText;

            var primGiveStart = elem.Element("PrimaryGiveAtStart");
            if (primGiveStart != null && !primGiveStart.IsEmpty)
                PrimaryGiveAtStart = (Int32)primGiveStart != 0;

            var primMultiUse = elem.Element("PrimaryMultipleUse");
            if (primMultiUse != null && !primMultiUse.IsEmpty)
                PrimaryMultipleUse = (Int32)primMultiUse != 0;

            var primExplode = elem.Element("PrimaryExplode");
            if (primExplode != null && !primExplode.IsEmpty)
                PrimaryExplode = (Int32)primExplode != 0;

            var primCompItem = elem.Element("PrimaryCompletedItem");
            if (primCompItem != null && !primCompItem.IsEmpty)
                PrimaryCompletedItem = (Int32)primCompItem;

            var secItem = elem.Element("SecondaryCBID");
            if (secItem != null && !secItem.IsEmpty)
                SecondaryCBID = (Int32)secItem;

            var secDestroy = elem.Element("SecondaryDestroy");
            if (secDestroy != null && !secDestroy.IsEmpty)
                PrimaryDestroy = (Int32)secDestroy != 0;

            var secGiveStart = elem.Element("SecondaryGiveAtStart");
            if (secGiveStart != null && !secGiveStart.IsEmpty)
                PrimaryDestroy = (Int32)secGiveStart != 0;

            var secMultiUse = elem.Element("SecondaryMultipleUse");
            if (secMultiUse != null && !secMultiUse.IsEmpty)
                PrimaryDestroy = (Int32)secMultiUse != 0;

            var progTime = elem.Element("ProgressTime");
            if (progTime != null && !progTime.IsEmpty)
                ProgressTime = (Int32)progTime;

            var progText = elem.Element("ProgressText");
            if (progText != null && !progText.IsEmpty)
                ProgressText = (String)progText;

            var progInterruptable = elem.Element("ProgressInterruptable");
            if (progInterruptable != null && !progInterruptable.IsEmpty)
                PrimaryDestroy = (Int32)progInterruptable != 0;

            var progInterrText = elem.Element("ProgressInterruptText");
            if (progInterrText != null && !progInterrText.IsEmpty)
                ProgressInterruptText = (String)progInterrText;

            var completeText = elem.Element("CompleteText");
            if (completeText != null && !completeText.IsEmpty)
                CompleteText = (String)completeText;

            var compItem = elem.Element("CompleteItem");
            if (compItem != null && !compItem.IsEmpty)
                CompletedItem = (Int32)compItem;

            var compMission = elem.Element("CompletedMission");
            if (compMission != null && !compMission.IsEmpty)
                CompletedMission = (Int32)compMission;

            var repCount = elem.Element("RepeatCount");
            if (repCount != null && !repCount.IsEmpty)
                RepeatCount = (Int32)repCount;

            var contId = elem.Element("ContinentID");
            if (contId != null && !contId.IsEmpty)
                ContinentID = (Int32)contId;
        }
    }
}
