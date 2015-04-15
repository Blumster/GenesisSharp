using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;
    using Structures;

    public class ObjectiveRequirementCrazyTaxi : ObjectiveRequirement
    {
        public List<Int64> Targets = new List<Int64>();
        public List<Reward> MoneyRewards = new List<Reward>();
        public List<Reward> ExpRewards = new List<Reward>();
        public List<TimeCurve> TimeLimits = new List<TimeCurve>();

        public Int32 ContinentId;
        public Int32 TargetCount;
        public Int32 FinishMissionCount;
        public Single VehicleMaxVecAtStop;
        public Boolean GiveExpReward;
        public Boolean GiveMoneyReward;
        public Boolean FinishOnMissionCount;
        public Single RadiusForTrigger;
        public Int32 ReachedTargetCount;
        public Single Timer;

        public ObjectiveRequirementCrazyTaxi(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.CrazyTaxi;
            ContinentId = -1;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var contCBID = elem.Element("ContinentCBID");
            if (contCBID != null && !contCBID.IsEmpty)
                ContinentId = (Int32)contCBID;

            var vehMaxVec = elem.Element("VehicleMaxVec");
            if (vehMaxVec != null && !vehMaxVec.IsEmpty)
                VehicleMaxVecAtStop = (Single)vehMaxVec;

            var radAtStop = elem.Element("RadiusOfStop");
            if (radAtStop != null && !radAtStop.IsEmpty)
                RadiusForTrigger = (Single)radAtStop;

            var missStopLim = elem.Element("MissionStopLimit");
            if (missStopLim != null && !missStopLim.IsEmpty)
                FinishOnMissionCount = (Int32)missStopLim != 0;

            var missStopCount = elem.Element("MissionStopCount");
            if (missStopCount != null && !missStopCount.IsEmpty)
                FinishMissionCount = (Int32)missStopCount;

            var giveMoney = elem.Element("GiveMoney");
            if (giveMoney != null && !giveMoney.IsEmpty)
                GiveMoneyReward = (Int32)giveMoney != 0;

            var giveExp = elem.Element("GiveExp");
            if (giveExp != null && !giveExp.IsEmpty)
                GiveExpReward = (Int32)giveExp != 0;

            // TODO: End Implement!
        }
    }
}
