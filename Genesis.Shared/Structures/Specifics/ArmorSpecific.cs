using System;
using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    public struct ArmorSpecific
    {
        public Int16 ArmorFactor;
        public Int16 DefenseBonus;
        public Single DeflectionModifier;
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
