using System.Collections.Generic;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;
    using Structures;

    public class ObjectiveRequirementCrazyTaxi : ObjectiveRequirement
    {
        public List<long> Targets = new List<long>();
        public List<Reward> MoneyRewards = new List<Reward>();
        public List<Reward> ExpRewards = new List<Reward>();
        public List<TimeCurve> TimeLimits = new List<TimeCurve>();

        public int ContinentId;
        public int TargetCount;
        public int FinishMissionCount;
        public float VehicleMaxVecAtStop;
        public bool GiveExpReward;
        public bool GiveMoneyReward;
        public bool FinishOnMissionCount;
        public float RadiusForTrigger;
        public int ReachedTargetCount;
        public float Timer;

        public ObjectiveRequirementCrazyTaxi(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.CrazyTaxi;
            ContinentId = -1;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var contCBID = elem.Element("ContinentCBID");
            if (contCBID != null && !contCBID.IsEmpty)
                ContinentId = (int)contCBID;

            var vehMaxVec = elem.Element("VehicleMaxVec");
            if (vehMaxVec != null && !vehMaxVec.IsEmpty)
                VehicleMaxVecAtStop = (float)vehMaxVec;

            var radAtStop = elem.Element("RadiusOfStop");
            if (radAtStop != null && !radAtStop.IsEmpty)
                RadiusForTrigger = (float)radAtStop;

            var missStopLim = elem.Element("MissionStopLimit");
            if (missStopLim != null && !missStopLim.IsEmpty)
                FinishOnMissionCount = (int)missStopLim != 0;

            var missStopCount = elem.Element("MissionStopCount");
            if (missStopCount != null && !missStopCount.IsEmpty)
                FinishMissionCount = (int)missStopCount;

            var giveMoney = elem.Element("GiveMoney");
            if (giveMoney != null && !giveMoney.IsEmpty)
                GiveMoneyReward = (int)giveMoney != 0;

            var giveExp = elem.Element("GiveExp");
            if (giveExp != null && !giveExp.IsEmpty)
                GiveExpReward = (int)giveExp != 0;

            // TODO: End Implement!
        }
    }
}
