using System;

namespace Genesis.Shared.Database.Tables
{
    using Utils;

    public class Clan
    {
        public void RemoveFromClan(long coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"DELETE FROM `clan_member` WHERE `CharCoid` = {coid}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in RemoveFromClan(Int64)! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
