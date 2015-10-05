using System.Collections.Generic;
using System.Xml.Serialization;

namespace Genesis.Shared.Structures.XML
{
    using Manager;

    public class ConfigNewCharacter
    {
        [XmlElement("IDRace")]
        public byte Race;
        [XmlElement("IDClass")]
        public byte Class;
        [XmlElement("IDOptionCode")]
        public int OptionCode;
        [XmlElement("CBIDPowerPlant")]
        public int PowerPlant;
        [XmlElement("CBIDArmor")]
        public int Armor;
        [XmlElement("CBIDRaceItem")]
        public int RaceItem;
        [XmlElement("IDSkillBattleMode1")]
        public uint SkillBattleMode1;
        [XmlElement("IDSkillBattleMode2")]
        public uint SkillBattleMode2;
        [XmlElement("IDSkillBattleMode3")]
        public uint SkillBattleMode3;
        [XmlElement("IDStartingSkill1")]
        public uint StartSkill;
        [XmlElement("IDStartingTown")]
        public int StartTown;
        [XmlElement("CBIDTrailer")]
        public int Trailer;
        [XmlElement("CBIDVehicle")]
        public int Vehicle;
        [XmlElement("CBIDWeapon")]
        public int Weapon;

        public override string ToString()
        {
            return $"ID: {Race} | {Class}";
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
