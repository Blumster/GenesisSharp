using System.Xml.Serialization;

namespace Genesis.Shared.Structures.XML
{
    using Manager;

    public struct ContinentObject
    {
        [XmlElement("intContestedMission")]
        public int ContestedMission;
        [XmlElement("IDContinentObject")]
        public uint ContinentObjectId;

        [XmlElement("intCoordinates")]
        public int Coordinates;
        [XmlElement("strDisplayName")]
        public string DisplayName;

        [XmlElement("CBIDImage")]
        public int Image;
        [XmlElement("strMapFilename")]
        public string MapFileName;
        [XmlElement("intMaxLevel")]
        public int MaxLevel;
        [XmlElement("intMaxPlayers")]
        public int MaxPlayers;
        [XmlElement("intMaxVersion")]
        public int MaxVersion;
        [XmlElement("intMinLevel")]
        public int MinLevel;
        [XmlElement("intMinVersion")]
        public int MinVersion;

        [XmlElement("IDObjective")]
        public int Objective;
        [XmlElement("IDOwningFaction")]
        public int OwningFaction;

        [XmlElement("rlPositionX")]
        public float PositionX;

        [XmlElement("rlPositionZ")]
        public float PositionZ;
        [XmlElement("rlRotation")]
        public float Rotation;
        [XmlElement("bitDropBrokenItems")]
        public string DropBrokenItems;
        [XmlElement("bitDropCommodities")]
        public string DropCommodities;

        [XmlElement("bitIsArena")]
        public string _isArena;
        [XmlElement("bitIsClientOnly")]
        public string _isClientOnly;
        [XmlElement("bitIsPersistent")]
        public string _isPersistent;
        [XmlElement("bitIsTown")]
        public string _isTown;

        [XmlElement("bitPlayCreateSounds")]
        public string _playCreateSounds;

        public bool IsPersistent => _isPersistent == "Tr";

        public bool IsTown => _isTown == "Tr";

        public bool IsClientOnly => _isClientOnly == "Tr";

        public bool IsArena => _isArena == "Tr";

        public bool IsPlayCreateSounds => _playCreateSounds == "Tr";

        public bool IsDropCommodities => DropCommodities == "Tr";

        public bool IsDropBrokenItems => DropBrokenItems == "Tr";

        public override string ToString()
        {
            return $"ID: {ContinentObjectId} | {DisplayName}";
        }

        public void OnDeserialization()
        {
            AssetManager.AssetContainer.AccessLock.EnterWriteLock();

            AssetManager.AssetContainer.ContinentObjects.Add(ContinentObjectId, this);

            AssetManager.AssetContainer.AccessLock.ExitWriteLock();
        }
    }
}
