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
        public int Achievement;
        public short ActiveObjectiveOverride;
        public short AutoAssing;
        public int Continent;
        public int Discipline;
        public int DisciplineValue;
        public uint Id;
        public short IsRepeatable;
        public int[] Item;
        public short[] ItemIsKit;
        public int[] ItemQuantity;
        public int[] ItemTemplate;
        public float[] ItemValue;
        public int NPC;
        public string Name;
        public byte NumberOfObjectives;

        public Dictionary<byte, MissionObjective> Objectives;
        public int Pocket;
        public int Priority;
        public int Region;
        public short ReqClass;
        public int ReqLevelMax;
        public int ReqLevelMin;
        public int[] ReqMissionId;
        public short ReqRace;
        public int RequirementEventId;
        public int RequirementsNegative;
        public int RequirementsOred;
        public int RewardDiscipline;
        public int RewardDisciplineValue;
        public int RewardUnassignedDisciplinePoints;
        public short TargetLevel;
        public byte Type;
        #endregion

        #region Extra Data
        public string Title;
        public string InternalName;
        public string Description;
        public string OnLineAccept;
        public string OnLineReject;
        public string NotCompleteText;
        public string CompleteText;
        public string FailText;
        public bool CoreMission;
        #endregion

        public static Mission Read(BinaryReader br)
        {
            var mi = new Mission
            {
                Id = br.ReadUInt32(),
                Name = br.ReadUnicodeString(65),
                Type = br.ReadByte(),
                Objectives = new Dictionary<byte, MissionObjective>()
            };

            br.ReadByte();

            mi.NPC = br.ReadInt32();
            mi.Priority = br.ReadInt32();
            mi.ReqRace = br.ReadInt16();
            mi.ReqClass = br.ReadInt16();
            mi.ReqLevelMin = br.ReadInt32();
            mi.ReqLevelMax = br.ReadInt32();
            mi.ReqMissionId = br.Read<int>(4);
            mi.IsRepeatable = br.ReadInt16();

            br.ReadBytes(2);

            mi.Item = br.Read<int>(4);
            mi.ItemTemplate = br.Read<int>(4);
            mi.ItemValue = br.Read<float>(4);
            mi.ItemIsKit = br.Read<short>(4);
            mi.ItemQuantity = br.Read<int>(4);
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

            var stream = AssetManager.GetStreamByName($"{mi.Name}.xml", "missions.glm") ??
                         AssetManager.GetStreamByName($"{mi.Name}.xml", "misc.glm");

            if (stream != null)
            {
                using (stream)
                {
                    var doc = XDocument.Load(stream);
                    Debug.Assert(doc != null);

                    element = doc.Element("Mission");
                    if (element != null)
                    {
                        mi.Title = (string)element.Element("Title");
                        mi.InternalName = (string)element.Element("Internal");
                        mi.Description = (string)element.Element("Description");
                        mi.OnLineAccept = (string)element.Element("OnLineAccept");
                        mi.OnLineReject = (string)element.Element("OnLineReject");
                        mi.NotCompleteText = (string)element.Element("NotCompleteText");
                        mi.CompleteText = (string)element.Element("CompleteText");
                        mi.FailText = (string)element.Element("FailText");
                        mi.CoreMission = (string)element.Element("CoreMission") != "0";
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
