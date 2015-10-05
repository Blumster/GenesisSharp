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

        private readonly IDictionary<int, CloneBaseObject> _cloneBaseObjects = new Dictionary<int, CloneBaseObject>();
        private readonly IDictionary<uint, string> _mapNameLookup = new Dictionary<uint, string>();

        public IDictionary<string, FileEntry> FileEntries { get; } = new Dictionary<string, FileEntry>();
        public IDictionary<string, FileEntry> DuplicatedFileEntries { get; } = new Dictionary<string, FileEntry>();
        public IDictionary<string, MapEntry> MapEntries { get; } = new Dictionary<string, MapEntry>();
        public IDictionary<int, CloneBaseObject> CloneBaseObjects => _cloneBaseObjects;
        public IDictionary<uint, Mission> Missions { get; } = new Dictionary<uint, Mission>();
        public IDictionary<uint, Skill> Skills { get; } = new Dictionary<uint, Skill>();
        public IDictionary<uint, PrefixArmor> ArmorPrefixes { get; } = new Dictionary<uint, PrefixArmor>();
        public IDictionary<uint, PrefixPowerPlant> PowerPlantPrefixes { get; } = new Dictionary<uint, PrefixPowerPlant>();
        public IDictionary<uint, PrefixWeapon> WeaponPrefixes { get; } = new Dictionary<uint, PrefixWeapon>();
        public IDictionary<uint, PrefixVehicle> VehiclePrefixes { get; } = new Dictionary<uint, PrefixVehicle>();
        public IDictionary<uint, PrefixOrnament> OrnamentPrefixes { get; } = new Dictionary<uint, PrefixOrnament>();
        public IDictionary<uint, PrefixRaceItem> RaceItemPrefixes { get; } = new Dictionary<uint, PrefixRaceItem>();
        public IDictionary<byte, IList<ConfigNewCharacter>> ConfigNewCharacters { get; } = new Dictionary<byte, IList<ConfigNewCharacter>>();
        public IDictionary<uint, ContinentObject> ContinentObjects { get; } = new Dictionary<uint, ContinentObject>();

        public ReaderWriterLockSlim AccessLock { get; } = new ReaderWriterLockSlim();
        #endregion

        public void AddMapEntry(string key, MapEntry entry)
        {
            AccessLock.EnterWriteLock();

            if (!MapEntries.ContainsKey(key))
                MapEntries.Add(key, entry);

            if (!_mapNameLookup.ContainsKey(entry.ContinentId))
                _mapNameLookup.Add(entry.ContinentId, key);

            AccessLock.ExitWriteLock();
        }

        public MapEntry GetMapEntryById(uint continentId)
        {
            AccessLock.EnterReadLock();

            string s;
            if (!_mapNameLookup.TryGetValue(continentId, out s))
                return null;

            AccessLock.ExitReadLock();

            return GetMapEntryByName(s);
        }

        public MapEntry GetMapEntryByName(string name)
        {
            AccessLock.EnterReadLock();

            MapEntry me;
            me = MapEntries.TryGetValue(name, out me) ? me : null;

            AccessLock.ExitReadLock();

            return me;
        }

        public CloneBaseObject GetCloneBaseObjectForCBID(int cbid)
        {
            AccessLock.EnterReadLock();

            CloneBaseObject cbo;
            cbo = _cloneBaseObjects.TryGetValue(cbid, out cbo) ? cbo : null;

            AccessLock.ExitReadLock();

            return cbo;
        }

        public ConfigNewCharacter GetNewCharacterDataByRaceClass(byte race, byte c)
        {
            AccessLock.EnterReadLock();

            IList<ConfigNewCharacter> l;
            var ret = ConfigNewCharacters.TryGetValue(race, out l) ? l.SingleOrDefault(e => e.Class == c) : null;

            AccessLock.ExitReadLock();

            return ret;
        }

        public ContinentObject GetContinentObjectById(uint id)
        {
            AccessLock.EnterReadLock();

            var co = ContinentObjects.SingleOrDefault(c => c.Value.ContinentObjectId == id);

            AccessLock.ExitReadLock();

            return co.Value;
        }
    }
}
