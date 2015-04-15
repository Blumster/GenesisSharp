using System;
using System.Collections.Generic;

namespace Genesis.Shared.Database.Tables
{
    using Constant;
    using Shared.Social;
    using Utils;

    public class Social
    {
        public void AddEntry(Int64 coid, Int64 friendCoid, SocialType type)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("INSERT INTO `social` (`OwnerCoid`, `Coid`, `Type`) VALUES ({0}, {1}, {2})", coid, friendCoid, (Byte)type)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in AddEntry(Int64, Int64, SocialType)! Exception: {0}", LogType.Error, e);
            }
        }

        public void RemoveEntry(Int64 coid, Int64 friendCoid, SocialType type)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("DELETE FROM `social` WHERE `OwnerCoid` = {0} AND `Coid` = {1} AND `Type` = {2}", coid, friendCoid, (Byte)type)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in RemoveEntry(Int64, Int64, SocialType)! Exception: {0}", LogType.Error, e);
            }
        }

        public IList<SocialEntry> GetEntries(Int64 coid, SocialType type)
        {
            var list = new List<SocialEntry>();

            try
            {
                lock (DataAccess.DatabaseAccess)
                {
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("SELECT `s`.`Coid`, `c`.`Race`, `c`.`Class`, `c`.`Level`, `c`.`LastMapId`, `c`.`Name`, `s`.`Type` FROM `social` AS `s` LEFT JOIN `vehicle` AS `v` ON `s`.`Coid` = `v`.`OwnerCoid` LEFT JOIN `character` AS `c` ON `c`.`Coid` = `v`.`OwnerCoid` WHERE `s`.`OwnerCoid` = {0}{1}", coid, (type != SocialType.All ? String.Format(" AND `s`.`Type` = {0}", (Byte)type) : ""))))
                    {
                        using (var reader = comm.ExecuteReader())
                        {
                            while (true)
                            {
                                var s = SocialEntry.Read(coid, reader, type);
                                if (s == null)
                                    break;

                                list.Add(s);
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetEntries(Int64, SocialType)! Exception: {0}", LogType.Error, e);
            }
            return list;
        }

        public void DeleteEntriesByCharacter(Int64 coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("DELETE FROM `social` WHERE `OwnerCoid` = {0}", coid)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in DeleteEntriesByCharacter(Int64)! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
