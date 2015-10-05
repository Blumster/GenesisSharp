using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementMission : ObjectiveRequirement
    {
        public List<int> MissionIds;
        public int CountNeeded;
        public bool IdsAreMedals;

        public ObjectiveRequirementMission(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Mission;
            MissionIds = new List<int>();
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var ids = elem.Element("IDs");
            if (ids != null && !ids.IsEmpty)
            {
                var str = (string)ids;
                foreach (var id in str.Split(new[] { '|' }).Where(id => !string.IsNullOrWhiteSpace(id)))
                    MissionIds.Add(int.Parse(id));
            }

            var countNeeded = elem.Element("CountNeeded");
            if (countNeeded != null && !countNeeded.IsEmpty)
                CountNeeded = (int)countNeeded;

            var idsAreMedals = elem.Element("IDsAreMedals");
            if (idsAreMedals != null && !idsAreMedals.IsEmpty)
                IdsAreMedals = (int)idsAreMedals != 0;
        }
    }
}
