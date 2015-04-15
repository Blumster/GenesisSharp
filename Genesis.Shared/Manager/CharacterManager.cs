using System;
using System.Collections.Generic;

using TNL.NET.Entities;

namespace Genesis.Shared.Manager
{
    using Constant;
    using Entities;
    using Entities.Base;
    using Structures.Model;
    using TNL;

    public static class CharacterManager
    {
        private static readonly Object Lock = new Object();
        private static readonly Dictionary<Int64, Character> ActiveCharacters = new Dictionary<Int64, Character>();

        public static Boolean CreateCharacterFromRequest(TNLConnection connection, CreateCharacterModel model, out Int64 charCoid)
        {
            var container = AssetManager.AssetContainer;

            charCoid = -1L;

            Byte r, c;
            if (!GetRaceClassByCBID(model.CBid, out r, out c))
                return false;

            var newCharEntry = container.GetNewCharacterDataByRaceClass(r, c);
            if (newCharEntry == null)
                return false;

            var map = MapManager.GetMap((UInt32)newCharEntry.StartTown);
            if (map == null)
                return false;

            charCoid = COIDManager.NextCOID;
            var vehCoid = COIDManager.NextCOID;

            var character = ClonedObjectBase.AllocateNewObjectFromCBID(model.CBid);

            character.InitializeFromCBID(model.CBid, null);

            var charObj = character.GetAsCharacter();
            if (charObj == null)
                return false;

            charObj.SetOwner(connection);
            charObj.InitNewCharacter(model, newCharEntry, map, charCoid, vehCoid);

            var cpacket = new Packet(Opcode.CreateCharacter);
            
            character.WriteToCreatePacket(cpacket);

            var vehicle = ClonedObjectBase.AllocateNewObjectFromCBID(newCharEntry.Vehicle);

            vehicle.InitializeFromCBID(newCharEntry.Vehicle, null);

            var vehObj = vehicle.GetAsVehicle();
            if (vehObj == null)
                return false;

            vehObj.InitNewVehicle(model, newCharEntry, map, charCoid, vehCoid);

            var vpacket = new Packet(Opcode.CreateVehicle);
            
            vehObj.WriteToCreatePacket(vpacket);

            connection.SendPacket(cpacket, RPCGuaranteeType.RPCGuaranteedOrdered);
            connection.SendPacket(vpacket, RPCGuaranteeType.RPCGuaranteedOrdered);

            charObj.SaveToDB();
            vehObj.SaveToDB();
            
            return true;
        }

        public static Boolean GetRaceClassByCBID(Int32 cbid, out Byte race, out Byte c)
        {
            switch (cbid)
            {
                case 32:
                case 3372:
                    race = 0;
                    c = 0;
                    return true;

                case 34:
                case 3366:
                    race = 0;
                    c = 1;
                    return true;

                case 97:
                case 3373:
                    race = 0;
                    c = 2;
                    return true;

                case 85:
                case 3374:
                    race = 0;
                    c = 3;
                    return true;

                case 100:
                case 8945:
                    race = 1;
                    c = 0;
                    return true;

                case 101:
                case 8171:
                    race = 1;
                    c = 1;
                    return true;

                case 102:
                case 9192:
                    race = 1;
                    c = 2;
                    return true;

                case 103:
                case 8200:
                    race = 1;
                    c = 3;
                    return true;

                case 51:
                case 3363:
                    race = 2;
                    c = 0;
                    return true;

                case 59:
                case 3370:
                    race = 2;
                    c = 1;
                    return true;

                case 98:
                case 3371:
                    race = 2;
                    c = 2;
                    return true;

                case 99:
                case 3368:
                    race = 2;
                    c = 3;
                    return true;

                default:
                    race = 0;
                    c = 0;
                    return false;
            }
        }

        public static void LoginCharacter(TNLConnection conn, Character character)
        {
            lock (Lock)
                ActiveCharacters.Add(conn.GetPlayerCOID(), character);
        }

        public static void LogoutCharacter(TNLConnection conn)
        {
            lock (Lock)
                ActiveCharacters.Remove(conn.GetPlayerCOID());
        }
    }
}
