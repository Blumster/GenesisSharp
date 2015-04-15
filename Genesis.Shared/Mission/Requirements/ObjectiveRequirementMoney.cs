using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementMoney : ObjectiveRequirement
    {
        public UInt32 MoneyNeeded;

        public ObjectiveRequirementMoney(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Money;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var moneyNeeded = elem.Element("MoneyNeeded");
            if (moneyNeeded != null && !moneyNeeded.IsEmpty)
                MoneyNeeded = (UInt32)moneyNeeded;
        }
    }
}
