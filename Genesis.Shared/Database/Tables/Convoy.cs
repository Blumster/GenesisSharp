using System;

namespace Genesis.Shared.Database.Tables
{
    using Utils;

    public class Convoy
    {
        public void RemoveFromConvoy(Int64 coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("DELETE FROM `convoy_member` WHERE `MemberCoid` = {0}", coid)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in RemoveFromConvoy(Int64)! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
