using System;

namespace Genesis.Shared.Database.Tables
{
    using Utils;

    public class Clan
    {
        public void RemoveFromClan(Int64 coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("DELETE FROM `clan_member` WHERE `CharCoid` = {0}", coid)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in RemoveFromClan(Int64)! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
