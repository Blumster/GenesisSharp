using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementDeliver : ObjectiveRequirement
    {
        public int ItemCBID;
        public int NumToDeliver;
        public int NPCTargetCBID;
        public int NPCContinentId;
        public bool GiveItemOnStart;
        public bool TakeItemAtEnd;
        public bool NPCTargetCompletes;
        public bool RequireItemToComplete;

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
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var cbid = elem.Element("CBIDItem");
            if (cbid != null && !cbid.IsEmpty)
            {
                ItemCBID = (int)cbid;
                RequireItemToComplete = ItemCBID == -1;
            }

            var contCBID = elem.Element("ContinentID");
            if (contCBID != null && !contCBID.IsEmpty)
                NPCContinentId = (int)contCBID;

            var numToDeliver = elem.Element("NumToDeliver");
            if (numToDeliver != null && !numToDeliver.IsEmpty)
                NumToDeliver = (int)numToDeliver;

            var targetCBID = elem.Element("TargetNPCCBID");
            if (targetCBID != null && !targetCBID.IsEmpty)
                NPCTargetCBID = (int)targetCBID;

            var giveItemAtStart = elem.Element("GiveItemAtStart");
            if (giveItemAtStart != null && !giveItemAtStart.IsEmpty)
                GiveItemOnStart = (int)giveItemAtStart != 0;

            var takeItemAtEnd = elem.Element("TakeItemAtEnd");
            if (takeItemAtEnd != null && !takeItemAtEnd.IsEmpty)
                TakeItemAtEnd = (int)takeItemAtEnd != 0;

            var targetCompletes = elem.Element("NPCTargetCompletes");
            if (targetCompletes != null && !targetCompletes.IsEmpty)
                NPCTargetCompletes = (int)targetCompletes != 0;
        }
    }
}
