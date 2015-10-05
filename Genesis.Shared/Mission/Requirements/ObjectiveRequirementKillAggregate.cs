using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    class ObjectiveRequirementKillAggregate : ObjectiveRequirement
    {
        public List<int> Targets = new List<int>();
        public List<int> TemplateTargets = new List<int>();
        public int AllowedType;
        public bool TrackDamage;
        public bool TargetIsFaction;
        public int ContinentId;
        public int NumToKill;
        public bool NegativeKill;
        public string ShortDescription;

        public ObjectiveRequirementKillAggregate(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.KillAggregate;
            AllowedType = -1;
            ContinentId = -1;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (byte)(int)elem.Attribute("slot");

            var contCBID = elem.Element("ContinentCBID");
            if (contCBID != null && !contCBID.IsEmpty)
                ContinentId = (int)contCBID;

            var cbid = elem.Element("CBID");
            if (cbid != null && !cbid.IsEmpty)
            {
                var str = (string)cbid;
                foreach (var id in str.Split(new[] { '|' }).Where(id => !string.IsNullOrWhiteSpace(id)))
                    Targets.Add(int.Parse(id));
            }

            var templId = elem.Element("TEMPLATEID");
            if (templId != null && !templId.IsEmpty)
            {
                var str = (string)templId;
                foreach (var id in str.Split(new[] { '|' }).Where(id => !string.IsNullOrWhiteSpace(id)))
                    TemplateTargets.Add(int.Parse(id));
            }

            var negativeKill = elem.Element("NegativeKill");
            if (negativeKill != null && !negativeKill.IsEmpty)
                NegativeKill = (int)negativeKill != 0;

            var numToKill = elem.Element("NumToKill");
            if (numToKill != null && !numToKill.IsEmpty)
                NumToKill = (int)numToKill;

            var tIsFaction = elem.Element("TargetIsFaction");
            if (tIsFaction != null && !tIsFaction.IsEmpty)
                TargetIsFaction = (int)tIsFaction != 0;

            var allowedType = elem.Element("AllowedType");
            if (allowedType != null && !allowedType.IsEmpty)
                AllowedType = (int)allowedType;

            var shortDescr = elem.Element("ShortDescription");
            if (shortDescr != null && !shortDescr.IsEmpty)
                ShortDescription = (string)shortDescr;
        }
    }
}
