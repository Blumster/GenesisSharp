using System;

namespace Genesis.Shared.Entities
{
    using Clonebase;
    using Database;
    using Database.DataStructs;
    using Structures;
    using Structures.Specifics;

    public class Armor : SimpleObject
    {
        private ArmorSpecific _armorspec;
        public ArmorSpecific ArmorSpecific
        {
            get { return _armorspec; }
            private set { _armorspec = value; }
        }

        private float _varianceArmorFactor;
        private float _varianceResistance;
        private ushort _varianceDefenseBonus;
        private ushort _storedVarianceDefenseBonus;

        public Armor()
        {
            _varianceArmorFactor = 0.0f;
            _varianceResistance = 0.0f;
            _varianceDefenseBonus = 0;
            _storedVarianceDefenseBonus = 0;
        }

        public override void InitializeBaseData()
        {
            base.InitializeBaseData();

            if (CloneBaseObject is CloneBaseArmor)
                ArmorSpecific = (CloneBaseObject as CloneBaseArmor).ArmorSpecific;
        }

        public override void WriteToCreatePacket(Packet packet, bool extended = false)
        {
            base.WriteToCreatePacket(packet, extended);

            #region Create Armor
            ArmorSpecific.WriteToPacket(packet);

            packet.WriteSingle(CloneBaseObject.SimpleObjectSpecific.Mass);
            packet.WriteUtf8StringOn(MangledName, 100);
            packet.WriteShort(_varianceDefenseBonus);

            packet.WritePadding(2);
            #endregion Create Armor
        }

        public static void WriteEmptyObjectToPacket(Packet packet)
        {
            WriteEmptyObjectToPacket(packet, 128);
        }

        public void ApplyResistanceModification(DamageArray dmg, bool reverse)
        {
            for (var i = 0; i < 6; ++i)
            {
                if (reverse)
                    ArmorSpecific.Resistances.Damage[i] -= dmg.Damage[i];
                else
                    ArmorSpecific.Resistances.Damage[i] += dmg.Damage[i];
            }
        }

        public void ApplyResistancePercentage(float percent)
        {
            if (percent <= 0.99000001f || percent >= 1.01)
            {
                for (var i = 0; i < 6; ++i)
                {
                    ArmorSpecific.Resistances.Damage[i] = (short)Math.Floor(ArmorSpecific.Resistances.Damage[i] * percent);

                    if (ArmorSpecific.Resistances.Damage[i] < 0)
                        ArmorSpecific.Resistances.Damage[i] = 0;
                }
            }
        }

        public void ApplyArmorFactorPrecentage(float pct)
        {

        }

        public void ApplyArmorFactorAdjust(short adjust)
        {
            _armorspec.ArmorFactor += adjust;

            if (ArmorSpecific.ArmorFactor < 1)
                _armorspec.ArmorFactor = 1;
        }

        public DamageArray GetResistances()
        {
            return ArmorSpecific.Resistances;
        }

        public void ApplyMassPercent(float pct)
        {
            GameMass *= pct;
        }

        public override Armor GetAsArmor()
        {
            return this;
        }

        public override void SaveToDB()
        {
            var id = new ItemData
            {
                Coid = COID.Coid,
                Cbid = CBID,
                TableName = "item_armor",
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
            var id = DataAccess.Item.GetItemFrom("item_armor", coid);
            if (id == null)
                return false;

            SetCOID(id.Coid);
            InitializeFromCBID(id.Cbid, null);

            IsInDB = true;

            return true;
        }
    }
}
