using System;
using System.Collections.Generic;

using UniversalAuth.Data;

namespace Genesis.Shared.Database.Tables
{
    using DataStructs;
    using Utils;

    public class Realmlist
    {
        public List<ServerInfoEx> GetGlobalServers()
        {
            var l = new List<ServerInfoEx>();

            try
            {
                lock (DataAccess.DatabaseAccess)
                { 
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand("SELECT `Id`, `Address`, `Port`, `AgeLimit`, `PKFlag`, `CurrentPlayers`, `MaxPlayers`, `Status` FROM `realmlist_global` LIMIT 16;"))
                    {
                        using (var reader = comm.ExecuteReader())
                        {
                            while (true)
                            {
                                var gsd = reader.ReadServerInfoEx();
                                if (gsd == null)
                                    break;

                                l.Add(gsd);
                            }
                        }
                    }
}
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetGlobalServers! Exception: {0}", LogType.Error, e);
            }
            
            return l;
        }

        public ServerInfoEx GetGlobalServerData(byte sId)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"SELECT `Id`, `Address`, `Port`, `AgeLimit`, `PKFlag`, `CurrentPlayers`, `MaxPlayers`, `Status` FROM `realmlist_global` WHERE `Id` = {sId}"))
                        using (var reader = comm.ExecuteReader())
                            return reader.ReadServerInfoEx();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetGlobalServerData! Exception: {0}", LogType.Error, e);
            }

            return null;
        }

        public SectorServerData GetSectorServerData(byte sId)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"SELECT `Id`, `Address`, `Port`, `CurrentPlayers`, `Status` FROM `realmlist_sector` WHERE `Id` = {sId}"))
                        using (var reader = comm.ExecuteReader())
                            return SectorServerData.Read(reader);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetSectorServerData! Exception: {0}", LogType.Error, e);
            }

            return null;
        }

        public void UpdateGlobalStatus(byte sId, ushort currPlayers, byte status)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"UPDATE `realmlist_global` SET `Status` = {status}, `CurrentPlayers` = {currPlayers} WHERE `Id` = {sId}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateGlobalStatus! Exception: {0}", LogType.Error, e);
            }
        }

        public void UpdateSectorStatus(byte sId, ushort currPlayers, byte status)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"UPDATE `realmlist_sector` SET `Status` = {status}, `CurrentPlayers` = {currPlayers} WHERE `Id` = {sId}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateSectorStatus! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
