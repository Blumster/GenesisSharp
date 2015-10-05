using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Entities
{
    using Structures;
    using Utils.Extensions;

    public class Store : SimpleObject
    {
        public bool IsJunkyard;
        public bool IsSouvenirStore;
        public bool IsVehicleStore;
        public List<ItemType> ItemsTypes = new List<ItemType>();

        public Vector4 Location;
        public uint MaxLevel;
        public uint MinLevel;
        public Vector4 Quaternion;
        public string StoreName;

        public override void Unserialize(BinaryReader br, uint mapVersion)
        {
            Location = Vector4.Read(br);
            Quaternion = Vector4.Read(br);

            if (((((mapVersion <= 50) ? 1 : 0) - 1) & 20) + 10 > 0)
            {
                var count = (uint)(((((mapVersion <= 50) ? 1 : 0) - 1) & 20) + 10);
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
