using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct DamageArray
    {
        public short[] Damage;

        public static DamageArray Read(BinaryReader br)
        {
            return new DamageArray { Damage = br.Read<short>(6) };
        }

        public void WriteToPacket(Packet packet)
        {
            for (var i = 0; i < 6; ++i)
                packet.WriteShort(Damage[i]);
        }
    }
}
