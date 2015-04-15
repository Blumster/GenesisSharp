using System;
using System.Collections.Generic;

namespace Genesis.Shared.Database.Tables
{
    using DataStructs;
    using Utils;

    public class Character
    {
        public CharacterData GetCharacter(Int64 coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("SELECT {0} FROM `character` WHERE `Coid` = {1}", CharacterData.GetQueryString(), coid)))
                        using (var reader = comm.ExecuteReader())
                            return CharacterData.Read(reader);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetCharacter! Exception: {0}", LogType.Error, e);
            }
            return null;
        }

        public IDictionary<Int64, CharacterData> GetCharacters(UInt64 accId)
        {
            var dict = new Dictionary<Int64, CharacterData>();

            try
            {
                lock (DataAccess.DatabaseAccess)
                {
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("SELECT {0} FROM `character` WHERE `AccountId` = {1} ORDER BY `Coid` ASC LIMIT 16", CharacterData.GetQueryString(), accId)))
                    {
                        using (var reader = comm.ExecuteReader())
                        {
                            while (true)
                            {
                                var c = CharacterData.Read(reader);
                                if (c == null)
                                    break;

                                dict.Add(c.Coid, c);
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetCharacters! Exception: {0}", LogType.Error, e);
            }
            return dict;
        }

        public void DeleteCharacter(UInt64 accId, Int64 charCoid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("DELETE FROM `character` WHERE `Coid` = {0} AND `AccountId` = {1}", charCoid, accId)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in DeleteCharacter! Exception: {0}", LogType.Error, e);
            }
        }

        public void InsertCharacter(CharacterData data)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("INSERT INTO `character` ({0}) VALUES ({1})", CharacterData.GetQueryString(), data.GetInsertString())))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in InsertCharacter! Exception: {0}", LogType.Error, e);
            }
        }

        public void UpdateCharacter(CharacterData data)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("UPDATE `character` SET {0} WHERE `Coid` = {1} AND `AccountId` = {2}", data.GetUpdateString(), data.Coid, data.AccountId)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateCharacter! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
