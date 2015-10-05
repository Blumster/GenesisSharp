using System;
using System.Text;

namespace Genesis.Shared.Database.Tables
{
    using DataStructs;
    using Utils;

    public class Item
    {
        public long GetNextCoid()
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(GetMaxCoidQuery()))
                        using (var reader = comm.ExecuteReader())
                            if (reader.Read())
                                return reader.GetInt64(0);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetNextCoid! Exception: {0}", LogType.Error, e);
            }
            return 0;
        }

        public void InsertItemInto(ItemData item)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"INSERT INTO `{item.TableName}` (`Coid`, `Cbid`) VALUES ({item.Coid}, {item.Cbid})"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in InsertItemInto! Exception: {0}", LogType.Error, e);
            }
        }

        public void UpdateItemInto(ItemData item)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(string.Format("UPDATE `{0}` SET `Cbid` = {2} WHERE `Coid` = {1}", item.TableName, item.Coid, item.Cbid)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in InsertItemInto! Exception: {0}", LogType.Error, e);
            }
        }

        public ItemData GetItemFrom(string table, long coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(
                        $"SELECT `Coid`, `Cbid` FROM `{table}` WHERE `Coid` = {coid}"))
                        using (var reader = comm.ExecuteReader())
                            return ItemData.Read(reader);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetItemFrom! Exception: {0}", LogType.Error, e);
            }
            return null;
        }

        public void DeleteItem(ItemData data)
        {
            DeleteItem(data.TableName, data.Coid);
        }

        public void DeleteItem(string table, long coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"DELETE FROM `{table}` WHERE `Coid` = {coid}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in DeleteItem! Exception: {0}", LogType.Error, e);
            }
        }

        private static string GetMaxCoidQuery()
        {
            var sb = new StringBuilder();

            sb.Append("SELECT GREATEST(");
            sb.Append("IFNULL((SELECT MAX(`Coid`) FROM `character`),0),");
            sb.Append("IFNULL((SELECT MAX(`Coid`) FROM `item_armor`),0),");
            sb.Append("IFNULL((SELECT MAX(`Coid`) FROM `item_powerplant`),0),");
            sb.Append("IFNULL((SELECT MAX(`Coid`) FROM `item_simple`),0),");
            sb.Append("IFNULL((SELECT MAX(`Coid`) FROM `item_weapon`),0),");
            sb.Append("IFNULL((SELECT MAX(`Coid`) FROM `item_wheelset`),0),");
            sb.Append("IFNULL((SELECT MAX(`Coid`) FROM `vehicle`),0),");
            sb.Append("0)");

            return sb.ToString();
        }
    }
}
