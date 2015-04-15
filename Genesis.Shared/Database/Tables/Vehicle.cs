using System;

namespace Genesis.Shared.Database.Tables
{
    using DataStructs;
    using Utils;

    public class Vehicle
    {
        public VehicleData GetVehicle(Int64 coid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("SELECT {0}  FROM `vehicle` WHERE `Coid` = {1}", VehicleData.GetQueryString(), coid)))
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
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("INSERT INTO `vehicle` ({0}) VALUES ({1})", VehicleData.GetQueryString(), data.GetInsertString())))
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
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("UPDATE `vehicle` SET {0} WHERE `Coid` = {1}", data.GetUpdateString(), data.Coid)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in UpdateVehicle! Exception: {0}", LogType.Error, e);
            }
        }

        public void DeleteVehicle(Int64 ownerCoid)
        {
            try
            {
                lock (DataAccess.DatabaseAccess)
                    using (var comm = DataAccess.DatabaseAccess.CreateCommand(String.Format("DELETE FROM `vehicle` WHERE `OwnerCoid` = {0}", ownerCoid)))
                        comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception in DeleteVehicle! Exception: {0}", LogType.Error, e);
            }
        }
    }
}
