using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementStunt : ObjectiveRequirement
    {
        public float Height;
        public float Distance;
        public float Time;

        public ObjectiveRequirementStunt(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Stunt;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var height = elem.Element("Height");
            if (height != null && !height.IsEmpty)
                Height = (float)height;

            var distance = elem.Element("MaxEscortDistance");
            if (distance != null && !distance.IsEmpty)
                Distance = (float)distance;

            var time = elem.Element("MaxEscortDistance");
            if (time != null && !time.IsEmpty)
                Time = (float)time;
        }
    }
}
