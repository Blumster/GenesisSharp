using System;
using System.Net;
using System.Net.Sockets;

namespace Genesis.Shared.Database.Tables
{
    using DataStructs;
    using Utils;

    public class Account
    {
        public AccountData LoginAccount(string accName, string password)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"SELECT `Id`, `UserName`, `Password`, `OneTimeKey`, `Level`, `FirstFlag1`, `FirstFlag2`, `FirstFlag3`, `FirstFlag4` FROM `account` WHERE `Username` = '{accName}' AND `Password` = '{password}' LIMIT 1;"))
                        using (var reader = comm.ExecuteReader())
                            return AccountData.Read(reader);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in LoginAccount(String, String)! Exception: {0}", LogType.Error, e);
            }

            return null;
        }

        public AccountData LoginAccount(uint oneTimeKey)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"SELECT `Id`, `UserName`, `Password`, `OneTimeKey`, `Level`, `FirstFlag1`, `FirstFlag2`, `FirstFlag3`, `FirstFlag4` FROM `account` WHERE `OneTimeKey` = '{oneTimeKey}' LIMIT 1;"))
                        using (var reader = comm.ExecuteReader())
                            return AccountData.Read(reader);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in LoginAccount(UInt32)! Exception: {0}", LogType.Error, e);
            }

            return null;
        }

        public void UpdateAccountValues(ulong accId, uint oneTimeKey, uint sessId1, uint sessId2, IPAddress ipa)
        {
            try
            {
                var off = ipa.AddressFamily == AddressFamily.InterNetworkV6 ? 12 : 0;
                var lastIp =
                    $"{ipa.GetAddressBytes()[off + 0]}.{ipa.GetAddressBytes()[off + 1]}.{ipa.GetAddressBytes()[off + 2]}.{ipa.GetAddressBytes()[off + 3]}";

                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"UPDATE `account` SET `OneTimeKey` = {oneTimeKey}, `SessionId1` = {sessId1}, `SessionId2` = {sessId2}, `LastIP` = '{lastIp}' WHERE `Id` = {accId}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateAccountValues! Exception: {0}", LogType.Error, e);
            }
        }

        public void UpdateFirstTimeFlags(ulong accId, uint[] flags)
        {
            if (flags == null || flags.Length != 4)
                return;

            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"UPDATE `account` SET `FirstFlag1` = {flags[0]}, `FirstFlag2` = {flags[1]}, `FirstFlag3` = {flags[2]}, `FirstFlag4` = {flags[3]} WHERE `Id` = {accId}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateFirstTimeFlags! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
