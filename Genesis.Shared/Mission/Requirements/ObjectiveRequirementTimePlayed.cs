using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementTimePlayed : ObjectiveRequirement
    {
        public Int32 SecondsPlayed;
        public Boolean UseTotal;
        public Boolean FailTimer;
        public Boolean ShowTimer;
        public String TimerText;

        public ObjectiveRequirementTimePlayed(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.TimePlayed;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var secsPlayed = elem.Element("SecondsPlayed");
            if (secsPlayed != null && !secsPlayed.IsEmpty)
                SecondsPlayed = (Int32)secsPlayed;

            var minsPlayed = elem.Element("MinutesPlayed");
            if (minsPlayed != null && !minsPlayed.IsEmpty)
                SecondsPlayed = (Int32)minsPlayed * 60;

            var useTotal = elem.Element("UseTotal");
            if (useTotal != null && !useTotal.IsEmpty)
                UseTotal = (Int32)useTotal != 0;

            var failTimer = elem.Element("FailTimer");
            if (failTimer != null && !failTimer.IsEmpty)
                FailTimer = (Int32)failTimer != 0;

            var showTimer = elem.Element("ShowTimer");
            if (showTimer != null && !showTimer.IsEmpty)
                ShowTimer = (Int32)showTimer != 0;

            var timerText = elem.Element("TimerText");
            if (timerText != null && !timerText.IsEmpty)
                TimerText = (String)timerText;
        }
    }
}
