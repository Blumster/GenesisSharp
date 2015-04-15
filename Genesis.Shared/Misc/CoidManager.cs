using System;

namespace Genesis.Shared.Misc
{
    using Database;

    public static class CoidManager
    {
        private static readonly Object _lock = new Object();

        private static Int64 _nextCOID;

        public static Int64 NextCOID
        {
            get
            {
                lock (_lock)
                    return _nextCOID++;
            }
        }

        public static void Initialize()
        {
            _nextCOID = DataAccess.Item.GetNextCoid();
        }
    }
}
