using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementEscort : ObjectiveRequirement
    {
        public int SkillId;
        public int SkillLevel;
        public bool FailOnSummonDeath;
        public float FailDistance;
        public int ContinentId;
        public long CompletionPatrol;
        public float CompletionDistance;
        public long FailPatrol;
        public float FailPatrolDistance;
        public bool StartEscort;
        public bool EndEscort;
        public int CachedCreatureId;

        public ObjectiveRequirementEscort(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Escort;
            StartEscort = true;
            EndEscort = true;
            ContinentId = -1;
            CompletionPatrol = -1L;
            FailPatrol = -1L;
            CachedCreatureId = -1;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var skillId = elem.Element("SkillID");
            if (skillId != null && !skillId.IsEmpty)
                SkillId = (int)skillId;

            var skillLvl = elem.Element("SkillLevel");
            if (skillLvl != null && !skillLvl.IsEmpty)
                SkillLevel = (int)skillLvl;

            var failOnSummDeath = elem.Element("FailOnDeath");
            if (failOnSummDeath != null && !failOnSummDeath.IsEmpty)
                FailOnSummonDeath = (int)failOnSummDeath != 0;

            var failDist = elem.Element("MaxDistance");
            if (failDist != null && !failDist.IsEmpty)
                FailDistance = (float)failDist;

            var contCBID = elem.Element("ContinentCBID");
            if (contCBID != null && !contCBID.IsEmpty)
                ContinentId = (int)contCBID;

            var compPatrol = elem.Element("CompletionCOID");
            if (compPatrol != null && !compPatrol.IsEmpty)
                CompletionPatrol = (long)compPatrol;

            var compPatrolDist = elem.Element("CompletionPatrolDistance");
            if (compPatrolDist != null && !compPatrolDist.IsEmpty)
                CompletionDistance = (float)compPatrolDist;

            var failPatrol = elem.Element("FailCOID");
            if (failPatrol != null && !failPatrol.IsEmpty)
                FailPatrol = (long)failPatrol;

            var failPatrolDist = elem.Element("FailPatrolDistance");
            if (failPatrolDist != null && !failPatrolDist.IsEmpty)
                FailPatrolDistance = (float)failPatrolDist;

            var startEscort = elem.Element("StartEscort");
            if (startEscort != null && !startEscort.IsEmpty)
                StartEscort = (int)startEscort != 0;

            var endEscort = elem.Element("EndEscort");
            if (endEscort != null && !endEscort.IsEmpty)
                EndEscort = (int)endEscort != 0;
        }
    }
}
