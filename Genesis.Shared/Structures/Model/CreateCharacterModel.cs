namespace Genesis.Shared.Structures.Model
{
    public class CreateCharacterModel
    {
        public int CBid { get; set; }
        public string CharacterName { get; set; }
        public string AccountName { get; set; }
        public string VehicleName { get; set; }
        public int Head { get; set; }
        public int Body { get; set; }
        public int HeadDetail { get; set; }
        public int HeadDetail2 { get; set; }
        public int Helmet { get; set; }
        public int Eyes { get; set; }
        public int Mouth { get; set; }
        public int Hair { get; set; }
        public uint PrimaryColor { get; set; }
        public uint SecondaryColor { get; set; }
        public uint EyeColor { get; set; }
        public uint HairColor { get; set; }
        public uint SkinColor { get; set; }
        public uint SpecialColor { get; set; }
        public uint ShardId { get; set; }
        public uint VehiclePrimaryColor { get; set; }
        public uint VehicleSecondaryColor { get; set; }
        public byte VehicleTrim { get; set; }
        public float ScaleOffset { get; set; }
        public int CBidWheelset { get; set; }

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
