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
        public bool ActOnActivator;
        public bool AllConditionsNeeded;
        public bool DoForAllPlayers;
        public bool DoForConvoy;
        public float GenericVar1;
        public float GenericVar2;
        public float GenericVar3;
        public byte MapTransferType;
        public uint MapTrasferData;
        public string MiscText;
        public List<int> MissionTypes = new List<int>();
        public List<int> Missions = new List<int>();
        public string Name;
        public int ObjectiveIDCheck;
        public List<ulong> Objects = new List<ulong>();
        public ReactionText ReactionText;
        public byte ReactionType;
        public List<ulong> Reactions = new List<ulong>();
        public List<TriggerConditional> TriggerConditionals = new List<TriggerConditional>();
        public string WaypointText;
        public uint WaypointType;

        public override void Unserialize(BinaryReader br, uint mapVersion)
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

        public override void WriteToCreatePacket(Packet packet, bool extended = false)
        {
            throw new NotSupportedException();
        }
    }
}
