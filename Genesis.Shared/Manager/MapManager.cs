using System.Collections.Generic;

namespace Genesis.Shared.Manager
{
    using Map;

    public static class MapManager
    {
        private static readonly Dictionary<uint, SectorMap> Maps = new Dictionary<uint, SectorMap>();
        private static readonly object LockObject = new object();

        public static SectorMap GetMap(uint id)
        {
            lock (LockObject)
            {
                if (Maps.ContainsKey(id))
                    return Maps[id];

                var sm = new SectorMap(id);
                if (sm.MapEntry == null) // failed to load map
                    return null;

                Maps.Add(id, sm);

                return sm;
            }
        }
    }
}
