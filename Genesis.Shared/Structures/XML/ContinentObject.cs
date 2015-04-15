using System;
using System.Xml.Serialization;

namespace Genesis.Shared.Structures.XML
{
    using Shared.Manager;

    public struct ContinentObject
    {
        [XmlElement("intContestedMission")]
        public Int32 ContestedMission;
        [XmlElement("IDContinentObject")]
        public UInt32 ContinentObjectId;

        [XmlElement("intCoordinates")]
        public Int32 Coordinates;
        [XmlElement("strDisplayName")]
        public String DisplayName;

        [XmlElement("CBIDImage")]
        public Int32 Image;
        [XmlElement("strMapFilename")]
        public String MapFileName;
        [XmlElement("intMaxLevel")]
        public Int32 MaxLevel;
        [XmlElement("intMaxPlayers")]
        public Int32 MaxPlayers;
        [XmlElement("intMaxVersion")]
        public Int32 MaxVersion;
        [XmlElement("intMinLevel")]
        public Int32 MinLevel;
        [XmlElement("intMinVersion")]
        public Int32 MinVersion;

        [XmlElement("IDObjective")]
        public Int32 Objective;
        [XmlElement("IDOwningFaction")]
        public Int32 OwningFaction;

        [XmlElement("rlPositionX")]
        public Single PositionX;

        [XmlElement("rlPositionZ")]
        public Single PositionZ;
        [XmlElement("rlRotation")]
        public Single Rotation;
        [XmlElement("bitDropBrokenItems")]
        public String _dropBrokenItems;
        [XmlElement("bitDropCommodities")]
        public String _dropCommodities;

        [XmlElement("bitIsArena")]
        public String _isArena;
        [XmlElement("bitIsClientOnly")]
        public String _isClientOnly;
        [XmlElement("bitIsPersistent")]
        public String _isPersistent;
        [XmlElement("bitIsTown")]
        public String _isTown;

        [XmlElement("bitPlayCreateSounds")]
        public String _playCreateSounds;

        public Boolean IsPersistent
        {
            get { return _isPersistent == "Tr"; }
        }

        public Boolean IsTown
        {
            get { return _isTown == "Tr"; }
        }

        public Boolean IsClientOnly
        {
            get { return _isClientOnly == "Tr"; }
        }

        public Boolean IsArena
        {
            get { return _isArena == "Tr"; }
        }

        public Boolean IsPlayCreateSounds
        {
            get { return _playCreateSounds == "Tr"; }
        }

        public Boolean IsDropCommodities
        {
            get { return _dropCommodities == "Tr"; }
        }

        public Boolean IsDropBrokenItems
        {
            get { return _dropBrokenItems == "Tr"; }
        }

        public override String ToString()
        {
            return String.Format("ID: {0} | {1}", ContinentObjectId, DisplayName);
        }

        public void OnDeserialization()
        {
            AssetManager.AssetContainer.AccessLock.EnterWriteLock();

            AssetManager.AssetContainer.ContinentObjects.Add(ContinentObjectId, this);

            AssetManager.AssetContainer.AccessLock.ExitWriteLock();
        }
    }
}
