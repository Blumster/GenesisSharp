using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Genesis.Shared.Structures.Asset
{
    using Clonebase;
    using Map;
    using Mission;
    using Prefix;
    using Skill;
    using XML;

    public class AssetContainer
    {
        #region Declarations
        private readonly IDictionary<String, FileEntry> _fileEntries = new Dictionary<String, FileEntry>();
        private readonly IDictionary<String, FileEntry> _duplicatedFileEntries = new Dictionary<String, FileEntry>();
        private readonly IDictionary<String, MapEntry> _mapEntries = new Dictionary<String, MapEntry>();
        private readonly IDictionary<Int32, CloneBaseObject> _cloneBaseObjects = new Dictionary<Int32, CloneBaseObject>();
        private readonly IDictionary<UInt32, Mission> _missions = new Dictionary<UInt32, Mission>();
        private readonly IDictionary<UInt32, Skill> _skills = new Dictionary<UInt32, Skill>();
        private readonly IDictionary<UInt32, PrefixArmor> _armorPrefixes = new Dictionary<UInt32, PrefixArmor>();
        private readonly IDictionary<UInt32, PrefixPowerPlant> _powerPlantPrefixes = new Dictionary<UInt32, PrefixPowerPlant>();
        private readonly IDictionary<UInt32, PrefixWeapon> _weaponPrefixes = new Dictionary<UInt32, PrefixWeapon>();
        private readonly IDictionary<UInt32, PrefixVehicle> _vehiclePrefixes = new Dictionary<UInt32, PrefixVehicle>();
        private readonly IDictionary<UInt32, PrefixOrnament> _ornamentPrefixes = new Dictionary<UInt32, PrefixOrnament>();
        private readonly IDictionary<UInt32, PrefixRaceItem> _raceItemPrefixes = new Dictionary<UInt32, PrefixRaceItem>();
        private readonly IDictionary<Byte, IList<ConfigNewCharacter>> _configNewCharacters = new Dictionary<Byte, IList<ConfigNewCharacter>>();
        private readonly IDictionary<UInt32, ContinentObject> _continentObjects = new Dictionary<UInt32, ContinentObject>();
        private readonly IDictionary<UInt32, String> _mapNameLookup = new Dictionary<UInt32, String>();

        public IDictionary<String, FileEntry> FileEntries { get { return _fileEntries; } }
        public IDictionary<String, FileEntry> DuplicatedFileEntries { get { return _duplicatedFileEntries; } }
        public IDictionary<String, MapEntry> MapEntries { get { return _mapEntries; } }
        public IDictionary<Int32, CloneBaseObject> CloneBaseObjects { get { return _cloneBaseObjects; } }
        public IDictionary<UInt32, Mission> Missions { get { return _missions; } }
        public IDictionary<UInt32, Skill> Skills { get { return _skills; } }
        public IDictionary<UInt32, PrefixArmor> ArmorPrefixes { get { return _armorPrefixes; } }
        public IDictionary<UInt32, PrefixPowerPlant> PowerPlantPrefixes { get { return _powerPlantPrefixes; } }
        public IDictionary<UInt32, PrefixWeapon> WeaponPrefixes { get { return _weaponPrefixes; } }
        public IDictionary<UInt32, PrefixVehicle> VehiclePrefixes { get { return _vehiclePrefixes; } }
        public IDictionary<UInt32, PrefixOrnament> OrnamentPrefixes { get { return _ornamentPrefixes; } }
        public IDictionary<UInt32, PrefixRaceItem> RaceItemPrefixes { get { return _raceItemPrefixes; } }
        public IDictionary<Byte, IList<ConfigNewCharacter>> ConfigNewCharacters { get { return _configNewCharacters; } }
        public IDictionary<UInt32, ContinentObject> ContinentObjects { get { return _continentObjects; } }

        public ReaderWriterLockSlim AccessLock { get; private set; }
        #endregion

        public AssetContainer()
        {
            AccessLock = new ReaderWriterLockSlim();
        }

        public void AddMapEntry(String key, MapEntry entry)
        {
            AccessLock.EnterWriteLock();

            if (!_mapEntries.ContainsKey(key))
                _mapEntries.Add(key, entry);

            if (!_mapNameLookup.ContainsKey(entry.ContinentId))
                _mapNameLookup.Add(entry.ContinentId, key);

            AccessLock.ExitWriteLock();
        }

        public MapEntry GetMapEntryById(UInt32 continentId)
        {
            AccessLock.EnterReadLock();

            String s;
            if (_mapNameLookup.TryGetValue(continentId, out s))
            {
                AccessLock.ExitReadLock();

                return GetMapEntryByName(s);
            }

            return null;
        }

        public MapEntry GetMapEntryByName(String name)
        {
            AccessLock.EnterReadLock();

            MapEntry me;
            me = _mapEntries.TryGetValue(name, out me) ? me : null;

            AccessLock.ExitReadLock();

            return me;
        }

        public CloneBaseObject GetCloneBaseObjectForCBID(Int32 cbid)
        {
            AccessLock.EnterReadLock();

            CloneBaseObject cbo;
            cbo = _cloneBaseObjects.TryGetValue(cbid, out cbo) ? cbo : null;

            AccessLock.ExitReadLock();

            return cbo;
        }

        public ConfigNewCharacter GetNewCharacterDataByRaceClass(Byte race, Byte c)
        {
            AccessLock.EnterReadLock();

            IList<ConfigNewCharacter> l;
            var ret = _configNewCharacters.TryGetValue(race, out l) ? l.SingleOrDefault(e => e.Class == c) : null;

            AccessLock.ExitReadLock();

            return ret;
        }

        public ContinentObject GetContinentObjectById(UInt32 id)
        {
            AccessLock.EnterReadLock();

            var co = _continentObjects.SingleOrDefault(c => c.Value.ContinentObjectId == id);

            AccessLock.ExitReadLock();

            return co.Value;
        }
    }
}
