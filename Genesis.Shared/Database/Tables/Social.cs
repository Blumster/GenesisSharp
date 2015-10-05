using System;
using System.Collections.Generic;

namespace Genesis.Shared.Database.Tables
{
    using Constant;
    using Shared.Social;
    using Utils;

    public class Social
    {
        public void AddEntry(long coid, long friendCoid, SocialType type)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"INSERT INTO `social` (`OwnerCoid`, `Coid`, `Type`) VALUES ({coid}, {friendCoid}, {(byte) type})"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in AddEntry(Int64, Int64, SocialType)! Exception: {0}", LogType.Error, e);
            }
        }

        public void RemoveEntry(long coid, long friendCoid, SocialType type)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"DELETE FROM `social` WHERE `OwnerCoid` = {coid} AND `Coid` = {friendCoid} AND `Type` = {(byte) type}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in RemoveEntry(Int64, Int64, SocialType)! Exception: {0}", LogType.Error, e);
            }
        }

        public IList<SocialEntry> GetEntries(long coid, SocialType type)
        {
            var list = new List<SocialEntry>();

            try
            {
                lock (DataAccess.DatabaseAccess)
                {
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"SELECT `s`.`Coid`, `c`.`Race`, `c`.`Class`, `c`.`Level`, `c`.`LastMapId`, `c`.`Name`, `s`.`Type` FROM `social` AS `s` LEFT JOIN `vehicle` AS `v` ON `s`.`Coid` = `v`.`OwnerCoid` LEFT JOIN `character` AS `c` ON `c`.`Coid` = `v`.`OwnerCoid` WHERE `s`.`OwnerCoid` = {coid}{(type != SocialType.All ? $" AND `s`.`Type` = {(byte) type}" : "")}"))
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

        public void DeleteEntriesByCharacter(long coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"DELETE FROM `social` WHERE `OwnerCoid` = {coid}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in DeleteEntriesByCharacter(Int64)! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
