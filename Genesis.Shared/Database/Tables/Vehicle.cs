using System;

namespace Genesis.Shared.Database.Tables
{
    using DataStructs;
    using Utils;

    public class Vehicle
    {
        public VehicleData GetVehicle(long coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"SELECT {VehicleData.GetQueryString()}  FROM `vehicle` WHERE `Coid` = {coid}"))
                        using (var reader = comm.ExecuteReader())
                            return VehicleData.Read(reader);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in GetVehicle! Exception: {0}", LogType.Error, e);
            }

            return null;
        }

        public void InsertVehicle(VehicleData data)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"INSERT INTO `vehicle` ({VehicleData.GetQueryString()}) VALUES ({data.GetInsertString()})"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in InsertVehicle! Exception: {0}", LogType.Error, e);
            }
        }

        public void UpdateVehicle(VehicleData data)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"UPDATE `vehicle` SET {data.GetUpdateString()} WHERE `Coid` = {data.Coid}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateVehicle! Exception: {0}", LogType.Error, e);
            }
        }

        public void DeleteVehicle(long ownerCoid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand($"DELETE FROM `vehicle` WHERE `OwnerCoid` = {ownerCoid}"))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in DeleteVehicle! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
