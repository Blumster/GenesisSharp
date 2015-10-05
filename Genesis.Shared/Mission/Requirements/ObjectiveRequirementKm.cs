using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementKm : ObjectiveRequirement
    {
        public float DistanceNeeded;
        public KmMode Mode;

        public ObjectiveRequirementKm(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Km;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var distNeeded = elem.Element("DistanceNeeded");
            if (distNeeded != null && !distNeeded.IsEmpty)
                DistanceNeeded = (float)distNeeded;

            var kmMode = elem.Element("Mode");
            if (kmMode != null && !kmMode.IsEmpty)
                Mode = (KmMode)(int)kmMode;
        }
    }
}
