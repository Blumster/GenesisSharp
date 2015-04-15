using System;
using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct VisualWaypoint
    {
        public UInt32 Id;
        public UInt64 ObjectCOID;
        public Int32 ObjectiveCount;
        public List<UInt32> Objectives;
        public UInt64 ReactionCOID;
        public Byte Type;
        public Single X;
        public Single Y;
        public Single Z;

        public static VisualWaypoint Read(BinaryReader br, UInt32 mapVersion)
        {
            /*var streamVer = */
            br.ReadUInt32();

            var wp = new VisualWaypoint
            {
                Id = br.ReadUInt32(),
                Type = br.ReadByte(),
                X = br.ReadSingle(),
                Y = br.ReadSingle(),
                Z = br.ReadSingle(),
                ObjectCOID = br.ReadUInt64(),
                ReactionCOID = br.ReadUInt64(),
                ObjectiveCount = br.ReadInt32()
            };

            wp.Objectives = new List<UInt32>(br.Read<UInt32>(wp.ObjectiveCount));

            return wp;
        }
    }
}
