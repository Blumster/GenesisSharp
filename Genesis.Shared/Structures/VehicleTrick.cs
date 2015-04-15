using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct VehicleTrick
    {
        public String Description;
        public Byte ExclusiveGroup;
        public String FileName;
        public String GroupDescription;
        public Byte GroupId;
        public Int32 Id;
        public Int32 VehicleId;

        public static VehicleTrick Read(BinaryReader br)
        {
            return new VehicleTrick
            {
                Id = br.ReadInt32(),
                VehicleId = br.ReadInt32(),
                ExclusiveGroup = br.ReadByte(),
                GroupId = br.ReadByte(),
                FileName = br.ReadUnicodeString(65),
                Description = br.ReadUnicodeString(33),
                GroupDescription = br.ReadUnicodeString(33)
            };
        }

        public override string ToString()
        {
            return String.Format("Id: {0} | File: {1} | Desc: {2} | GDesc: {3}", Id, FileName, Description, GroupDescription);
        }
    }
}
