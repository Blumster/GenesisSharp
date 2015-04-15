using System;

namespace Genesis.Shared.Structures.Model
{
    public class CreateCharacterModel
    {
        public Int32 CBid { get; set; }
        public String CharacterName { get; set; }
        public String AccountName { get; set; }
        public String VehicleName { get; set; }
        public Int32 Head { get; set; }
        public Int32 Body { get; set; }
        public Int32 HeadDetail { get; set; }
        public Int32 HeadDetail2 { get; set; }
        public Int32 Helmet { get; set; }
        public Int32 Eyes { get; set; }
        public Int32 Mouth { get; set; }
        public Int32 Hair { get; set; }
        public UInt32 PrimaryColor { get; set; }
        public UInt32 SecondaryColor { get; set; }
        public UInt32 EyeColor { get; set; }
        public UInt32 HairColor { get; set; }
        public UInt32 SkinColor { get; set; }
        public UInt32 SpecialColor { get; set; }
        public UInt32 ShardId { get; set; }
        public UInt32 VehiclePrimaryColor { get; set; }
        public UInt32 VehicleSecondaryColor { get; set; }
        public Byte VehicleTrim { get; set; }
        public Single ScaleOffset { get; set; }
        public Int32 CBidWheelset { get; set; }

        public static CreateCharacterModel Read(Packet packet)
        {
            return new CreateCharacterModel
            {
                CBid = packet.ReadInteger(),
                AccountName = packet.ReadUtf8StringOn(33),
                CharacterName = packet.ReadUtf8StringOn(51),
                Head = packet.ReadInteger(),
                Body = packet.ReadInteger(),
                HeadDetail = packet.ReadInteger(),
                HeadDetail2 = packet.ReadInteger(),
                Helmet = packet.ReadInteger(),
                Eyes = packet.ReadInteger(),
                Mouth = packet.ReadInteger(),
                Hair = packet.ReadInteger(),
                PrimaryColor = packet.ReadUInteger(),
                SecondaryColor = packet.ReadUInteger(),
                EyeColor = packet.ReadUInteger(),
                HairColor = packet.ReadUInteger(),
                SkinColor = packet.ReadUInteger(),
                SpecialColor = packet.ReadUInteger(),
                ShardId = packet.ReadUInteger(),
                VehiclePrimaryColor = packet.ReadUInteger(),
                VehicleSecondaryColor = packet.ReadUInteger(),
                VehicleTrim = packet.ReadByte(),
                ScaleOffset = packet.ReadPadding(3).ReadSingle(),
                CBidWheelset = packet.ReadInteger(),
                VehicleName = packet.ReadUtf8StringOn(33),
            };
        }
    }
}
