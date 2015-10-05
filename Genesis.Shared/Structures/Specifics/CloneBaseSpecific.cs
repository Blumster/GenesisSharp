using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public struct CloneBaseSpecific
    {
        public uint Available;
        public int BaseValue;
        public int CloneBaseId;
        public int CommodityGroupType;
        public string FxFileName;
        public uint InLootGenerator;
        public uint InStores;
        public uint IsGeneratable;
        public bool IsSellable;
        public uint IsTargetable;
        public string LongDesc;
        public string ShortDesc;
        public int TilesetFlags;
        public int Type;
        public string UniqueName;

        public static CloneBaseSpecific Read(BinaryReader br)
        {
            return new CloneBaseSpecific
            {
                CloneBaseId = br.ReadInt32(),
                Type = br.ReadInt32(),
                TilesetFlags = br.ReadInt32(),
                UniqueName = br.ReadUnicodeString(65),
                ShortDesc = br.ReadUnicodeString(65),
                LongDesc = br.ReadUnicodeString(257),
                FxFileName = br.ReadUnicodeString(65),
                IsGeneratable = br.ReadUInt32(),
                IsTargetable = br.ReadUInt32(),
                Available = br.ReadUInt32(),
                InStores = br.ReadUInt32(),
                InLootGenerator = br.ReadUInt32(),
                BaseValue = br.ReadInt32(),
                CommodityGroupType = br.ReadInt32(),
                IsSellable = br.ReadUInt32() == 1
            };
        }
    }
}
