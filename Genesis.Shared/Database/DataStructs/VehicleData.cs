using System;
using System.Data;

namespace Genesis.Shared.Database.DataStructs
{
    using System.Globalization;
    using Utils.Extensions;

    public class VehicleData
    {
        public Int64 Coid { get; set; }
        public Int64 OwnerCoid { get; set; }
        public Int32 Cbid { get; set; }
        public Int32 TeamFaction { get; set; }
        public Single X { get; set; }
        public Single Y { get; set; }
        public Single Z { get; set; }
        public Single Q1 { get; set; }
        public Single Q2 { get; set; }
        public Single Q3 { get; set; }
        public Single Q4 { get; set; }
        public Int64 Ornament { get; set; }
        public Int64 RaceItem { get; set; }
        public Int64 PowerPlant { get; set; }
        public Int64 Wheelset { get; set; }
        public Int64 Armor { get; set; }
        public Int64 MeleeWeapon { get; set; }
        public Int64 Front { get; set; }
        public Int64 Turret { get; set; }
        public Int64 Rear { get; set; }
        public String Name { get; set; }
        public UInt32 PrimaryColor { get; set; }
        public UInt32 SecondaryColor { get; set; }
        public Byte Trim { get; set; }

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

        public static String GetQueryString()
        {
            return "`Coid`, `OwnerCoid`, `Cbid`, `TeamFaction`, `X`, `Y`, `Z`, `Q1`, `Q2`, `Q3`, `Q4`, `Ornament`, `RaceItem`, `PowerPlant`, `Wheelset`, `Armor`, `MeleeWeapon`, `Front`, `Turret`, `Rear`, `Name`, `PrimaryColor`, `SecondaryColor`, `Trim`";
        }

        public String GetInsertString()
        {
            return String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, '{20}', {21}, {22}, {23} ", Coid, OwnerCoid, Cbid, TeamFaction, X.ToString(CultureInfo.InvariantCulture), Y.ToString(CultureInfo.InvariantCulture), Z.ToString(CultureInfo.InvariantCulture), Q1.ToString(CultureInfo.InvariantCulture), Q2.ToString(CultureInfo.InvariantCulture), Q3.ToString(CultureInfo.InvariantCulture), Q4.ToString(CultureInfo.InvariantCulture), Ornament, RaceItem, PowerPlant, Wheelset, Armor, MeleeWeapon, Front, Turret, Rear, Name, PrimaryColor, SecondaryColor, Trim);
        }

        public String GetUpdateString()
        {
            return String.Format("`Coid` = {0}, `OwnerCoid` = {1}, `Cbid` = {2}, `TeamFaction` = {3}, `X` = {4}, `Y` = {5}, `Z` = {6}, `Q1` = {7}, `Q2` = {8}, `Q3` = {9}, `Q4` = {10}, `Ornament` = {11}, `RaceItem` = {12}, `PowerPlant` = {13}, `Wheelset` = {14}, `Armor` = {15}, `MeleeWeapon` = {16}, `Front` = {17}, `Turret` = {18}, `Rear` = {19}, `Name` = '{20}', `PrimaryColor` = {21}, `SecondaryColor` = {22}, `Trim` = {23}", Coid, OwnerCoid, Cbid, TeamFaction, X.ToString(CultureInfo.InvariantCulture), Y.ToString(CultureInfo.InvariantCulture), Z.ToString(CultureInfo.InvariantCulture), Q1.ToString(CultureInfo.InvariantCulture), Q2.ToString(CultureInfo.InvariantCulture), Q3.ToString(CultureInfo.InvariantCulture), Q4.ToString(CultureInfo.InvariantCulture), Ornament, RaceItem, PowerPlant, Wheelset, Armor, MeleeWeapon, Front, Turret, Rear, Name, PrimaryColor, SecondaryColor, Trim);
        }
    }
}
