using System;
using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Entities
{
    using Structures;
    using Utils.Extensions;

    public class Store : SimpleObject
    {
        public Boolean IsJunkyard;
        public Boolean IsSouvenirStore;
        public Boolean IsVehicleStore;
        public List<ItemType> ItemsTypes = new List<ItemType>();

        public Vector4 Location;
        public UInt32 MaxLevel;
        public UInt32 MinLevel;
        public Vector4 Quaternion;
        public String StoreName;

        public override void Unserialize(BinaryReader br, UInt32 mapVersion)
        {
            Location = Vector4.Read(br);
            Quaternion = Vector4.Read(br);

            if (((((mapVersion <= 50) ? 1 : 0) - 1) & 20) + 10 > 0)
            {
                var count = (UInt32)(((((mapVersion <= 50) ? 1 : 0) - 1) & 20) + 10);
                for (var i = 0U; i < count; ++i)
                    ItemsTypes.Add(ItemType.Read(br));
            }

            if (mapVersion >= 39)
            {
                StoreName = br.ReadLengthedString();
                MinLevel = br.ReadUInt32();
                MaxLevel = br.ReadUInt32();
            }

            if (mapVersion > 40)
                IsJunkyard = br.ReadBoolean();

            if (mapVersion >= 50)
                IsVehicleStore = br.ReadBoolean();

            if (mapVersion >= 61)
                IsSouvenirStore = br.ReadBoolean();
        }
    }
}
