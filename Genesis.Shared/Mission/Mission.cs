using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace Genesis.Shared.Mission
{
    using Manager;
    using Utils.Extensions;

    public class Mission
    {
        #region Template Data
        public Int32 Achievement;
        public Int16 ActiveObjectiveOverride;
        public Int16 AutoAssing;
        public Int32 Continent;
        public Int32 Discipline;
        public Int32 DisciplineValue;
        public UInt32 Id;
        public Int16 IsRepeatable;
        public Int32[] Item;
        public Int16[] ItemIsKit;
        public Int32[] ItemQuantity;
        public Int32[] ItemTemplate;
        public Single[] ItemValue;
        public Int32 NPC;
        public String Name;
        public Byte NumberOfObjectives;

        public Dictionary<Byte, MissionObjective> Objectives;
        public Int32 Pocket;
        public Int32 Priority;
        public Int32 Region;
        public Int16 ReqClass;
        public Int32 ReqLevelMax;
        public Int32 ReqLevelMin;
        public Int32[] ReqMissionId;
        public Int16 ReqRace;
        public Int32 RequirementEventId;
        public Int32 RequirementsNegative;
        public Int32 RequirementsOred;
        public Int32 RewardDiscipline;
        public Int32 RewardDisciplineValue;
        public Int32 RewardUnassignedDisciplinePoints;
        public Int16 TargetLevel;
        public Byte Type;
        #endregion

        #region Extra Data
        public String Title;
        public String InternalName;
        public String Description;
        public String OnLineAccept;
        public String OnLineReject;
        public String NotCompleteText;
        public String CompleteText;
        public String FailText;
        public Boolean CoreMission;
        #endregion

        public static Mission Read(BinaryReader br)
        {
            var mi = new Mission
            {
                Id = br.ReadUInt32(),
                Name = br.ReadUnicodeString(65),
                Type = br.ReadByte(),
                Objectives = new Dictionary<Byte, MissionObjective>()
            };

            br.ReadByte();

            mi.NPC = br.ReadInt32();
            mi.Priority = br.ReadInt32();
            mi.ReqRace = br.ReadInt16();
            mi.ReqClass = br.ReadInt16();
            mi.ReqLevelMin = br.ReadInt32();
            mi.ReqLevelMax = br.ReadInt32();
            mi.ReqMissionId = br.Read<Int32>(4);
            mi.IsRepeatable = br.ReadInt16();

            br.ReadBytes(2);

            mi.Item = br.Read<Int32>(4);
            mi.ItemTemplate = br.Read<Int32>(4);
            mi.ItemValue = br.Read<Single>(4);
            mi.ItemIsKit = br.Read<Int16>(4);
            mi.ItemQuantity = br.Read<Int32>(4);
            mi.AutoAssing = br.ReadInt16();
            mi.ActiveObjectiveOverride = br.ReadInt16();
            mi.Continent = br.ReadInt32();
            mi.Achievement = br.ReadInt32();
            mi.Discipline = br.ReadInt32();
            mi.DisciplineValue = br.ReadInt32();
            mi.RewardDiscipline = br.ReadInt32();
            mi.RewardDisciplineValue = br.ReadInt32();
            mi.RewardUnassignedDisciplinePoints = br.ReadInt32();
            mi.RequirementEventId = br.ReadInt32();
            mi.TargetLevel = br.ReadInt16();

            br.ReadBytes(2);

            mi.RequirementsOred = br.ReadInt32();
            mi.RequirementsNegative = br.ReadInt32();
            mi.Region = br.ReadInt32();
            mi.Pocket = br.ReadInt32();
            mi.NumberOfObjectives = br.ReadByte();

            br.ReadBytes(7);

            XElement element = null;

            var stream = AssetManager.GetStreamByName(String.Format("{0}.xml", mi.Name), "missions.glm") ??
                         AssetManager.GetStreamByName(String.Format("{0}.xml", mi.Name), "misc.glm");

            if (stream != null)
            {
                using (stream)
                {
                    var doc = XDocument.Load(stream);
                    Debug.Assert(doc != null);

                    element = doc.Element("Mission");
                    if (element != null)
                    {
                        mi.Title = (String)element.Element("Title");
                        mi.InternalName = (String)element.Element("Internal");
                        mi.Description = (String)element.Element("Description");
                        mi.OnLineAccept = (String)element.Element("OnLineAccept");
                        mi.OnLineReject = (String)element.Element("OnLineReject");
                        mi.NotCompleteText = (String)element.Element("NotCompleteText");
                        mi.CompleteText = (String)element.Element("CompleteText");
                        mi.FailText = (String)element.Element("FailText");
                        mi.CoreMission = (String)element.Element("CoreMission") != "0";
                    }
                }
            }

            var numOfObjective = br.ReadInt32();
            for (var i = 0; i < numOfObjective; ++i)
            {
                var obj = MissionObjective.Read(br, mi, element);
                mi.Objectives.Add(obj.Sequence, obj);
            }

            return mi;
        }
    }
}
