using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementCharacterLevel : ObjectiveRequirement
    {
        public int RequiredLevel;
        public int PreviousLevel;

        public ObjectiveRequirementCharacterLevel(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.CharacterLevel;
            PreviousLevel = -1;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var reqLevel = elem.Element("CharacterLevel");
            if (reqLevel != null && !reqLevel.IsEmpty)
                RequiredLevel = (int)reqLevel;
        }
    }
}
