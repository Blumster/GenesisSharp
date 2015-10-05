using System;
using System.Collections.Generic;

namespace Genesis.Shared.Database.Tables
{
    using DataStructs;
    using Utils;

    public class Character
    {
        public CharacterData GetCharacter(long coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"SELECT {CharacterData.GetQueryString()} FROM `character` WHERE `Coid` = {coid}"))
                        using (var reader = comm.ExecuteReader())
                            return CharacterData.Read(reader);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetCharacter! Exception: {0}", LogType.Error, e);
            }
            return null;
        }

        public IDictionary<long, CharacterData> GetCharacters(ulong accId)
        {
            var dict = new Dictionary<long, CharacterData>();

            try
            {
                lock (DataAccess.DatabaseAccess)
                {
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"SELECT {CharacterData.GetQueryString()} FROM `character` WHERE `AccountId` = {accId} ORDER BY `Coid` ASC LIMIT 16"))
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

        public void DeleteCharacter(ulong accId, long charCoid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"DELETE FROM `character` WHERE `Coid` = {charCoid} AND `AccountId` = {accId}"))
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
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"INSERT INTO `character` ({CharacterData.GetQueryString()}) VALUES ({data.GetInsertString()})"))
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
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"UPDATE `character` SET {data.GetUpdateString()} WHERE `Coid` = {data.Coid} AND `AccountId` = {data.AccountId}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateCharacter! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
