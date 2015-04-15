using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementDeliver : ObjectiveRequirement
    {
        public Int32 ItemCBID;
        public Int32 NumToDeliver;
        public Int32 NPCTargetCBID;
        public Int32 NPCContinentId;
        public Boolean GiveItemOnStart;
        public Boolean TakeItemAtEnd;
        public Boolean NPCTargetCompletes;
        public Boolean RequireItemToComplete;

        public ObjectiveRequirementDeliver(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Deliver;

            GiveItemOnStart = true;
            TakeItemAtEnd = true;
            NPCTargetCompletes = true;
            RequireItemToComplete = true;

            ItemCBID = -1;
            NPCTargetCBID = -1;
            NPCContinentId = -1;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var cbid = elem.Element("CBIDItem");
            if (cbid != null && !cbid.IsEmpty)
            {
                ItemCBID = (Int32)cbid;
                RequireItemToComplete = ItemCBID == -1;
            }

            var contCBID = elem.Element("ContinentID");
            if (contCBID != null && !contCBID.IsEmpty)
                NPCContinentId = (Int32)contCBID;

            var numToDeliver = elem.Element("NumToDeliver");
            if (numToDeliver != null && !numToDeliver.IsEmpty)
                NumToDeliver = (Int32)numToDeliver;

            var targetCBID = elem.Element("TargetNPCCBID");
            if (targetCBID != null && !targetCBID.IsEmpty)
                NPCTargetCBID = (Int32)targetCBID;

            var giveItemAtStart = elem.Element("GiveItemAtStart");
            if (giveItemAtStart != null && !giveItemAtStart.IsEmpty)
                GiveItemOnStart = (Int32)giveItemAtStart != 0;

            var takeItemAtEnd = elem.Element("TakeItemAtEnd");
            if (takeItemAtEnd != null && !takeItemAtEnd.IsEmpty)
                TakeItemAtEnd = (Int32)takeItemAtEnd != 0;

            var targetCompletes = elem.Element("NPCTargetCompletes");
            if (targetCompletes != null && !targetCompletes.IsEmpty)
                NPCTargetCompletes = (Int32)targetCompletes != 0;
        }
    }
}
