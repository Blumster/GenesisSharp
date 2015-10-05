using System.Data;

namespace Genesis.Shared.Database.DataStructs
{
    using System.Globalization;
    using Utils.Extensions;

    public class VehicleData
    {
        public long Coid { get; set; }
        public long OwnerCoid { get; set; }
        public int Cbid { get; set; }
        public int TeamFaction { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Q1 { get; set; }
        public float Q2 { get; set; }
        public float Q3 { get; set; }
        public float Q4 { get; set; }
        public long Ornament { get; set; }
        public long RaceItem { get; set; }
        public long PowerPlant { get; set; }
        public long Wheelset { get; set; }
        public long Armor { get; set; }
        public long MeleeWeapon { get; set; }
        public long Front { get; set; }
        public long Turret { get; set; }
        public long Rear { get; set; }
        public string Name { get; set; }
        public uint PrimaryColor { get; set; }
        public uint SecondaryColor { get; set; }
        public byte Trim { get; set; }

        public static VehicleData Read(IDataReader reader)
        {
            if (reader.Read())
            {
                return new VehicleData
                {
                    Coid = reader.GetInt64(0),
                    OwnerCoid = reader.GetInt64(1),
                    Cbid = reader.GetInt32(2),
                    TeamFaction = reader.GetInt32(3),
                    X = reader.GetFloat(4),
                    Y = reader.GetFloat(5),
                    Z = reader.GetFloat(6),
                    Q1 = reader.GetFloat(7),
                    Q2 = reader.GetFloat(8),
                    Q3 = reader.GetFloat(9),
                    Q4 = reader.GetFloat(10),
                    Ornament = reader.GetInt64(11),
                    RaceItem = reader.GetInt64(12),
                    PowerPlant = reader.GetInt64(13),
                    Wheelset = reader.GetInt64(14),
                    Armor = reader.GetInt64(15),
                    MeleeWeapon = reader.GetInt64(16),
                    Front = reader.GetInt64(17),
                    Turret = reader.GetInt64(18),
                    Rear = reader.GetInt64(19),
                    Name = reader.GetString(20),
                    PrimaryColor = reader.GetUInt32(21),
                    SecondaryColor = reader.GetUInt32(22),
                    Trim = reader.GetByte(23),
                };
            }

            return null;
        }

        public static string GetQueryString()
        {
            return "`Coid`, `OwnerCoid`, `Cbid`, `TeamFaction`, `X`, `Y`, `Z`, `Q1`, `Q2`, `Q3`, `Q4`, `Ornament`, `RaceItem`, `PowerPlant`, `Wheelset`, `Armor`, `MeleeWeapon`, `Front`, `Turret`, `Rear`, `Name`, `PrimaryColor`, `SecondaryColor`, `Trim`";
        }

        public string GetInsertString()
        {
            return $"{Coid}, {OwnerCoid}, {Cbid}, {TeamFaction}, {X.ToString(CultureInfo.InvariantCulture)}, {Y.ToString(CultureInfo.InvariantCulture)}, {Z.ToString(CultureInfo.InvariantCulture)}, {Q1.ToString(CultureInfo.InvariantCulture)}, {Q2.ToString(CultureInfo.InvariantCulture)}, {Q3.ToString(CultureInfo.InvariantCulture)}, {Q4.ToString(CultureInfo.InvariantCulture)}, {Ornament}, {RaceItem}, {PowerPlant}, {Wheelset}, {Armor}, {MeleeWeapon}, {Front}, {Turret}, {Rear}, '{Name}', {PrimaryColor}, {SecondaryColor}, {Trim} ";
        }

        public string GetUpdateString()
        {
            return $"`Coid` = {Coid}, `OwnerCoid` = {OwnerCoid}, `Cbid` = {Cbid}, `TeamFaction` = {TeamFaction}, `X` = {X.ToString(CultureInfo.InvariantCulture)}, `Y` = {Y.ToString(CultureInfo.InvariantCulture)}, `Z` = {Z.ToString(CultureInfo.InvariantCulture)}, `Q1` = {Q1.ToString(CultureInfo.InvariantCulture)}, `Q2` = {Q2.ToString(CultureInfo.InvariantCulture)}, `Q3` = {Q3.ToString(CultureInfo.InvariantCulture)}, `Q4` = {Q4.ToString(CultureInfo.InvariantCulture)}, `Ornament` = {Ornament}, `RaceItem` = {RaceItem}, `PowerPlant` = {PowerPlant}, `Wheelset` = {Wheelset}, `Armor` = {Armor}, `MeleeWeapon` = {MeleeWeapon}, `Front` = {Front}, `Turret` = {Turret}, `Rear` = {Rear}, `Name` = '{Name}', `PrimaryColor` = {PrimaryColor}, `SecondaryColor` = {SecondaryColor}, `Trim` = {Trim}";
        }
    }
}
