using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using TNL.NET.Entities;

namespace Genesis.Shared.TNL
{
    using Constant;
    using Database;
    using Entities;
    using Manager;
    using Structures.Model;
    using Utils;

    public partial class TNLConnection
    {
        private void HandleLoginRequest(Packet packet)
        {
            var user = packet.ReadUtf8StringOn(33);
            var pass = packet.ReadUtf8StringOn(33);

            packet.ReadPadding(2);

            packet.ReadUInteger();
            _oneTimeKey = packet.ReadUInteger();

            if (!LoginAccount(_oneTimeKey, user, pass))
            {
                SendLoginResponse(1);
                Disconnect("Invalid Username or password!");
                return;
            }

            Logger.WriteLog("Client ({3} -> {1} | {2}) authenticated from {0}", LogType.Network, GetNetAddressString(), AccountId, AccountName, _playerCOID);

            var list = DataAccess.Character.GetCharacters(AccountId);

            foreach (var character in from charData in list let character = new Character() where character.LoadFromDB(charData.Value, charData.Key) select character)
            {
                character.SetOwner(this);

                var pack = new Packet(Opcode.CreateCharacter);
                character.WriteToCreatePacket(pack);

                var vpack = new Packet(Opcode.CreateVehicle);
                character.GetVehicle().WriteToCreatePacket(vpack);

                SendPacket(pack, RPCGuaranteeType.RPCGuaranteedOrdered);
                SendPacket(vpack, RPCGuaranteeType.RPCGuaranteedOrdered);
            }

            SendLoginResponse(0x1000000);
        }

        private void SendLoginResponse(Int32 response)
        {
            var packet = new Packet(Opcode.LoginResponse);
            packet.WriteInteger(response);

            SendPacket(packet, RPCGuaranteeType.RPCGuaranteedOrdered);
        }

        private void HandleNews(Packet p)
        {
            var language = p.ReadUInteger();
            p.ReadUInteger();

            const String news = "Welcome everybody to the world first [$emote]Auto Assault Private Server[$/emote]!\nHave fun, and enjoy your stay! :)";

            var pack = new Packet(Opcode.News);
            pack.WriteInteger(language);
            pack.WriteInteger(news.Length + 1);
            pack.WriteUtf8NullString(news);

            SendPacket(pack, RPCGuaranteeType.RPCGuaranteedOrdered);
        }

        private void HandleLoginNewCharacter(Packet packet)
        {
            Int64 coid;
            var succ = CharacterManager.CreateCharacterFromRequest(this, CreateCharacterModel.Read(packet), out coid);

            var pack = new Packet(Opcode.LoginNewCharacterResponse);
            pack.WriteInteger(succ ? 0x80000000 : 0x1);
            pack.WriteLong(coid);

            SendPacket(pack, RPCGuaranteeType.RPCGuaranteedOrdered);
        }

        private void HandleLoginDeleteCharacter(Packet packet)
        {
            packet.ReadUInteger();

            var coid = packet.ReadLong();

            DataAccess.Character.DeleteCharacter(AccountId, coid);
            DataAccess.Clan.RemoveFromClan(coid);
            DataAccess.Convoy.RemoveFromConvoy(coid);
            DataAccess.Social.DeleteEntriesByCharacter(coid);
            DataAccess.Vehicle.DeleteVehicle(coid);
        }

        private void HandleGlobalLogin(Packet packet)
        {
            var coid = packet.ReadPadding(4).ReadLong();
            /*var startSectorOverride = */packet.ReadInteger();

            packet.ReadPadding(4);

            var resp = new Packet(Opcode.LoginAck);

            var character = new Character();
            if (!character.LoadFromDB(null, coid))
            {
                resp.WriteBoolean(false);

                SendPacket(resp, RPCGuaranteeType.RPCGuaranteedOrdered);
                return;
            }

            var data = DataAccess.Realmlist.GetSectorServerData(1);
            if (data == null)
            {
                resp.WriteBoolean(false);

                SendPacket(resp, RPCGuaranteeType.RPCGuaranteedOrdered);
                return;
            }

            resp.WriteBoolean(true);

            SendPacket(resp, RPCGuaranteeType.RPCGuaranteedOrdered);

            character.EnterMap(false);

            CharacterManager.LoginCharacter(this, character);

            CurrentCharacter = character;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);

                var pack = new Packet(Opcode.TransferToSector);
                pack.WriteBytes(data.Address.GetAddressBytes().Reverse().ToArray());
                pack.WriteInteger(data.Port);
                pack.WriteInteger(0); // Flags, unused in the client

                SendPacket(pack, RPCGuaranteeType.RPCGuaranteedOrdered);
            });
        }

        private void HandleDisconnect(Packet packet)
        {
            CharacterManager.LogoutCharacter(this);

            SendPacket(new Packet(Opcode.DisconnectAck), RPCGuaranteeType.RPCGuaranteedOrdered);
        }
    }
}
