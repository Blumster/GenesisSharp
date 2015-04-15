using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementKm : ObjectiveRequirement
    {
        public Single DistanceNeeded;
        public KmMode Mode;

        public ObjectiveRequirementKm(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Km;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var distNeeded = elem.Element("DistanceNeeded");
            if (distNeeded != null && !distNeeded.IsEmpty)
                DistanceNeeded = (Single)distNeeded;

            var kmMode = elem.Element("Mode");
            if (kmMode != null && !kmMode.IsEmpty)
                Mode = (KmMode)(Int32)kmMode;
        }
    }
}
