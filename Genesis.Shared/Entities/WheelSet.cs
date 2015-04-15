using System;

namespace Genesis.Shared.Entities
{
    using Clonebase;
    using Database;
    using Database.DataStructs;

    public class WheelSet : SimpleObject
    {
        private Byte _numOfWheels;
        private Single _frictionGravel;
        private Single _frictionIce;
        private Single _frictionMud;
        private Single _frictionPaved;
        private Single _frictionPlains;
        private Single _frictionSand;
        private Boolean _isDefault;
        private Int32 _numAxles;
        private Boolean _wheelsVisible;

        public WheelSet()
        {
            _numOfWheels = 0;
            _isDefault = false;
            _numAxles = 0;
            _wheelsVisible = true;
        }

        public override void InitializeBaseData()
        {
            base.InitializeBaseData();

            if (!(CloneBaseObject is CloneBaseWheelSet))
                return;

            _frictionGravel = (CloneBaseObject as CloneBaseWheelSet).WheelSetSpecific.Friction[0];
            _frictionIce = (CloneBaseObject as CloneBaseWheelSet).WheelSetSpecific.Friction[1];
            _frictionMud = (CloneBaseObject as CloneBaseWheelSet).WheelSetSpecific.Friction[2];
            _frictionPaved = (CloneBaseObject as CloneBaseWheelSet).WheelSetSpecific.Friction[3];
            _frictionPlains = (CloneBaseObject as CloneBaseWheelSet).WheelSetSpecific.Friction[4];
            _frictionSand = (CloneBaseObject as CloneBaseWheelSet).WheelSetSpecific.Friction[5];
            _numAxles = (CloneBaseObject as CloneBaseWheelSet).WheelSetSpecific.NumWheelsAxle[0] + (CloneBaseObject as CloneBaseWheelSet).WheelSetSpecific.NumWheelsAxle[1];
        }

        public override void WriteToCreatePacket(Packet packet, Boolean extended = false)
        {
            base.WriteToCreatePacket(packet, extended);

            #region Create Wheel Set
            packet.WriteSingle(_frictionGravel); // friction gravel
            packet.WriteSingle(_frictionIce); // friction ice
            packet.WriteSingle(_frictionMud); // friction mud
            packet.WriteSingle(_frictionPaved); // friction paved
            packet.WriteSingle(_frictionPlains); // friction plains
            packet.WriteSingle(_frictionSand); // friction sand
            packet.WriteBoolean(_isDefault); // is default
            packet.WriteUtf8StringOn(MangledName, 100); // name
            packet.WritePadding(3);
            #endregion Create Wheel Set
        }

        public override WheelSet GetAsWheelSet()
        {
            return this;
        }

        public override void SaveToDB()
        {
            var id = new ItemData
            {
                Coid = COID.Coid,
                Cbid = CBID,
                TableName = "item_wheelset",
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
            var id = DataAccess.Item.GetItemFrom("item_wheelset", coid);
            if (id == null)
                return false;

            SetCOID(id.Coid);
            InitializeFromCBID(id.Cbid, null);

            IsInDB = true;

            return true;
        }
    }
}
