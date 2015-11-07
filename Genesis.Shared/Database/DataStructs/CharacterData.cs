using System.Data;

namespace Genesis.Shared.Database.DataStructs
{
    using System.Globalization;
    using Utils.Extensions;

    public class CharacterData
    {
        public ulong AccountId { get; set; }
        public long Coid { get; set; }
        public int Cbid { get; set; }
        public int TeamFaction { get; set; }
        public uint LastMapId { get; set; }
        public int LastStationId { get; set; }
        public int LastStationMapId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Q1 { get; set; }
        public float Q2 { get; set; }
        public float Q3 { get; set; }
        public float Q4 { get; set; }
        public int Head { get; set; }
        public int Body { get; set; }
        public int HeadDetail { get; set; }
        public int HeadDetail2 { get; set; }
        public int Hair { get; set; }
        public int Mouth { get; set; }
        public int Eyes { get; set; }
        public int Helmet { get; set; }
        public uint PrimaryColor { get; set; }
        public uint SecondaryColor { get; set; }
        public uint EyeColor { get; set; }
        public uint HairColor { get; set; }
        public uint SkinColor { get; set; }
        public uint SpecialColor { get; set; }
        public byte Level { get; set; }
        public string Name { get; set; }
        public float ScaleOffset { get; set; }
        public long ActiveVehicleCOID { get; set; }
        public byte Race { get; set; }
        public byte Class { get; set; }
        public uint CombatMode { get; set; }
        public short BattleMode { get; set; }
        public ulong Credits { get; set; }
        public ulong CreditsDebt { get; set; }
        public float Scale { get; set; }
        public float KmTravelled { get; set; }
        public bool Online { get; set; }

        public static CharacterData Read(IDataReader reader)
        {
            if (reader.Read())
            {
                return new CharacterData
                {
                    AccountId = reader.GetUInt64(0),
                    Coid = reader.GetInt64(1),
                    Cbid = reader.GetInt32(2),
                    TeamFaction = reader.GetInt32(3),
                    LastMapId = reader.GetUInt32(4),
                    LastStationMapId = reader.GetInt32(5),
                    X = reader.GetFloat(6),
                    Y = reader.GetFloat(7),
                    Z = reader.GetFloat(8),
                    Q1 = reader.GetFloat(9),
                    Q2 = reader.GetFloat(10),
                    Q3 = reader.GetFloat(11),
                    Q4 = reader.GetFloat(12),
                    Head = reader.GetInt32(13),
                    Body = reader.GetInt32(14),
                    HeadDetail = reader.GetInt32(15),
                    HeadDetail2 = reader.GetInt32(16),
                    Hair = reader.GetInt32(17),
                    Mouth = reader.GetInt32(18),
                    Eyes = reader.GetInt32(19),
                    Helmet = reader.GetInt32(20),
                    PrimaryColor = reader.GetUInt32(21),
                    SecondaryColor = reader.GetUInt32(22),
                    EyeColor = reader.GetUInt32(23),
                    HairColor = reader.GetUInt32(24),
                    SkinColor = reader.GetUInt32(25),
                    SpecialColor = reader.GetUInt32(26),
                    Level = reader.GetByte(27),
                    Name = reader.GetString(28),
                    ScaleOffset = reader.GetFloat(29),
                    ActiveVehicleCOID = reader.GetInt64(30),
                    LastStationId = reader.GetInt32(31),
                    Race = reader.GetByte(32),
                    Class = reader.GetByte(33),
                    CombatMode = reader.GetUInt32(34),
                    BattleMode = reader.GetInt16(35),
                    Credits = reader.GetUInt64(36),
                    CreditsDebt = reader.GetUInt64(37),
                    KmTravelled = reader.GetFloat(37),
                    Scale = reader.GetFloat(39),
                    Online = reader.GetBoolean(40)
                };
            }

            return null;
        }

