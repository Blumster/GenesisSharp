using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    class ObjectiveRequirementKillAggregate : ObjectiveRequirement
    {
        public List<Int32> Targets = new List<Int32>();
        public List<Int32> TemplateTargets = new List<Int32>();
        public Int32 AllowedType;
        public Boolean TrackDamage;
        public Boolean TargetIsFaction;
        public Int32 ContinentId;
        public Int32 NumToKill;
        public Boolean NegativeKill;
        public String ShortDescription;

        public ObjectiveRequirementKillAggregate(MissionObjective owner)
            : base(owner)
        {
            RequirementType = RequirementType.KillAggregate;
            AllowedType = -1;
            ContinentId = -1;
        }

        public override void UnSerialize(XElement elem)
        {
            FirstStateSlot = (Byte)(Int32)elem.Attribute("slot");

            var contCBID = elem.Element("ContinentCBID");
            if (contCBID != null && !contCBID.IsEmpty)
                ContinentId = (Int32)contCBID;

            var cbid = elem.Element("CBID");
            if (cbid != null && !cbid.IsEmpty)
            {
                var str = (String)cbid;
                foreach (var id in str.Split(new[] { '|' }).Where(id => !String.IsNullOrWhiteSpace(id)))
                    Targets.Add(Int32.Parse(id));
            }

            var templId = elem.Element("TEMPLATEID");
            if (templId != null && !templId.IsEmpty)
            {
                var str = (String)templId;
                foreach (var id in str.Split(new[] { '|' }).Where(id => !String.IsNullOrWhiteSpace(id)))
                    TemplateTargets.Add(Int32.Parse(id));
            }

            var negativeKill = elem.Element("NegativeKill");
            if (negativeKill != null && !negativeKill.IsEmpty)
                NegativeKill = (Int32)negativeKill != 0;

            var numToKill = elem.Element("NumToKill");
            if (numToKill != null && !numToKill.IsEmpty)
                NumToKill = (Int32)numToKill;

            var tIsFaction = elem.Element("TargetIsFaction");
            if (tIsFaction != null && !tIsFaction.IsEmpty)
                TargetIsFaction = (Int32)tIsFaction != 0;

            var allowedType = elem.Element("AllowedType");
            if (allowedType != null && !allowedType.IsEmpty)
                AllowedType = (Int32)allowedType;

            var shortDescr = elem.Element("ShortDescription");
            if (shortDescr != null && !shortDescr.IsEmpty)
                ShortDescription = (String)shortDescr;
        }
    }
}
