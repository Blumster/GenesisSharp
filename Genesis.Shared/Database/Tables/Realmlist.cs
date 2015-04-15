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
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("SELECT `Id`, `Address`, `Port`, `AgeLimit`, `PKFlag`, `CurrentPlayers`, `MaxPlayers`, `Status` FROM `realmlist_global` LIMIT 16;")))
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

        public ServerInfoEx GetGlobalServerData(Byte sId)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("SELECT `Id`, `Address`, `Port`, `AgeLimit`, `PKFlag`, `CurrentPlayers`, `MaxPlayers`, `Status` FROM `realmlist_global` WHERE `Id` = {0}", sId)))
                    using (var reader = comm.ExecuteReader())
                        return reader.ReadServerInfoEx();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetGlobalServerData! Exception: {0}", LogType.Error, e);
            }

            return null;
        }

        public SectorServerData GetSectorServerData(Byte sId)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("SELECT `Id`, `Address`, `Port`, `CurrentPlayers`, `Status` FROM `realmlist_sector` WHERE `Id` = {0}", sId)))
                        using (var reader = comm.ExecuteReader())
                            return SectorServerData.Read(reader);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetSectorServerData! Exception: {0}", LogType.Error, e);
            }

            return null;
        }

        public void UpdateGlobalStatus(Byte sId, UInt16 currPlayers, Byte status)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("UPDATE `realmlist_global` SET `Status` = {0}, `CurrentPlayers` = {1} WHERE `Id` = {2}", status, currPlayers, sId)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateGlobalStatus! Exception: {0}", LogType.Error, e);
            }
        }

        public void UpdateSectorStatus(Byte sId, UInt16 currPlayers, Byte status)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("UPDATE `realmlist_sector` SET `Status` = {0}, `CurrentPlayers` = {1} WHERE `Id` = {2}", status, currPlayers, sId)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateSectorStatus! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
