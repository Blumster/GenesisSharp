using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementStunt : ObjectiveRequirement
    {
        public Single Height;
        public Single Distance;
        public Single Time;

        public ObjectiveRequirementStunt(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Stunt;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var height = elem.Element("Height");
            if (height != null && !height.IsEmpty)
                Height = (Single)height;

            var distance = elem.Element("MaxEscortDistance");
            if (distance != null && !distance.IsEmpty)
                Distance = (Single)distance;

            var time = elem.Element("MaxEscortDistance");
            if (time != null && !time.IsEmpty)
                Time = (Single)time;
        }
    }
}
