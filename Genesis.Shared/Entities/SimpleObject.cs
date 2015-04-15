using System;
using System.Diagnostics;
using System.IO;
using Genesis.Shared.Database;

namespace Genesis.Shared.Entities
{
    using Base;
    using Constant;
    using Database.DataStructs;
    using Structures;

    public class SimpleObject : ClonedObjectBase
    {
        protected Int32[] Prefixes;
        protected Int32[] Gadgets;
        protected Int32 MaxGadgets;
        protected Int32 TeamFaction;
        protected Int32 Quantity;
        protected UInt32 HP;
        protected UInt32 MaxHP;
        protected Int32 ItemTemplateId;
        protected Byte InventoryPositionX;
        protected Byte InventoryPositionY;
        protected Byte SkillLevel1;
        protected Byte SkillLevel2;
        protected Byte SkillLevel3;
        protected Boolean AlreadyAssembled;
        protected Boolean IsInDB;

        public SimpleObject()
        {
            MaxGadgets = 0;
            TeamFaction = 0;
            HP = 0;
            MaxHP = 500;
            InventoryPositionX = 0;
            InventoryPositionY = 0;
            AlreadyAssembled = false;
            Quantity = 1;
            ItemTemplateId = -1;
            SkillLevel1 = 1;
            SkillLevel2 = 1;
            SkillLevel3 = 1;
            CustomizedName = "";
            IsInDB = false;
        }

        public override void InitializeBaseData()
        {
            base.InitializeBaseData();

            MaxHP = (UInt32)CloneBaseObject.SimpleObjectSpecific.MaxHitPoint;
            HP = (UInt32)CloneBaseObject.SimpleObjectSpecific.MinHitPoints;
            TeamFaction = CloneBaseObject.SimpleObjectSpecific.Faction;
        }

        public override Int32 GetQuantity()
        {
            return Quantity;
        }

        public override void Unserialize(BinaryReader br, uint mapVersion)
        {
        }

        public override void WriteToCreatePacket(Packet packet, Boolean extended = false)
        {
            packet.WriteInteger(CBID);
            packet.WriteLong(-1L); // coid Store
            packet.WriteInteger(GetCurrentHP() + 100);
            packet.WriteInteger(GetMaximumHP() + 100);
            packet.WriteInteger(GetValue());
            packet.WriteInteger(GetIDFaction());
            packet.WriteInteger(TeamFaction);
            packet.WriteInteger(CustomValue);

            for (var i = 0; i < 5; ++i) // prefix id
                packet.WriteInteger(-1);

            for (var i = 0; i < 5; ++i) // gadget id
                packet.WriteInteger(-1);

            for (var i = 0; i < 5; ++i) // prefix level
                packet.WriteShort(0);

            for (var i = 0; i < 5; ++i) // gadget level
                packet.WriteShort(0);

            packet.WriteSingle(Position.X);
            packet.WriteSingle(Position.Y);
            packet.WriteSingle(Position.Z);
            packet.WriteSingle(Rotation.X);
            packet.WriteSingle(Rotation.Y);
            packet.WriteSingle(Rotation.Z);
            packet.WriteSingle(Rotation.W);
            packet.WriteSingle(Scale);
            packet.WriteInteger(GetQuantity());
            packet.WriteByte(InventoryPositionX);
            packet.WriteByte(InventoryPositionY);
            packet.WriteBoolean(GetIsCorpse()); // is corpse

            packet.WritePadding(5);

            packet.WriteTFID(COID);
            packet.WriteBoolean(false); // will equip
            packet.WriteBoolean(false); // is item link
            packet.WriteBoolean(false); // is in inventory
            packet.WriteByte(SkillLevel1);
            packet.WriteByte(SkillLevel2);
            packet.WriteByte(SkillLevel3);
            packet.WriteBoolean(false); // is identified
            packet.WriteBoolean(false); // possible mission item
            packet.WriteBoolean(false); // tempitem
            packet.WriteBoolean((UnkFlags & UnkFlags.IsKit) != 0);
            packet.WriteBoolean(false); // isinfinite
            packet.WriteBoolean((UnkFlags & UnkFlags.IsBound) != 0);
            packet.WriteShort(UsesLeft);
            packet.WriteUtf8StringOn(CustomizedName, 17);
            packet.WriteBoolean(MadeFromMemory);
            packet.WriteBoolean(false); // is mail

            packet.WritePadding(1);

            packet.WriteShort((Int16)MaxGadgets);
            packet.WriteShort((Int16)RequiredLevel);
            packet.WriteShort((Int16)RequiredCombat);
            packet.WriteShort((Int16)RequiredPerception);
            packet.WriteShort((Int16)RequiredTech);
            packet.WriteShort((Int16)RequiredTheory);

            packet.WritePadding(2);

            packet.WriteInteger(ItemTemplateId);

            packet.WritePadding(4);
        }

        public override void SetCurrentHP(UInt32 hp)
        {
            HP = hp;
        }

        public override void SetMaximumHP(UInt32 hp)
        {
            MaxHP = hp;
        }

        public override uint GetCurrentHP()
        {
            return HP;
        }

        public override uint GetMaximumHP()
        {
            return MaxHP;
        }

        public override SimpleObject GetAsSimpleObject()
        {
            return this;
        }

        public virtual void SaveToDB()
        {
            var id = new ItemData
            {
                Coid = COID.Coid,
                Cbid = CBID,
                TableName = "item_simple",
            };

            if (IsInDB)
                DataAccess.Item.UpdateItemInto(id);
            else
            {
                DataAccess.Item.InsertItemInto(id);
                IsInDB = true;
            }
        }

        public virtual Boolean LoadFromDB(Int64 coid)
        {
            var id = DataAccess.Item.GetItemFrom("item_simple", coid);
            if (id == null)
                return false;

            SetCOID(id.Coid);
            InitializeFromCBID(id.Cbid, null);

            IsInDB = true;

            return true;
        }

        public static void WriteEmptyObjectToPacket(Packet packet, Int32 extraSkip = 0)
        {
            packet.WriteInteger(-1); // CBID
            packet.WritePadding(208 + extraSkip);

            // TODO: fill actual empty data maybe?
        }

        public virtual UInt32 GetMapId()
        {
            return 0;
        }

        public virtual void HandleMove(Packet packet)
        {
            var fidObject = packet.ReadPadding(4).ReadTFID();
            Debug.Assert(fidObject == GetTFID(), "A TFID nem egyezik?");

            Position = new Vector3(packet.ReadSingle(), packet.ReadSingle(), packet.ReadSingle());
            Velocity = new Vector3(packet.ReadSingle(), packet.ReadSingle(), packet.ReadSingle());
            Rotation = new Vector4(packet.ReadSingle(), packet.ReadSingle(), packet.ReadSingle(), packet.ReadSingle());
            AngularVelocity = new Vector3(packet.ReadSingle(), packet.ReadSingle(), packet.ReadSingle());

            Console.WriteLine("Pos: {0} | {1} | {2} ", Position.X, Position.Y, Position.Z);
            Console.WriteLine("Rot: {0} | {1} | {2} | {3}", Rotation.X, Rotation.Y, Rotation.Z, Rotation.W);

            var absolute = packet.ReadBoolean();
            var tarLoc = new Vector3(packet.ReadPadding(3).ReadSingle(), packet.ReadSingle(), packet.ReadSingle());

            packet.ReadPadding(4);

            //if (GhostObject != null)
            //GhostObject.SetMaskBits(2UL);
        }
    }
}
