using System;

namespace Genesis.Shared.Entities
{
    using Clonebase;
    using Database;
    using Database.DataStructs;
    using Structures;

    public class Weapon : SimpleObject
    {
        private Boolean _inventoryBody;
        private Boolean _lightOn;
        private Boolean _lightInitialized;
        private Boolean _equipped;
        private Boolean _weaponDisabled;
        private Boolean _isFiring;
        private Boolean _wasFiring;
        private Boolean _firingRequest;
        private Boolean _continousFiring;
        private Boolean _recharging;
        private Boolean _alwaysHits;
        private Int16 _frameCount;
        private Int16 _heatPerShot;
        private Int16 _powerPerShot;
        private Int16 _varianceOffensiveBonus;
        private Int16 _storedVarianceOffensiveBonus;
        private Int16 _prefixPenetrationBonus;
        private Byte _flags;
        private Int32 _rechargeInterval;
        private Single _rechargeTimePassed;
        private Single _validArc;
        private Single _explosionRadius;
        private Single _rangeMinimum;
        private Single _rangeMaximum;
        private Single _damageScalar;
        private Single _varianceRange;
        private Single _varianceRefireRate;
        private Single _varianceDamageMinimum;
        private Single _varianceDamageMaximum;
        private Single _prefixAccurayBonus;
        private Single _prefixOffensiveBonus;
        private Single _hitBonusPerLevel;
        private Single _damageBonusPerLevel;
        private Vector3 _firePoint;
        private DamageArray _dmgMinimum;
        private DamageArray _dmgMaximum;

        public Weapon()
        {
            _validArc = -1.0f;
            _equipped = false;
            _weaponDisabled = false;
            _isFiring = false;
            _wasFiring = false;
            _firingRequest = false;
            _continousFiring = false;
            _recharging = false;
            _alwaysHits = false;
            _rechargeInterval = 0;
            _heatPerShot = 0;
            _powerPerShot = 0;
            _explosionRadius = 0.0f;
            _rangeMinimum = 0.0f;
            _rangeMaximum = 0.0f;
            _damageScalar = 1.0f;
            _varianceRange = 0.0f;
            _varianceRefireRate = 0.0f;
            _varianceDamageMinimum = 0.0f;
            _varianceDamageMaximum = 0.0f;
            _varianceOffensiveBonus = 0;
            _prefixAccurayBonus = 0.0f;
            _prefixOffensiveBonus = 0.0f;
            _prefixPenetrationBonus = 0;
            _hitBonusPerLevel = 0.0f;
            _damageBonusPerLevel = 0.0f;
            _firePoint.X = 0.0f;
            _firePoint.Y = 0.0f;
            _firePoint.Z = 0.0f;
            _inventoryBody = false;
            _lightOn = false;
            _lightInitialized = false;
            _frameCount = 0;
        }

        public override void InitializeBaseData()
        {
            base.InitializeBaseData();

            if (!(CloneBaseObject is CloneBaseWeapon))
                return;

            var cbw = (CloneBaseObject as CloneBaseWeapon);

            _damageBonusPerLevel = cbw.WeaponSpecific.DamageBonusPerLevel;
            _dmgMinimum = cbw.WeaponSpecific.MinMin;
            _dmgMaximum = cbw.WeaponSpecific.MaxMax;
            _rangeMaximum = cbw.WeaponSpecific.RangeMax;
            _rangeMinimum = cbw.WeaponSpecific.RangeMin;
            _prefixPenetrationBonus = cbw.WeaponSpecific.PenetrationModifier;
            _varianceOffensiveBonus = cbw.WeaponSpecific.OffenseBonus;
            _flags = cbw.WeaponSpecific.Flags;
            _validArc = cbw.WeaponSpecific.ValidArc;
            _rechargeInterval = cbw.WeaponSpecific.RechargeTime;
            _hitBonusPerLevel = cbw.WeaponSpecific.HitBonusPerLevel;
            _prefixAccurayBonus = cbw.WeaponSpecific.AccucaryModifier;
            _damageScalar = cbw.WeaponSpecific.DamageScalar;
            _varianceDamageMinimum = cbw.WeaponSpecific.DmgMinMin;
            _varianceDamageMaximum = cbw.WeaponSpecific.DmgMaxMax;
            _explosionRadius = cbw.WeaponSpecific.ExplosionRadius;
            _firePoint = cbw.WeaponSpecific.FirePoint;
        }

        public override void WriteToCreatePacket(Packet packet, Boolean extended = false)
        {
            base.WriteToCreatePacket(packet, extended);

            #region Create Weapon
            packet.WriteSingle(_varianceRange);
            packet.WriteSingle(_varianceRefireRate);
            packet.WriteSingle(_varianceDamageMinimum);
            packet.WriteSingle(_varianceDamageMaximum);
            packet.WriteShort(_varianceOffensiveBonus);

            packet.WritePadding(2);

            packet.WriteSingle(_prefixAccurayBonus);
            packet.WriteShort(_prefixPenetrationBonus);

            packet.WritePadding(2);

            packet.WriteInteger(_rechargeInterval);
            packet.WriteSingle(CloneBaseObject.SimpleObjectSpecific.Mass);
            packet.WriteSingle(_rangeMinimum);
            packet.WriteSingle(_rangeMaximum);
            packet.WriteSingle(_validArc);

            _dmgMinimum.WriteToPacket(packet);
            _dmgMaximum.WriteToPacket(packet);

            packet.WriteUtf8StringOn("", 100);

            packet.WritePadding(4);
            #endregion Create Weapon
        }

        public static void WriteEmptyObjectToPacket(Packet packet)
        {
            WriteEmptyObjectToPacket(packet, 176);
        }

        public override Weapon GetAsWeapon()
        {
            return this;
        }

        public override void SaveToDB()
        {
            var id = new ItemData
            {
                Coid = COID.Coid,
                Cbid = CBID,
                TableName = "item_weapon",
            };

            if (IsInDB)
                DataAccess.Item.UpdateItemInto(id);
            else
            {
                DataAccess.Item.InsertItemInto(id);
                IsInDB = true;
            }
        }

        public override bool LoadFromDB(Int64 coid)
        {
            var id = DataAccess.Item.GetItemFrom("item_weapon", coid);
            if (id == null)
                return false;

            SetCOID(id.Coid);
            InitializeFromCBID(id.Cbid, null);

            IsInDB = true;

            return true;
        }

        public Boolean IsHeatOk()
        {
            return true;
        }
    }
}
