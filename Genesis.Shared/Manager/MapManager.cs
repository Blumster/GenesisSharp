using System;
using System.Collections.Generic;

namespace Genesis.Shared.Manager
{
    using Map;

    public static class MapManager
    {
        private static readonly Dictionary<UInt32, SectorMap> Maps = new Dictionary<UInt32, SectorMap>();
        private static readonly Object LockObject = new Object();

        public static SectorMap GetMap(UInt32 id)
        {
            lock (LockObject)
            {
                if (Maps.ContainsKey(id))
                    return Maps[id];

                var sm = new SectorMap(id);

                Maps.Add(id, sm);

                return sm;
            }
        }
    }
}
