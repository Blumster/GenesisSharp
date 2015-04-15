using System;
using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    public struct PowerPlantSpecific
    {
        public Int16 CoolRate;
        public Int32 HeatMaximum;
        public Int32 PowerMaximum;
        public Int16 PowerRegenRate;

        public static PowerPlantSpecific Read(BinaryReader br)
        {
            return new PowerPlantSpecific
            {
                HeatMaximum = br.ReadInt32(),
                PowerMaximum = br.ReadInt32(),
                PowerRegenRate = br.ReadInt16(),
                CoolRate = br.ReadInt16()
            };
        }

        public void WriteToPacket(Packet packet)
        {
            packet.WriteInteger(HeatMaximum);
            packet.WriteInteger(PowerMaximum);
            packet.WriteShort(PowerRegenRate);
            packet.WriteShort(CoolRate);
        }
    }
}
