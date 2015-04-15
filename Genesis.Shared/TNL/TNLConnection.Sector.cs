using System;
using System.Threading;

using TNL.NET.Entities;

namespace Genesis.Shared.TNL
{
    using Constant;
    using Entities;
    using Manager;

    public partial class TNLConnection
    {
        public void HandleTransferFromGlobal(Packet packet)
        {
            var pack = new Packet(Opcode.MapInfo, false);
                
            var oneTimeKey = packet.ReadUInteger();

            var charcoid = packet.ReadLong();
            if (charcoid == -1L)
            {
                Disconnect("Invalid Character!");
                return;
            }


            if (!LoginAccount(oneTimeKey))
            {
                Disconnect("Invalid Account!");
                return;
            }

            var character = new Character();
            if (!character.LoadFromDB(charcoid))
            {
                Disconnect("Unable to load Character!");
                return;
            }

            CurrentCharacter = character;
            CurrentCharacter.SetOwner(this);

            var map = MapManager.GetMap(character.GetMapId());
            if (map != null)
            {
                map.WritePacket(pack);

                character.SetMap(map);

                SendPacket(pack, RPCGuaranteeType.RPCGuaranteedOrdered);
            }
            else
                Disconnect("Unable to send map!");
        }

        private void HandleTransferFromGlobalStage2(Packet packet)
        {
            var secuKey = packet.ReadUInteger();
            var coidChar = packet.ReadULong();

            Thread.Sleep(5000);

            var pack = new Packet(Opcode.TransferFromGlobalStage3);
            
            pack.WriteInteger(secuKey);
            pack.WriteLong(coidChar);
            pack.WriteSingle(CurrentCharacter.GetPositionX()); // X
            pack.WriteSingle(CurrentCharacter.GetPositionY()); // Y
            pack.WriteSingle(CurrentCharacter.GetPositionZ()); // Z
            pack.WritePadding(4);

            SendPacket(pack, RPCGuaranteeType.RPCGuaranteedOrdered);
        }

        private void HandleTransferFromGlobalStage3(Packet packet)
        {
// ReSharper disable UnusedVariable
            var securityKey = packet.ReadUInteger();
            var coidCharacter = packet.ReadULong();
            var coordX = packet.ReadSingle();
            var coordY = packet.ReadSingle();
            var coordZ = packet.ReadSingle();
// ReSharper restore UnusedVariable

            var pack = new Packet(Opcode.CreateVehicleExtended);
            
            CurrentCharacter.GetVehicle().WriteToCreatePacket(pack, true);

            SendPacket(pack, RPCGuaranteeType.RPCGuaranteedOrdered);
            
            pack = new Packet(Opcode.CreateCharacterExtended);
            
            CurrentCharacter.WriteToCreatePacket(pack, true);

            SendPacket(pack, RPCGuaranteeType.RPCGuaranteedOrdered);
            
            CurrentCharacter.EnterMap();
        }

        public void HandleUpdateFirstTimeFlagsRequest(Packet packet)
        {
            UpdateFirstTimeFlags(packet.ReadUInteger(), packet.ReadUInteger(), packet.ReadUInteger(), packet.ReadUInteger());
        }
    }
}
