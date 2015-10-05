using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    public struct ArmorSpecific
    {
        public short ArmorFactor;
        public short DefenseBonus;
        public float DeflectionModifier;
        public DamageArray Resistances;

        public static ArmorSpecific Read(BinaryReader br)
        {
            return new ArmorSpecific
            {
                DeflectionModifier = br.ReadSingle(),
                ArmorFactor = br.ReadInt16(),
                Resistances = DamageArray.Read(br),
                DefenseBonus = br.ReadInt16()
            };
        }

        public void WriteToPacket(Packet packet)
        {
            packet.WriteSingle(DeflectionModifier);
            packet.WriteShort(ArmorFactor);
            Resistances.WriteToPacket(packet);
            packet.WriteShort(DefenseBonus);
        }
    }
}
