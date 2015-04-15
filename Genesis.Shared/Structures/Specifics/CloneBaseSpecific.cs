using System;
using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public struct CloneBaseSpecific
    {
        public UInt32 Available;
        public Int32 BaseValue;
        public Int32 CloneBaseId;
        public Int32 CommodityGroupType;
        public String FxFileName;
        public UInt32 InLootGenerator;
        public UInt32 InStores;
        public UInt32 IsGeneratable;
        public Boolean IsSellable;
        public UInt32 IsTargetable;
        public String LongDesc;
        public String ShortDesc;
        public Int32 TilesetFlags;
        public Int32 Type;
        public String UniqueName;

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
