using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementPatrol : ObjectiveRequirement
    {
        public Boolean AutoComplete;
        public Single AutoCompleteDistance;
        public Boolean AutoFail;
        public Single AutoFailDistance;
        public Int32 ContinentId;
        public Int32 TargetCount;
        public Int64[] GenericTargets;
        public Int32 Laps;
        public Boolean Sequential;

        public ObjectiveRequirementPatrol(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Patrol;
            ContinentId = -1;
            Laps = 1;
            Sequential = true;
            GenericTargets = new Int64[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var autoComp = elem.Element("AutoComplete");
            if (autoComp != null && !autoComp.IsEmpty)
                AutoComplete = (Int32)autoComp != 0;

            var autoCompDist = elem.Element("AutoCompleteDistance");
            if (autoCompDist != null && !autoCompDist.IsEmpty)
                AutoCompleteDistance = (Single)autoCompDist;

            var autoFail = elem.Element("AutoFail");
            if (autoFail != null && !autoFail.IsEmpty)
                AutoFail = (Int32)autoFail != 0;

            var autoFailDist = elem.Element("AutoFailDistance");
            if (autoFailDist != null && !autoFailDist.IsEmpty)
                AutoFailDistance = (Single)autoFailDist;

            var contId = elem.Element("ContinentCBID");
            if (contId != null && !contId.IsEmpty)
                ContinentId = (Int32)contId;

            var laps = elem.Element("Laps");
            if (laps != null && !laps.IsEmpty)
                Laps = (Int32)laps;

            foreach (var el in elem.Elements("GenericTargetCOID"))
                if (TargetCount < 10)
                    GenericTargets[TargetCount++] = (Int64)el;
        }
    }
}
