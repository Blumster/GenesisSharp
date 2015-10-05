using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct VehicleTrick
    {
        public string Description;
        public byte ExclusiveGroup;
        public string FileName;
        public string GroupDescription;
        public byte GroupId;
        public int Id;
        public int VehicleId;

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
            return $"Id: {Id} | File: {FileName} | Desc: {Description} | GDesc: {GroupDescription}";
        }
    }
}
