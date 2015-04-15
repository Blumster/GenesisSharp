using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Genesis.Shared.Structures.XML
{
    using Shared.Manager;

    public class ConfigNewCharacter
    {
        [XmlElement("IDRace")]
        public Byte Race;
        [XmlElement("IDClass")]
        public Byte Class;
        [XmlElement("IDOptionCode")]
        public Int32 OptionCode;
        [XmlElement("CBIDPowerPlant")]
        public Int32 PowerPlant;
        [XmlElement("CBIDArmor")]
        public Int32 Armor;
        [XmlElement("CBIDRaceItem")]
        public Int32 RaceItem;
        [XmlElement("IDSkillBattleMode1")]
        public UInt32 SkillBattleMode1;
        [XmlElement("IDSkillBattleMode2")]
        public UInt32 SkillBattleMode2;
        [XmlElement("IDSkillBattleMode3")]
        public UInt32 SkillBattleMode3;
        [XmlElement("IDStartingSkill1")]
        public UInt32 StartSkill;
        [XmlElement("IDStartingTown")]
        public Int32 StartTown;
        [XmlElement("CBIDTrailer")]
        public Int32 Trailer;
        [XmlElement("CBIDVehicle")]
        public Int32 Vehicle;
        [XmlElement("CBIDWeapon")]
        public Int32 Weapon;

        public override String ToString()
        {
            return String.Format("ID: {0} | {1}", Race, Class);
        }

        public void OnDeserialization()
        {
            AssetManager.AssetContainer.AccessLock.EnterWriteLock();

            if (!AssetManager.AssetContainer.ConfigNewCharacters.ContainsKey(Race))
                AssetManager.AssetContainer.ConfigNewCharacters.Add(Race, new List<ConfigNewCharacter>());

            AssetManager.AssetContainer.ConfigNewCharacters[Race].Add(this);

            AssetManager.AssetContainer.AccessLock.ExitWriteLock();
        }
    }
}
