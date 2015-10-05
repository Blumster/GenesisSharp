using System;

namespace Genesis.Shared.Entities
{
    using Database;
    using Database.DataStructs;
    using Structures.Specifics;

    public class PowerPlant : SimpleObject
    {
        private PowerPlantSpecific _powerPlantData;
        private float _coolRateVariance;
        private float _maximumHeatVariance;
        private float _maximumPowerVariance;
        private float _powerRegenRateVariance;
        private float _skillCooldownPercent;

        public PowerPlant()
        {
            _coolRateVariance = 1.0f;
            _maximumHeatVariance = 1.0f;
            _maximumPowerVariance = 1.0f;
            _powerRegenRateVariance = 1.0f;
            _skillCooldownPercent = 1.0f;

        }

        public override void InitializeBaseData()
        {
            base.InitializeBaseData();

            if (CloneBaseObject is Clonebase.CloneBasePowerPlant)
                _powerPlantData = (CloneBaseObject as Clonebase.CloneBasePowerPlant).PowerPlantSpecific;
        }

        public void ApplyCoolingRate()
        {
            _powerPlantData.CoolRate = (short)Math.Floor(_powerPlantData.CoolRate * _coolRateVariance);
        }

        public void ApplyCoolingRatePercentage(float f)
        {
            _powerPlantData.CoolRate = (short)Math.Ceiling(_powerPlantData.CoolRate * f);
        }

        public void ApplyMaximumHeat()
        {
            _powerPlantData.HeatMaximum = (int)Math.Floor(_powerPlantData.HeatMaximum * _maximumHeatVariance);
        }

        public void ApplyMaximumHeatPercentage(float f)
        {
            _powerPlantData.HeatMaximum = (int)Math.Ceiling(_powerPlantData.HeatMaximum * f);
        }

        public void ApplyMaximumPower()
        {
            _powerPlantData.PowerMaximum = (int)Math.Floor(_powerPlantData.PowerMaximum * _maximumPowerVariance);
        }

        public void ApplyMaximumPowerPercentage(float f)
        {
            _powerPlantData.PowerMaximum = (int)Math.Ceiling(_powerPlantData.PowerMaximum * f);
        }

        public void ApplyPowerRegenRate()
        {
            _powerPlantData.PowerRegenRate = (short)Math.Floor(_powerPlantData.PowerRegenRate * _powerRegenRateVariance);
        }

        public void ApplyPowerRegenRatePercentage(float f)
        {
            _powerPlantData.PowerRegenRate = (short)Math.Ceiling(_powerPlantData.PowerRegenRate * f);
        }

        public void ApplyVariances()
        {
            ApplyCoolingRate();
            ApplyMaximumHeat();
            ApplyMaximumPower();
            ApplyPowerRegenRate();
        }

        public override void WriteToCreatePacket(Packet packet, bool extended = false)
        {
            base.WriteToCreatePacket(packet, extended);

            #region Create Power Plant
            _powerPlantData.WriteToPacket(packet);

            packet.WriteSingle(CloneBaseObject.SimpleObjectSpecific.Mass);
            packet.WriteUtf8StringOn(MangledName, 100);
            packet.WriteSingle(_skillCooldownPercent);
            #endregion Create Power Plant
        }

        public static void WriteEmptyObjectToPacket(Packet packet)
        {
            WriteEmptyObjectToPacket(packet, 120);
        }

        public override PowerPlant GetAsPowerPlant()
        {
            return this;
        }

        public override void SaveToDB()
        {
            var id = new ItemData
            {
                Coid = COID.Coid,
                Cbid = CBID,
                TableName = "item_powerplant",
            };

            if (IsInDB)
                DataAccess.Item.UpdateItemInto(id);
            else
            {
                DataAccess.Item.InsertItemInto(id);
                IsInDB = true;
            }
        }

        public override bool LoadFromDB(long coid)
        {
            var id = DataAccess.Item.GetItemFrom("item_powerplant", coid);
            if (id == null)
                return false;

            SetCOID(id.Coid);
            InitializeFromCBID(id.Cbid, null);

            IsInDB = true;

            return true;
        }
    }
}
