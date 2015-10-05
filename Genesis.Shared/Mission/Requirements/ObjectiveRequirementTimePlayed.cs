using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementTimePlayed : ObjectiveRequirement
    {
        public int SecondsPlayed;
        public bool UseTotal;
        public bool FailTimer;
        public bool ShowTimer;
        public string TimerText;

        public ObjectiveRequirementTimePlayed(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.TimePlayed;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var secsPlayed = elem.Element("SecondsPlayed");
            if (secsPlayed != null && !secsPlayed.IsEmpty)
                SecondsPlayed = (int)secsPlayed;

            var minsPlayed = elem.Element("MinutesPlayed");
            if (minsPlayed != null && !minsPlayed.IsEmpty)
                SecondsPlayed = (int)minsPlayed * 60;

            var useTotal = elem.Element("UseTotal");
            if (useTotal != null && !useTotal.IsEmpty)
                UseTotal = (int)useTotal != 0;

            var failTimer = elem.Element("FailTimer");
            if (failTimer != null && !failTimer.IsEmpty)
                FailTimer = (int)failTimer != 0;

            var showTimer = elem.Element("ShowTimer");
            if (showTimer != null && !showTimer.IsEmpty)
                ShowTimer = (int)showTimer != 0;

            var timerText = elem.Element("TimerText");
            if (timerText != null && !timerText.IsEmpty)
                TimerText = (string)timerText;
        }
    }
}
