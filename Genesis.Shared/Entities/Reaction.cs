using System;
using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Entities
{
    using Base;
    using Structures;
    using Utils.Extensions;

    public class Reaction : ClonedObjectBase
    {
        public Boolean ActOnActivator;
        public Boolean AllConditionsNeeded;
        public Boolean DoForAllPlayers;
        public Boolean DoForConvoy;
        public Single GenericVar1;
        public Single GenericVar2;
        public Single GenericVar3;
        public Byte MapTransferType;
        public UInt32 MapTrasferData;
        public String MiscText;
        public List<Int32> MissionTypes = new List<Int32>();
        public List<Int32> Missions = new List<Int32>();
        public String Name;
        public Int32 ObjectiveIDCheck;
        public List<UInt64> Objects = new List<UInt64>();
        public ReactionText ReactionText;
        public Byte ReactionType;
        public List<UInt64> Reactions = new List<UInt64>();
        public List<TriggerConditional> TriggerConditionals = new List<TriggerConditional>();
        public String WaypointText;
        public UInt32 WaypointType;

        public override void Unserialize(BinaryReader br, UInt32 mapVersion)
        {
            Name = br.ReadUtf8StringOn(65);
            ReactionType = br.ReadByte();
            ActOnActivator = br.ReadBoolean();
            ObjectiveIDCheck = br.ReadInt32();
            DoForConvoy = br.ReadBoolean();

            GenericVar1 = br.ReadSingle();
            GenericVar2 = br.ReadSingle();
            GenericVar3 = br.ReadSingle();

            if (ReactionType == 10) // Reaction Transfer Map
            {
                MapTransferType = br.ReadByte();
                MapTrasferData = br.ReadUInt32();
            }
            else
            {
                var size = br.ReadInt32();
                for (var i = 0; i < size; ++i)
                    Objects.Add(br.ReadUInt32());
            }

            var numOfItems = br.ReadUInt32();
            for (var i = 0; i < numOfItems; ++i)
                Reactions.Add(br.ReadUInt32());

            if (ReactionType == 18 && br.ReadByte() != 0) // Reaction Text
                ReactionText = new ReactionText(br, mapVersion);

            if (mapVersion >= 8)
            {
                AllConditionsNeeded = br.ReadBoolean();

                var numOfConds = br.ReadUInt32();
                for (var i = 0; i < numOfConds; ++i)
                    TriggerConditionals.Add(TriggerConditional.Read(br));

                DoForAllPlayers = br.ReadBoolean();
            }

            if (mapVersion >= 9)
            {
                if (ReactionType == 46 || ReactionType == 47 || ReactionType == 76 || ReactionType == 77)
                    // ReactionTimerStart / ReactionTimerStop / ReactionPlayMusic / ReactionPath
                    MiscText = br.ReadLengthedString();
            }

            if (mapVersion >= 10)
            {
                if (ReactionType == 35 || ReactionType == 64 || ReactionType == 65)
                // ReactionAddWaypoint / ReactionSetMapDynamicWaypoint / ReactionSetProgressBar
                {
                    WaypointType = br.ReadUInt32();
                    WaypointText = br.ReadLengthedString();
                }
            }

            if (mapVersion >= 16 && (ReactionType == 37 || mapVersion == 16)) // ReactionGiveMissionDialog
            {
                var missionCount = br.ReadUInt32();
                for (var i = 0; i < missionCount; ++i)
                    MissionTypes.Add(br.ReadInt32());

                var missionCount2 = br.ReadUInt32();
                for (var i = 0; i < missionCount2; ++i)
                    Missions.Add(br.ReadInt32());
            }
        }

        public override void WriteToCreatePacket(Packet packet, Boolean extended = false)
        {
            throw new NotSupportedException();
        }
    }
}
