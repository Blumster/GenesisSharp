using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementEscort : ObjectiveRequirement
    {
        public Int32 SkillId;
        public Int32 SkillLevel;
        public Boolean FailOnSummonDeath;
        public Single FailDistance;
        public Int32 ContinentId;
        public Int64 CompletionPatrol;
        public Single CompletionDistance;
        public Int64 FailPatrol;
        public Single FailPatrolDistance;
        public Boolean StartEscort;
        public Boolean EndEscort;
        public Int32 CachedCreatureId;

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
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var skillId = elem.Element("SkillID");
            if (skillId != null && !skillId.IsEmpty)
                SkillId = (Int32)skillId;

            var skillLvl = elem.Element("SkillLevel");
            if (skillLvl != null && !skillLvl.IsEmpty)
                SkillLevel = (Int32)skillLvl;

            var failOnSummDeath = elem.Element("FailOnDeath");
            if (failOnSummDeath != null && !failOnSummDeath.IsEmpty)
                FailOnSummonDeath = (Int32)failOnSummDeath != 0;

            var failDist = elem.Element("MaxDistance");
            if (failDist != null && !failDist.IsEmpty)
                FailDistance = (Single)failDist;

            var contCBID = elem.Element("ContinentCBID");
            if (contCBID != null && !contCBID.IsEmpty)
                ContinentId = (Int32)contCBID;

            var compPatrol = elem.Element("CompletionCOID");
            if (compPatrol != null && !compPatrol.IsEmpty)
                CompletionPatrol = (Int64)compPatrol;

            var compPatrolDist = elem.Element("CompletionPatrolDistance");
            if (compPatrolDist != null && !compPatrolDist.IsEmpty)
                CompletionDistance = (Single)compPatrolDist;

            var failPatrol = elem.Element("FailCOID");
            if (failPatrol != null && !failPatrol.IsEmpty)
                FailPatrol = (Int64)failPatrol;

            var failPatrolDist = elem.Element("FailPatrolDistance");
            if (failPatrolDist != null && !failPatrolDist.IsEmpty)
                FailPatrolDistance = (Single)failPatrolDist;

            var startEscort = elem.Element("StartEscort");
            if (startEscort != null && !startEscort.IsEmpty)
                StartEscort = (Int32)startEscort != 0;

            var endEscort = elem.Element("EndEscort");
            if (endEscort != null && !endEscort.IsEmpty)
                EndEscort = (Int32)endEscort != 0;
        }
    }
}
