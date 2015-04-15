using System;
using System.Net;
using System.Net.Sockets;

namespace Genesis.Shared.Database.Tables
{
    using DataStructs;
    using Utils;

    public class Account
    {
        public AccountData LoginAccount(String accName, String password)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("SELECT `Id`, `UserName`, `Password`, `OneTimeKey`, `Level`, `FirstFlag1`, `FirstFlag2`, `FirstFlag3`, `FirstFlag4` FROM `account` WHERE `Username` = '{0}' AND `Password` = '{1}' LIMIT 1;", accName, password)))
                        using (var reader = comm.ExecuteReader())
                            return AccountData.Read(reader);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in LoginAccount(String, String)! Exception: {0}", LogType.Error, e);
            }

            return null;
        }

        public AccountData LoginAccount(UInt32 oneTimeKey)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("SELECT `Id`, `UserName`, `Password`, `OneTimeKey`, `Level`, `FirstFlag1`, `FirstFlag2`, `FirstFlag3`, `FirstFlag4` FROM `account` WHERE `OneTimeKey` = '{0}' LIMIT 1;", oneTimeKey)))
                        using (var reader = comm.ExecuteReader())
                            return AccountData.Read(reader);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in LoginAccount(UInt32)! Exception: {0}", LogType.Error, e);
            }

            return null;
        }

        public void UpdateAccountValues(UInt64 accId, UInt32 oneTimeKey, UInt32 sessId1, UInt32 sessId2, IPAddress ipa)
        {
            try
            {
                var off = ipa.AddressFamily == AddressFamily.InterNetworkV6 ? 12 : 0;
                var lastIp = String.Format("{0}.{1}.{2}.{3}", ipa.GetAddressBytes()[off + 0], ipa.GetAddressBytes()[off + 1], ipa.GetAddressBytes()[off + 2], ipa.GetAddressBytes()[off + 3]);

                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("UPDATE `account` SET `OneTimeKey` = {0}, `SessionId1` = {1}, `SessionId2` = {2}, `LastIP` = '{3}' WHERE `Id` = {4}", oneTimeKey, sessId1, sessId2, lastIp, accId)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateAccountValues! Exception: {0}", LogType.Error, e);
            }
        }

        public void UpdateFirstTimeFlags(UInt64 accId, UInt32[] flags)
        {
            if (flags == null || flags.Length != 4)
                return;

            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("UPDATE `account` SET `FirstFlag1` = {0}, `FirstFlag2` = {1}, `FirstFlag3` = {2}, `FirstFlag4` = {3} WHERE `Id` = {4}", flags[0], flags[1], flags[2], flags[3], accId)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateFirstTimeFlags! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
