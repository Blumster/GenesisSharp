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
        public int AttribPoints;
        public int ContinentObject;
        public float CreditScaler;
        public int Credits;
        public short CreditsIndex;
        public byte LayerIndex;
        public string MapName;
        public int ObjectiveId;
        public string ObjectiveName;
        public int QuestId;
        public int ReturnToNPC;
        public byte Sequence;
        public int SkillPoints;
        public int WorldPosition;
        public int XP;
        public float XPBalanceScaler;
        public short XPIndex;
        public float XPScaler;

        #region Extra Data
        public List<ObjectiveRequirement> Requirements;
        public Mission Owner { get; private set; }
        public string ExternalMapText { get; private set; }
        public string DefaultMapText { get; private set; }
        public string Title { get; private set; }
        public int CompleteCount { get; private set; }
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

            var obj = elem?.Elements("Objective").SingleOrDefault(e => (uint) e.Attribute("sequence") == mo.Sequence);
            if (obj == null)
                return mo;

            mo.ExternalMapText = (string)obj.Element("ExternalText");
            mo.Title = (string)obj.Element("Title");
            mo.DefaultMapText = (string)obj.Element("DefaultText");
            var cCountElem = obj.Element("CompleteCount");
            mo.CompleteCount = (cCountElem == null || string.IsNullOrEmpty((string)cCountElem)) ? 0 : (int)cCountElem;

            var req = obj.Elements("Requirement").ToList();
            if (req.Any())
                mo.Requirements.AddRange(req.Select(xElem => ObjectiveRequirement.Create(mo, xElem)).Where(requirement => requirement != null));

            return mo;
        }
    }
}
