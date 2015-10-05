using System;

namespace Genesis.Shared.Database.Tables
{
    using Utils;

    public class Convoy
    {
        public void RemoveFromConvoy(long coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"DELETE FROM `convoy_member` WHERE `MemberCoid` = {coid}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in RemoveFromConvoy(Int64)! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
