using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public struct WheelSetSpecific
    {
        public short[] Friction;
        public byte[] NumWheelsAxle;
        public string Wheel0Name;
        public string Wheel1Name;
        public byte WheelSetType;

        public static WheelSetSpecific Read(BinaryReader br)
        {
            return new WheelSetSpecific
            {
                Friction = br.Read<short>(6),
                NumWheelsAxle = br.ReadBytes(2),
                WheelSetType = br.ReadByte(),
                Wheel0Name = br.ReadPadding(1).ReadUnicodeString(65),
                Wheel1Name = br.ReadUnicodeString(65)
            };
        }
    }
}
