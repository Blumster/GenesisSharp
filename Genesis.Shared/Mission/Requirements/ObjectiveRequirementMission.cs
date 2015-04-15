using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public class ObjectiveRequirementMission : ObjectiveRequirement
    {
        public List<Int32> MissionIds;
        public Int32 CountNeeded;
        public Boolean IdsAreMedals;

        public ObjectiveRequirementMission(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.Mission;
            MissionIds = new List<Int32>();
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var ids = elem.Element("IDs");
            if (ids != null && !ids.IsEmpty)
            {
                var str = (String)ids;
                foreach (var id in str.Split(new[] { '|' }).Where(id => !String.IsNullOrWhiteSpace(id)))
                    MissionIds.Add(Int32.Parse(id));
            }

            var countNeeded = elem.Element("CountNeeded");
            if (countNeeded != null && !countNeeded.IsEmpty)
                CountNeeded = (Int32)countNeeded;

            var idsAreMedals = elem.Element("IDsAreMedals");
            if (idsAreMedals != null && !idsAreMedals.IsEmpty)
                IdsAreMedals = (Int32)idsAreMedals != 0;
        }
    }
}
