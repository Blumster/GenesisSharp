using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementPatrol : ObjectiveRequirement
    {
        public bool AutoComplete;
        public float AutoCompleteDistance;
        public bool AutoFail;
        public float AutoFailDistance;
        public int ContinentId;
        public int TargetCount;
        public long[] GenericTargets;
        public int Laps;
        public bool Sequential;

        public ObjectiveRequirementPatrol(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Patrol;
            ContinentId = -1;
            Laps = 1;
            Sequential = true;
            GenericTargets = new long[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var autoComp = elem.Element("AutoComplete");
            if (autoComp != null && !autoComp.IsEmpty)
                AutoComplete = (int)autoComp != 0;

            var autoCompDist = elem.Element("AutoCompleteDistance");
            if (autoCompDist != null && !autoCompDist.IsEmpty)
                AutoCompleteDistance = (float)autoCompDist;

            var autoFail = elem.Element("AutoFail");
            if (autoFail != null && !autoFail.IsEmpty)
                AutoFail = (int)autoFail != 0;

            var autoFailDist = elem.Element("AutoFailDistance");
            if (autoFailDist != null && !autoFailDist.IsEmpty)
                AutoFailDistance = (float)autoFailDist;

            var contId = elem.Element("ContinentCBID");
            if (contId != null && !contId.IsEmpty)
                ContinentId = (int)contId;

            var laps = elem.Element("Laps");
            if (laps != null && !laps.IsEmpty)
                Laps = (int)laps;

            foreach (var el in elem.Elements("GenericTargetCOID"))
                if (TargetCount < 10)
                    GenericTargets[TargetCount++] = (long)el;
        }
    }
}