        public static string GetQueryString()
        {
            //       0            1       2       3               4            5                   6    7    8    9     10    11    12    13      14      15            16             17      18        19     20        21              22                23          24           25           26              27       28      29             30                   31               32      33       34            35            36         37             38             39       40
            return "`AccountId`, `Coid`, `Cbid`, `TeamFaction`,  `LastMapId`, `LastStationMapId`, `X`, `Y`, `Z`, `Q1`, `Q2`, `Q3`, `Q4`, `Head`, `Body`, `HeadDetail`, `HeadDetail2`, `Hair`, `Mouth`, `Eyes`, `Helmet`, `PrimaryColor`, `SecondaryColor`, `EyeColor`, `HairColor`, `SkinColor`, `SpecialColor`, `Level`, `Name`, `ScaleOffset`, `ActiveVehicleCoid`, `LastStationId`, `Race`, `Class`, `CombatMode`, `BattleMode`, `Credits`, `CreditsDebt`, `KMTravelled`, `Scale`, `Online`";
        }

        public string GetInsertString()
        {
            return $"{AccountId}, {Coid}, {Cbid}, {TeamFaction}, {LastMapId}, {LastStationMapId}, {X.ToString(CultureInfo.InvariantCulture)}, {Y.ToString(CultureInfo.InvariantCulture)}, {Z.ToString(CultureInfo.InvariantCulture)}, {Q1.ToString(CultureInfo.InvariantCulture)}, {Q2.ToString(CultureInfo.InvariantCulture)}, {Q3.ToString(CultureInfo.InvariantCulture)}, {Q4.ToString(CultureInfo.InvariantCulture)}, {Head}, {Body}, {HeadDetail}, {HeadDetail2}, {Hair}, {Mouth}, {Eyes}, {Helmet}, {PrimaryColor}, {SecondaryColor}, {EyeColor}, {HairColor}, {SkinColor}, {SpecialColor}, {Level}, '{Name}', {ScaleOffset.ToString(CultureInfo.InvariantCulture)}, {ActiveVehicleCOID}, {LastStationId}, {Race}, {Class}, {CombatMode}, {BattleMode}, {Credits}, {CreditsDebt}, {KmTravelled.ToString(CultureInfo.InvariantCulture)}, {Scale.ToString(CultureInfo.InvariantCulture)}, {(Online ? 1 : 0)}";
        }

        public string GetUpdateString()
        {
            return $"`AccountId` = {AccountId}, `Coid` = {Coid}, `Cbid` = {Cbid}, `TeamFaction` = {TeamFaction}, `LastMapId` = {LastMapId}, `LastStationMapId` = {LastStationMapId}, `X` = {X.ToString(CultureInfo.InvariantCulture)}, `Y` = {Y.ToString(CultureInfo.InvariantCulture)}, `Z` = {Z.ToString(CultureInfo.InvariantCulture)}, `Q1` = {Q1.ToString(CultureInfo.InvariantCulture)}, `Q2` = {Q2.ToString(CultureInfo.InvariantCulture)}, `Q3` = {Q3.ToString(CultureInfo.InvariantCulture)}, `Q4` = {Q4.ToString(CultureInfo.InvariantCulture)}, `Head` = {Head}, `Body` = {Body}, `HeadDetail` = {HeadDetail}, `HeadDetail2` = {HeadDetail2}, `Hair` = {Hair}, `Mouth` = {Mouth},  `Eyes` = {Eyes}, `Helmet` = {Helmet}, `PrimaryColor` = {PrimaryColor}, `SecondaryColor` = {SecondaryColor}, `EyeColor` = {EyeColor}, `HairColor` = {HairColor}, `SkinColor` = {SkinColor}, `SpecialColor` = {SpecialColor}, `Level` = {Level}, `Name` = '{Name}', `ScaleOffset` = {ScaleOffset.ToString(CultureInfo.InvariantCulture)}, `ActiveVehicleCoid` = {ActiveVehicleCOID}, `LastStationId` = {LastStationId}, `Race` = {Race}, `Class` = {Class}, `CombatMode` = {CombatMode}, `BattleMode` = {BattleMode}, `Credits` = {Credits}, `CreditsDebt` = {CreditsDebt}, `KMTravelled` = {KmTravelled.ToString(CultureInfo.InvariantCulture)}, `Scale` = {Scale.ToString(CultureInfo.InvariantCulture)}, `Online` = {(Online ? 1 : 0)}";
        }
    }
}
