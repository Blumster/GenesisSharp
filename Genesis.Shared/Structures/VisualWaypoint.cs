using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct VisualWaypoint
    {
        public uint Id;
        public ulong ObjectCOID;
        public int ObjectiveCount;
        public List<uint> Objectives;
        public ulong ReactionCOID;
        public byte Type;
        public float X;
        public float Y;
        public float Z;

        public static VisualWaypoint Read(BinaryReader br, uint mapVersion)
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

            wp.Objectives = new List<uint>(br.Read<uint>(wp.ObjectiveCount));

            return wp;
        }
    }
}
