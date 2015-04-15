using System;

namespace Genesis.Shared.Manager
{
    using Database;

    public static class COIDManager
    {
        private static readonly Object Lock = new Object();

        private static Int64 _nextCOID = -1;

        public static void Intialize(Boolean global)
        {
            _nextCOID = DataAccess.Item.GetNextCoid() + 1;

            if ((global && _nextCOID % 2 == 1) || (!global && _nextCOID % 2 == 0)) // TODO: a real coid generation, whithc works with 2+ servers
                ++_nextCOID;
        }

        public static Int64 NextCOID
        {
            get
            {
                if (_nextCOID == -1L)
                    throw new InvalidOperationException("You must Initialize the manager first!");

                lock (Lock)
                {
                    var ret = _nextCOID;

                    _nextCOID += 2;

                    return ret;
                }
            }
        }
    }
}
