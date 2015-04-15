using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Genesis.Shared.Mission
{
    using Requirements;
    using Utils.Extensions;

    public class MissionObjective
    {
        public Int32 AttribPoints;
        public Int32 ContinentObject;
        public Single CreditScaler;
        public Int32 Credits;
        public Int16 CreditsIndex;
        public Byte LayerIndex;
        public String MapName;
        public Int32 ObjectiveId;
        public String ObjectiveName;
        public Int32 QuestId;
        public Int32 ReturnToNPC;
        public Byte Sequence;
        public Int32 SkillPoints;
        public Int32 WorldPosition;
        public Int32 XP;
        public Single XPBalanceScaler;
        public Int16 XPIndex;
        public Single XPScaler;

        #region Extra Data
        public List<ObjectiveRequirement> Requirements;
        public Mission Owner { get; private set; }
        public String ExternalMapText { get; private set; }
        public String DefaultMapText { get; private set; }
        public String Title { get; private set; }
        public Int32 CompleteCount { get; private set; }
        #endregion

        public static MissionObjective Read(BinaryReader br, Mission owner, XElement elem)
        {
            var mo = new MissionObjective
            {
                QuestId = br.ReadInt32(),
                ObjectiveId = br.ReadInt32(),
                Sequence = br.ReadByte(),
                Owner = owner,
                Requirements = new List<ObjectiveRequirement>(),
            };

            br.ReadBytes(1);

            mo.ObjectiveName = br.ReadUnicodeString(65);
            mo.MapName = br.ReadUnicodeString(65);

            br.ReadBytes(2);

            mo.WorldPosition = br.ReadInt32();
            mo.ContinentObject = br.ReadInt32();
            mo.LayerIndex = br.ReadByte();

            br.ReadBytes(3);

            mo.XP = br.ReadInt32();
            mo.Credits = br.ReadInt32();
            mo.AttribPoints = br.ReadInt32();
            mo.SkillPoints = br.ReadInt32();
            mo.ReturnToNPC = br.ReadInt32();

            mo.XPIndex = br.ReadInt16();
            mo.CreditsIndex = br.ReadInt16();

            mo.XPScaler = br.ReadSingle();
            mo.XPBalanceScaler = br.ReadSingle();
            mo.CreditScaler = br.ReadSingle();

            if (elem != null)
            {
                var obj = elem.Elements("Objective").SingleOrDefault(e => (UInt32)e.Attribute("sequence") == mo.Sequence);
                if (obj != null)
                {
                    mo.ExternalMapText = (String)obj.Element("ExternalText");
                    mo.Title = (String)obj.Element("Title");
                    mo.DefaultMapText = (String)obj.Element("DefaultText");
                    var cCountElem = obj.Element("CompleteCount");
                    mo.CompleteCount = (cCountElem == null || String.IsNullOrEmpty((String)cCountElem)) ? 0 : (Int32)cCountElem;

                    var req = obj.Elements("Requirement").ToList();
                    if (req.Any())
                        mo.Requirements.AddRange(req.Select(xElem => ObjectiveRequirement.Create(mo, xElem)).Where(requirement => requirement != null));
                }
            }

            return mo;
        }
    }
}
