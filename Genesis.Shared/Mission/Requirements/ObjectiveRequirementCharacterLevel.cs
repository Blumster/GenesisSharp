using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementCharacterLevel : ObjectiveRequirement
    {
        public Int32 RequiredLevel;
        public Int32 PreviousLevel;

        public ObjectiveRequirementCharacterLevel(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.CharacterLevel;
            PreviousLevel = -1;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var reqLevel = elem.Element("CharacterLevel");
            if (reqLevel != null && !reqLevel.IsEmpty)
                RequiredLevel = (Int32)reqLevel;
        }
    }
}
