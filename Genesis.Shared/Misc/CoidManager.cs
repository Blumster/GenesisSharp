namespace Genesis.Shared.Misc
{
    using Database;

    public static class CoidManager
    {
        private static readonly object Lock = new object();

        private static long _nextCOID;

        public static long NextCOID
        {
            get
            {
                lock (Lock)
                    return _nextCOID++;
            }
        }

        public static void Initialize()
        {
            _nextCOID = DataAccess.Item.GetNextCoid();
        }
    }
}
