using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementMoney : ObjectiveRequirement
    {
        public uint MoneyNeeded;

        public ObjectiveRequirementMoney(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Money;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var moneyNeeded = elem.Element("MoneyNeeded");
            if (moneyNeeded != null && !moneyNeeded.IsEmpty)
                MoneyNeeded = (uint)moneyNeeded;
        }
    }
}
