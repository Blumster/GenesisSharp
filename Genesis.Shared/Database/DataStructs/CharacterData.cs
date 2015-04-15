using System;
using System.Data;

namespace Genesis.Shared.Database.DataStructs
{
    using System.Globalization;
    using Utils.Extensions;

    public class CharacterData
    {
        public UInt64 AccountId { get; set; }
        public Int64 Coid { get; set; }
        public Int32 Cbid { get; set; }
        public Int32 TeamFaction { get; set; }
        public UInt32 LastMapId { get; set; }
        public Int32 LastStationId { get; set; }
        public Int32 LastStationMapId { get; set; }
        public Single X { get; set; }
        public Single Y { get; set; }
        public Single Z { get; set; }
        public Single Q1 { get; set; }
        public Single Q2 { get; set; }
        public Single Q3 { get; set; }
        public Single Q4 { get; set; }
        public Int32 Head { get; set; }
        public Int32 Body { get; set; }
        public Int32 HeadDetail { get; set; }
        public Int32 HeadDetail2 { get; set; }
        public Int32 Hair { get; set; }
        public Int32 Mouth { get; set; }
        public Int32 Eyes { get; set; }
        public Int32 Helmet { get; set; }
        public UInt32 PrimaryColor { get; set; }
        public UInt32 SecondaryColor { get; set; }
        public UInt32 EyeColor { get; set; }
        public UInt32 HairColor { get; set; }
        public UInt32 SkinColor { get; set; }
        public UInt32 SpecialColor { get; set; }
        public Byte Level { get; set; }
        public String Name { get; set; }
        public Single ScaleOffset { get; set; }
        public Int64 ActiveVehicleCOID { get; set; }
        public Byte Race { get; set; }
        public Byte Class { get; set; }
        public UInt32 CombatMode { get; set; }
        public Int16 BattleMode { get; set; }
        public UInt64 Credits { get; set; }
        public UInt64 CreditsDebt { get; set; }
        public Single Scale { get; set; }
        public Single KmTravelled { get; set; }

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
                };
            }

            return null;
        }

        public static String GetQueryString()
        {
            //       0            1       2       3               4            5                   6    7    8    9     10    11    12    13      14      15            16             17      18        19     20        21              22                23          24           25           26              27       28      29             30                   31               32      33       34            35            36         37             38             39
            return "`AccountId`, `Coid`, `Cbid`, `TeamFaction`,  `LastMapId`, `LastStationMapId`, `X`, `Y`, `Z`, `Q1`, `Q2`, `Q3`, `Q4`, `Head`, `Body`, `HeadDetail`, `HeadDetail2`, `Hair`, `Mouth`, `Eyes`, `Helmet`, `PrimaryColor`, `SecondaryColor`, `EyeColor`, `HairColor`, `SkinColor`, `SpecialColor`, `Level`, `Name`, `ScaleOffset`, `ActiveVehicleCoid`, `LastStationId`, `Race`, `Class`, `CombatMode`, `BattleMode`, `Credits`, `CreditsDebt`, `KMTravelled`, `Scale`";
        }

        public String GetInsertString()
        {
            return String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, '{28}', {29}, {30}, {31}, {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}", AccountId, Coid, Cbid, TeamFaction, LastMapId, LastStationMapId, X.ToString(CultureInfo.InvariantCulture), Y.ToString(CultureInfo.InvariantCulture), Z.ToString(CultureInfo.InvariantCulture), Q1.ToString(CultureInfo.InvariantCulture), Q2.ToString(CultureInfo.InvariantCulture), Q3.ToString(CultureInfo.InvariantCulture), Q4.ToString(CultureInfo.InvariantCulture), Head, Body, HeadDetail, HeadDetail2, Hair, Mouth, Eyes, Helmet, PrimaryColor, SecondaryColor, EyeColor, HairColor, SkinColor, SpecialColor, Level, Name, ScaleOffset.ToString(CultureInfo.InvariantCulture), ActiveVehicleCOID, LastStationId, Race, Class, CombatMode, BattleMode, Credits, CreditsDebt, KmTravelled.ToString(CultureInfo.InvariantCulture), Scale.ToString(CultureInfo.InvariantCulture));
        }

        public String GetUpdateString()
        {
            return String.Format("`AccountId` = {0}, `Coid` = {1}, `Cbid` = {2}, `TeamFaction` = {3}, `LastMapId` = {4}, `LastStationMapId` = {5}, `X` = {6}, `Y` = {7}, `Z` = {8}, `Q1` = {9}, `Q2` = {10}, `Q3` = {11}, `Q4` = {12}, `Head` = {13}, `Body` = {14}, `HeadDetail` = {15}, `HeadDetail2` = {16}, `Hair` = {17}, `Mouth` = {18},  `Eyes` = {19}, `Helmet` = {20}, `PrimaryColor` = {21}, `SecondaryColor` = {22}, `EyeColor` = {23}, `HairColor` = {24}, `SkinColor` = {25}, `SpecialColor` = {26}, `Level` = {27}, `Name` = '{28}', `ScaleOffset` = {29}, `ActiveVehicleCoid` = {30}, `LastStationId` = {31}, `Race` = {32}, `Class` = {33}, `CombatMode` = {34}, `BattleMode` = {35}, `Credits` = {36}, `CreditsDebt` = {37}, `KMTravelled` = {38}, `Scale` = {39}", AccountId, Coid, Cbid, TeamFaction, LastMapId, LastStationMapId, X.ToString(CultureInfo.InvariantCulture), Y.ToString(CultureInfo.InvariantCulture), Z.ToString(CultureInfo.InvariantCulture), Q1.ToString(CultureInfo.InvariantCulture), Q2.ToString(CultureInfo.InvariantCulture), Q3.ToString(CultureInfo.InvariantCulture), Q4.ToString(CultureInfo.InvariantCulture), Head, Body, HeadDetail, HeadDetail2, Hair, Mouth, Eyes, Helmet, PrimaryColor, SecondaryColor, EyeColor, HairColor, SkinColor, SpecialColor, Level, Name, ScaleOffset.ToString(CultureInfo.InvariantCulture), ActiveVehicleCOID, LastStationId, Race, Class, CombatMode, BattleMode, Credits, CreditsDebt, KmTravelled.ToString(CultureInfo.InvariantCulture), Scale.ToString(CultureInfo.InvariantCulture));
        }
    }
}
