namespace Genesis.Shared.Entities
{
    using Clonebase;
    using Database;
    using Database.DataStructs;
    using Structures;

    public class Weapon : SimpleObject
    {
        private bool _inventoryBody;
        private bool _lightOn;
        private bool _lightInitialized;
        private bool _equipped;
        private bool _weaponDisabled;
        private bool _isFiring;
        private bool _wasFiring;
        private bool _firingRequest;
        private bool _continousFiring;
        private bool _recharging;
        private bool _alwaysHits;
        private short _frameCount;
        private short _heatPerShot;
        private short _powerPerShot;
        private short _varianceOffensiveBonus;
        private short _storedVarianceOffensiveBonus;
        private short _prefixPenetrationBonus;
        private byte _flags;
        private int _rechargeInterval;
        private float _rechargeTimePassed;
        private float _validArc;
        private float _explosionRadius;
        private float _rangeMinimum;
        private float _rangeMaximum;
        private float _damageScalar;
        private float _varianceRange;
        private float _varianceRefireRate;
        private float _varianceDamageMinimum;
        private float _varianceDamageMaximum;
        private float _prefixAccurayBonus;
        private float _prefixOffensiveBonus;
        private float _hitBonusPerLevel;
        private float _damageBonusPerLevel;
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

        public override void WriteToCreatePacket(Packet packet, bool extended = false)
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

        public override bool LoadFromDB(long coid)
        {
            var id = DataAccess.Item.GetItemFrom("item_weapon", coid);
            if (id == null)
                return false;

            SetCOID(id.Coid);
            InitializeFromCBID(id.Cbid, null);

            IsInDB = true;

            return true;
        }

        public bool IsHeatOk()
        {
            return true;
        }
    }
}
