using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    public struct PowerPlantSpecific
    {
        public short CoolRate;
        public int HeatMaximum;
        public int PowerMaximum;
        public short PowerRegenRate;

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
