using System;
using System.Collections.Generic;

namespace Genesis.Shared.Entities
{
    using Constant;
    using Database;
    using Database.DataStructs;
    using Manager;
    using Map;
    using Structures;
    using Structures.Model;
    using Structures.XML;
    using TNL;
    using TNL.Ghost;

    public class Character : Creature
    {
        public TNLConnection Connection { get; private set; }

        #region Declaration
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        // ReSharper disable NotAccessedField.Local
        private UInt64 _accountId;
        private Vector4 _lastHeading;
        public Byte GMLevel { get; private set; }
        private Byte _level;
        private Byte _bf297;
        private Byte _class;
        private Byte _race;
        public String Name { get; private set; }
        public String ClanName { get; private set; }
        public Int32 ClanId { get; private set; }
        public Int32 ClanRank { get; private set; }
        private Boolean _isInHazardMode;
        private Boolean _isGMFrozen;
        private Boolean _isGMChatBanned;
        private Boolean _isPossessingCreature;
        private Single _kmTravelled;
        private Single _kmTravelledMarker;
        public Int32 BodyId { get; private set; }
        public Int32 HeadId { get; private set; }
        public Int32 HairId { get; private set; }
        public Int32 HelmetId { get; private set; }
        public Int32 AccessoryId1 { get; private set; }
        public Int32 AccessoryId2 { get; private set; }
        public Int32 EyesId { get; private set; }
        public Int32 MouthId { get; private set; }
        private readonly UInt32[] _characterColors = new UInt32[6];
        public Single ScaleOffset { get; private set; }
        private UInt32 _xp;
        private Int32 _rapidKillTick;
        private Byte _rapidKillCount;
        private UInt64 _credits;
        private UInt64 _creditsDebt;
        private Int32 _lastTownId;
        private Int32 _lastStationMapId;
        private Int32 _lastStationId;
        private Int32 _worldLocationId;
        private UInt32 _mapId;
        private readonly Int32[] _quickBarSkills;
        private readonly Int64[] _quickBarItems;
        private Boolean _hasLocker;
        private Boolean _spectating;
        private Boolean _hasLevelled;
        private Boolean _isInFormation;
        private Boolean _transferPending;
        private Int32 _arenaId;
        private Boolean _waitingForArena;
        private Boolean _dontLeaveArena;
        private Boolean _foreignChar;
        private Boolean _preferPVP;
        private Boolean _gmInvisible;
        private Boolean _gmUntargetable;
        private UInt32 _timeLastKilled;
        private List<Object> _pendingMissions;
        public Int32 PetCBID { get; private set; }
        private TFID _petTFID;
        private Boolean _isAllUnlocked;
        public Boolean GivesToken { get; private set; }
        private Boolean _hasNewMail;
        private Int32 _playerChatId;
        private List<Object> _temporarySkills;
        private List<Int32> _failedMissions;
        private List<Int32> _achievements;
        private Int32 DisciplinePoints;
        private Int32 _currentTrainerMax;
        private Int32 _currentTrainerDisciplineId;
        private Byte _ranksReverseEngineering;
        private Byte _ranksExperimenting;
        private Byte _ranksMemorization;
        private Byte _ranksGadgeting;
        private Int64[] _memorizedList;
        private Single _hazardModeCount;
        private Single _hazardCountBonusPercent;
        private Single _hazardCountBonusPoints;
        private UInt16 _mapPoints;
        private Int32 _arenaBattleKills;
        private Int32 _arenaBattleObjKills;
        private Int32 _arenaBattleDeaths;
        private UInt16 _arenaBattleRank;
        private UInt32 _timeOfInstantRepair;
        private Single _mapPositionX;
        private Single _mapPositionY;
        private Single _mapPositionZ;
        private Int32 _lastMapSaved;
        private Int32 _dirtyBits;
        private Byte[] _userData;
        private Int32 _respecsBought;
        private Int64 _lastRespecTime;
        private Int32 _freeRespecs;
        private Int32 _specialMode;
        private CombatMode _combatMode;
        private Boolean _dieIfHazardDies;
        private Boolean _strafing;
        private Int16 SkillPoints;
        private Int16 AttributePoints;
        private Int32 _pendingTorunamentId;
        private Int16 _identify;
        private Int32 _shardId;
        private Int64 _lifeTimeAtLogin;
        private Int64 _lifeTimeMS;
        private Int32 _maxLevel;
        private Single _xpModifier;
        private Single _lootBonusModifier;
        private Single _lootEnhanceBonusModifier;
        private Single _clinkBonusModifier;
        private Single _clinkAmountBonusModifier;
        private Single _powerToHPModifier;
        private Single _powerToHeatModifier;
        private Boolean _activeProgressBar;
        private Boolean _progressBarInterruptable;
        private Single _missionUseTimer;
        private TFID _missionUseObject;
        private Single _kmTravelledAtProgressStart;
        private UInt64 _convoyCOID;
        private Boolean _tradeRequestPending;
        private Boolean _trading;
        private Int64 _tradingWithCOID;
        private Boolean _tradeApprove;
        private Int64 _tradeCredits;
        private Boolean _toHitStats;
        private Boolean _damageStats;
        private Boolean _lootRollStats;
        private Single _combatModeBoost;
        private Single _combatModeOffense;
        private Single _combatModeDefense;
        private DateTime _combatModeLastChanged;
        private Int16 _currentBattleMode;
        private Boolean _toggleDebugTokens;
        private Int32[] _arenaRank;
        private UInt32 _lastLifeTimeTick;
        private Int32[] _battleModes;
        private Boolean _postTransform;
        private Boolean _viewingTarget;
        private Inventory _inventory;
        private Inventory _tradeInventory;
        private Vehicle _vehicle;
        // ReSharper restore NotAccessedField.Local
        // ReSharper restore FieldCanBeMadeReadOnly.Local
        #endregion Declaration

        public Character()
        {
            _hasLocker = true;
            _spectating = false;
            _hasLevelled = false;
            _isInFormation = false;
            _transferPending = false;
            _arenaId = 0;
            _waitingForArena = false;
            _dontLeaveArena = false;
            _foreignChar = false;
            _gmInvisible = false;
            _gmUntargetable = false;
            _timeLastKilled = 0;
            PetCBID = -1;
            _petTFID = new TFID
            {
                Coid = -1L,
                Global = false
            }; ;
            _isAllUnlocked = false;
            GivesToken = true;
            _hasNewMail = false;
            _playerChatId = -1;
            DisciplinePoints = 0;
            _currentTrainerMax = -1;
            _currentTrainerDisciplineId = -1;
            _ranksReverseEngineering = 1;
            _ranksExperimenting = 1;
            _ranksMemorization = 1;
            _ranksGadgeting = 1;
            _hazardModeCount = 0.0f;
            _hazardCountBonusPercent = 0.0f;
            _hazardCountBonusPoints = 0;
            _mapPoints = 0;
            _arenaBattleKills = 0;
            _arenaBattleObjKills = 0;
            _arenaBattleDeaths = 0;
            _arenaBattleRank = 0;
            _timeOfInstantRepair = 0;
            _mapPositionX = 0.0f;
            _mapPositionY = 0.0f;
            _mapPositionZ = 0.0f;
            _lastMapSaved = -1;
            _dirtyBits = 0;
            ScaleOffset = 0.0f;
            Name = "";
            ClanName = "";
            _respecsBought = 0;
            _lastRespecTime = 0;
            _freeRespecs = 0;
            GMLevel = 0;
            _isInHazardMode = false;
            _isPossessingCreature = false;
            _dieIfHazardDies = false;
            _strafing = false;
            _isGMFrozen = false;
            _isGMChatBanned = false;
            _kmTravelled = 0.0f;
            _kmTravelledMarker = 0.0f;
            _level = 1;
            SkillPoints = 0;
            AttributePoints = 0;
            _pendingTorunamentId = -1;
            _identify = 0;
            BodyId = -1;
            HeadId = -1;
            HairId = -1;
            HelmetId = -1;
            AccessoryId1 = -1;
            AccessoryId2 = -1;
            EyesId = -1;
            MouthId = -1;
            _shardId = 0;
            _credits = 0UL;
            _creditsDebt = 0UL;
            _xp = 0;
            _rapidKillTick = 0;
            _rapidKillCount = 0;
            _lastTownId = -1;
            _lastStationMapId = -1;
            _lastStationId = -1;
            _worldLocationId = -1;
            _lifeTimeAtLogin = 0;
            _lifeTimeMS = 0;
            _maxLevel = 80;
            _xpModifier = 1.0f;
            _lootBonusModifier = 0.0f;
            _lootEnhanceBonusModifier = 0.0f;
            _clinkBonusModifier = 0.0f;
            _clinkAmountBonusModifier = 0.0f;
            _powerToHPModifier = 0.0f;
            _powerToHeatModifier = 0.0f;
            _activeProgressBar = false;
            _progressBarInterruptable = false;
            _missionUseTimer = 0.0f;
            _missionUseObject = new TFID();
            _kmTravelledAtProgressStart = 0.0f;
            _convoyCOID = 0;
            _tradeRequestPending = false;
            _trading = false;
            _tradingWithCOID = -1;
            _tradeApprove = false;
            _tradeCredits = 0;
            _toHitStats = false;
            _damageStats = false;
            _lootRollStats = false;
            ClanId = -1;
            ClanRank = -1;
            _specialMode = -1;
            _combatMode = 0;
            _combatModeBoost = 0.0f;
            _combatModeOffense = 0.0f;
            _combatModeDefense = 0.0f;
            _combatModeLastChanged = DateTime.Now - TimeSpan.FromMinutes(1);
            _currentBattleMode = -1;
            _toggleDebugTokens = false;

            SetCombatMode(_combatMode);

            _arenaRank = new Int32[7];
            _lastLifeTimeTick = (UInt32) DateTime.Now.Ticks;
            _battleModes = new[] {-1, -1, -1};

            ResetPossibleRewards(false, -1);

            _quickBarItems = new Int64[100];
            _quickBarSkills = new Int32[100];

            for (var i = 0; i < 100; ++i)
            {
                _quickBarItems[i] = -1L;
                _quickBarSkills[i] = -1;
            }

            _memorizedList = new Int64[8];
            for (var i = 0; i < 8; ++i)
                _memorizedList[i] = -1L;

            _lastHeading.X = 0.0f;
            _lastHeading.Y = 0.0f;
            _lastHeading.Z = 0.0f;
            _lastHeading.W = 0.0f;
            _postTransform = false;
            _viewingTarget = false;

            SetCustomColor(CustomCharacterColor.Primary, 0);
            SetCustomColor(CustomCharacterColor.Secondary, 0);
            SetCustomColor(CustomCharacterColor.Eye, 0);
            SetCustomColor(CustomCharacterColor.Hair, 0);
            SetCustomColor(CustomCharacterColor.Skin, 0);
            SetCustomColor(CustomCharacterColor.Speciality, 0);

            CreateInventory();
            CreateTradeInventory();
        }

        private void CreateInventory()
        {
            _inventory = new Inventory(34, 6, 4);
            _inventory.SetInventoryType(InventoryType.Locker);
            _inventory.SetMap(GetMap());
            _inventory.SetOwner(this);
        }

        private void CreateTradeInventory()
        {
            _tradeInventory = new Inventory(4, 6, 1);
            _tradeInventory.SetInventoryType(InventoryType.TradeYou);
            _tradeInventory.SetMap(GetMap());
        }

        private void ResetPossibleRewards(Boolean cleanupItems, Int32 missionId)
        {
            if (missionId == -1)
            {
                
            }

        }

        private void SetCombatMode(CombatMode combatMode)
        {
            if (GetVehicle() == null || (DateTime.Now - _combatModeLastChanged).Milliseconds < 10000)
                return;

            _combatModeLastChanged = DateTime.Now;

            Single boost, offense, defense;
            GetCombatModeValues(combatMode, out boost, out offense, out defense);

            UpdateVehicleStatus();

            _combatModeBoost = boost;
            _combatModeOffense = offense;
            _combatModeDefense = defense;

            _combatMode = combatMode;
        }

        public void SetOwner(TNLConnection connection)
        {
            Connection = connection;
        }

        public override void SaveToDB()
        {
            var cdata = new CharacterData
            {
                AccountId = _accountId,
                Coid = COID.Coid,
                Cbid = CBID,
                TeamFaction = TeamFaction,
                Race = _race,
                Class = _class,
                LastMapId = _mapId,
                LastStationMapId = _lastStationMapId,
                LastStationId = _lastStationId,
                X = Position.X,
                Y = Position.Y,
                Z = Position.Z,
                Q1 = Rotation.X,
                Q2 = Rotation.Y,
                Q3 = Rotation.Z,
                Q4 = Rotation.W,
                Head = HeadId,
                Body = BodyId,
                HeadDetail = AccessoryId1,
                HeadDetail2 = AccessoryId2,
                Hair = HairId,
                Mouth = MouthId,
                Eyes = EyesId,
                Helmet = HelmetId,
                PrimaryColor = GetCustomColor(CustomCharacterColor.Primary),
                SecondaryColor = GetCustomColor(CustomCharacterColor.Secondary),
                EyeColor = GetCustomColor(CustomCharacterColor.Eye),
                HairColor = GetCustomColor(CustomCharacterColor.Hair),
                SkinColor = GetCustomColor(CustomCharacterColor.Skin),
                SpecialColor = GetCustomColor(CustomCharacterColor.Speciality),
                Level = _level,
                Name = Name,
                ScaleOffset = ScaleOffset,
                ActiveVehicleCOID = CurrentVehicleId,
                Scale = Scale,
                CombatMode = (UInt32) _combatMode,
                BattleMode = _currentBattleMode,
                Credits = _credits,
                CreditsDebt = _creditsDebt,
                KmTravelled = _kmTravelled,
            };

            if (IsInDB)
                DataAccess.Character.UpdateCharacter(cdata);
            else
            {
                DataAccess.Character.InsertCharacter(cdata);
                IsInDB = true;
            }

            if (GetVehicle() != null)
                GetVehicle().SaveToDB();
        }

        public Vehicle GetVehicle()
        {
            return _vehicle;
        }

        public override Character GetSuperCharacter(Boolean inclSummons)
        {
            return this;
        }

        public void SetClanID(Int32 clanId)
        {
            // TODO: missing
            ClanId = clanId;
            GhostObject.SetMaskBits(0x20000000UL);
        }

        public void SetClanRank(Int32 rank)
        {
            // TODO: missing
            ClanRank = rank;
            GhostObject.SetMaskBits(0x20000000UL);
        }

        public void SetClanName(String clanName)
        {
            ClanName = clanName;
            GhostObject.SetMaskBits(0x20000000UL);
        }

        public override UInt32 GetMapId()
        {
            return _mapId;
        }

        public Byte GetRace()
        {
            return _race;
        }

        public Single GetPositionX()
        {
            return Position.X;
        }

        public Single GetPositionY()
        {
            return Position.Y;
        }

        public Single GetPositionZ()
        {
            return Position.Z;
        }

        public Boolean LoadFromDB(CharacterData data, Int64 charCoid = 0)
        {
            if (data == null)
                data = DataAccess.Character.GetCharacter(charCoid);

            if (data == null)
                return false;

            InitializeFromCBID(data.Cbid, null);

            _level = data.Level;
            _class = data.Class;
            _race = data.Race;
            TeamFaction = data.TeamFaction;

            SetCurrentHP(GetMaximumHP());
            Position = new Vector3(data.X, data.Y, data.Z);
            Rotation = new Vector4(data.Q1, data.Q2, data.Q3, data.Q4);
            Velocity = new Vector3();
            AngularVelocity = new Vector3();
            Name = data.Name;
            ScaleOffset = data.ScaleOffset;

            SetCOID(data.Coid);

            _accountId = data.AccountId;
            CurrentVehicleId = data.ActiveVehicleCOID;

            _lastStationId = data.LastStationId;
            _lastStationMapId = data.LastStationMapId;

            var map = MapManager.GetMap(data.LastMapId);
            SetMap(map);

            EyesId = data.Eyes;
            HairId = data.Hair;
            HeadId = data.Head;
            HelmetId = data.Helmet;
            BodyId = data.Body;
            AccessoryId1 = data.HeadDetail;
            AccessoryId2 = data.HeadDetail2;
            MouthId = data.Mouth;

            SetCustomColor(CustomCharacterColor.Primary, data.PrimaryColor);
            SetCustomColor(CustomCharacterColor.Secondary, data.SecondaryColor);
            SetCustomColor(CustomCharacterColor.Eye, data.EyeColor);
            SetCustomColor(CustomCharacterColor.Hair, data.HairColor);
            SetCustomColor(CustomCharacterColor.Skin, data.SkinColor);
            SetCustomColor(CustomCharacterColor.Speciality, data.SpecialColor);

            SetCombatMode((CombatMode) data.CombatMode);
            _currentBattleMode = data.BattleMode;
            _kmTravelled = data.KmTravelled;
            Scale = data.Scale;
            _credits = data.Credits;
            _creditsDebt = data.CreditsDebt;

            IsInDB = true;

            var vehicle = new Vehicle();
            vehicle.LoadFromDB(CurrentVehicleId);
            vehicle.SetOwner(this);

            SetVehicle(vehicle);

            return vehicle.GetAsVehicle() != null;
        }

        public void SetVehicle(Vehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public Boolean IsVehicleActive()
        {
            return GetVehicle() != null;
        }

        public override Boolean LoadFromDB(Int64 coid)
        {
            return LoadFromDB(null, coid);
        }

        public UInt32 GetCustomColor(CustomCharacterColor color)
        {
            return _characterColors[(Int32) color];
        }

        public void SetCustomColor(CustomCharacterColor color, UInt32 value)
        {
            _characterColors[(Int32) color] = value;
        }

        public override void InitializeFromCBID(Int32 cbid, SectorMap map)
        {
            base.InitializeFromCBID(cbid, map);
        }

        public override Character GetAsCharacter()
        {
            return this;
        }

        public override void WriteToCreatePacket(Packet packet, Boolean extended = false)
        {
            base.WriteToCreatePacket(packet, extended);

            #region Create Character
            packet.WriteLong(CurrentVehicleId); // current vehicle
            packet.WriteLong(CurrentTrailerCoid); // current trailer

            packet.WriteInteger(HeadId);
            packet.WriteInteger(BodyId);
            packet.WriteInteger(AccessoryId1);
            packet.WriteInteger(AccessoryId2);
            packet.WriteInteger(HairId);
            packet.WriteInteger(MouthId);
            packet.WriteInteger(EyesId);
            packet.WriteInteger(HelmetId);
            packet.WriteInteger(GetCustomColor(CustomCharacterColor.Primary));
            packet.WriteInteger(GetCustomColor(CustomCharacterColor.Secondary));
            packet.WriteInteger(GetCustomColor(CustomCharacterColor.Eye));
            packet.WriteInteger(GetCustomColor(CustomCharacterColor.Hair));
            packet.WriteInteger(GetCustomColor(CustomCharacterColor.Skin));
            packet.WriteInteger(GetCustomColor(CustomCharacterColor.Speciality));

            packet.WriteInteger(_lastTownId); // lasttownid
            packet.WriteInteger(_lastStationMapId); // laststationmapid

            packet.WriteByte(_level); // Level
            packet.WriteByte(_bf297); // bf297
            packet.WriteByte(GMLevel); // GMLevel

            packet.WritePadding(5);

            packet.WriteLong(DateTime.Now.Ticks); // Server Time

            packet.WriteUtf8StringOn(Name, 51);
            packet.WriteUtf8StringOn(ClanId != -1 ? ClanName : "", 51);

            packet.WritePadding(2);

            packet.WriteSingle(ScaleOffset);

            packet.WritePadding(4);
            #endregion Create Character

            if (!extended)
                return;

            #region Create Character Extended
            packet.WriteInteger(0); // num completed quests
            packet.WriteInteger(0); // num current quests
            packet.WriteShort(0); // num achievements
            packet.WriteShort(0); // num disciplines

            packet.WriteByte(0); // num skills

            packet.WritePadding(3);

            for (var i = 0; i < 50; ++i) // ContinentUnlocked
            {
                packet.WriteInteger(-1); // continent id

                packet.WriteByte(0); // State

                packet.WritePadding(3);

                packet.WriteInteger(0); // ExploredBits
            }

            for (var i = 0; i < 100; ++i) // coidQuickBarItems
                packet.WriteLong(_quickBarItems[i]);

            for (var i = 0; i < 100; ++i) // QuickBarSkills
                packet.WriteInteger(_quickBarSkills[i]);

            packet.WriteLong(_credits);
            packet.WriteLong(_creditsDebt);

            packet.WriteInteger(_xp);

            packet.WriteShort(Mana); // mana
            packet.WriteShort(MaxMana); // mana max
            packet.WriteShort(AttributePoints); // attrib points
            packet.WriteShort((Int16)AttribTech); // attrib tech
            packet.WriteShort((Int16)AttribCombat); // attrib combat
            packet.WriteShort((Int16)AttribTheory); // attrib theory
            packet.WriteShort((Int16)AttribPerception); // attrib perception
            packet.WriteShort((Int16)DisciplinePoints); // discipline points
            packet.WriteShort(SkillPoints); // skill points

            packet.WriteByte(_ranksReverseEngineering); // reverse engineering rank
            packet.WriteByte(_ranksExperimenting); // experimenting rank
            packet.WriteByte(_ranksMemorization); // memorization rank
            packet.WriteByte(_ranksGadgeting); // gadgeting rank

            packet.WritePadding(2);

            for (var i = 0; i < 4; ++i) // first time flags
                packet.WriteInteger(Connection.FirstTimeFlags[i]);

            packet.WritePadding(4);

            for (var i = 0; i < 8; ++i) // Memorized List
                packet.WriteLong(_memorizedList[i]);

            for (var i = 0; i < 7; ++i) // Arena Ranks
                packet.WriteInteger(0);

            packet.WritePadding(4);

            for (var i = 0; i < 312; ++i) // Inventory
                packet.WriteLong(-1L);

            packet.WriteSingle(_kmTravelled);
            packet.WriteSingle(_hazardModeCount); // hazard mode count

            packet.WriteInteger(_respecsBought); // respecs bought

            packet.WritePadding(4);

            packet.WriteLong(_lastRespecTime); // last respec time

            packet.WriteInteger(_freeRespecs); // free respecs

            packet.WritePadding(28);
            #endregion Create Character Extended
        }

        public void InitNewCharacter(CreateCharacterModel model, ConfigNewCharacter newCharEntry, SectorMap map, Int64 charCoid, Int64 vehicleCoid)
        {
            _accountId = Connection.AccountId;

            SetCOID(charCoid);
            SetCurrentHP(1);
            SetMaximumHP(100);
            Position = new Vector3(map.MapEntry.EntryX, map.MapEntry.EntryY, map.MapEntry.EntryZ);
            Rotation = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            Velocity = new Vector3();
            AngularVelocity = new Vector3();
            Scale = 1.0f;
            Name = model.CharacterName;
            HeadId = model.Head;
            HairId = model.Hair;
            BodyId = model.Body;
            MouthId = model.Mouth;
            EyesId = model.Eyes;
            AccessoryId1 = model.HeadDetail;
            AccessoryId2 = model.HeadDetail2;
            ScaleOffset = model.ScaleOffset;
            HelmetId = model.Helmet;
            _level = 1;
            _race = newCharEntry.Race;
            _class = newCharEntry.Class;
            _lastTownId = newCharEntry.StartTown;
            _lastStationMapId = (Int32) map.ContinentId;
            SetCustomColor(CustomCharacterColor.Primary, model.PrimaryColor);
            SetCustomColor(CustomCharacterColor.Secondary, model.SecondaryColor);
            SetCustomColor(CustomCharacterColor.Eye, model.EyeColor);
            SetCustomColor(CustomCharacterColor.Hair, model.HairColor);
            SetCustomColor(CustomCharacterColor.Skin, model.SkinColor);
            SetCustomColor(CustomCharacterColor.Speciality, model.SpecialColor);
            CurrentVehicleId = vehicleCoid;

            SetMap(map);
        }

        public override void SetMap(SectorMap map)
        {
            base.SetMap(map);
            
            if (map != null)
                _mapId = map.MapEntry.ContinentId;

            if (_vehicle != null)
                _vehicle.SetMap(map);
        }

        public void EnterMap(Boolean createGhost = true)
        {
            GetMap().AddObjectToMap(this);

            if (createGhost && false)
                CreateGhost();

            if (_vehicle != null)
            {
                GetMap().AddObjectToMap(_vehicle);

                if (createGhost)
                    _vehicle.CreateGhost();
            }
        }

        public void LeaveMap()
        {
            GetMap().RemoveFromMap(GetTFID());

            if (_vehicle != null)
                GetMap().RemoveFromMap(_vehicle.GetTFID());

            (Connection.GetInterface() as TNLInterface).RemoveGhost(Connection);
        }

        public override void CreateGhost()
        {
            SetGhosted(false);

            var g = GhostObject.CreateObject(GetTFID(), GhostType.Character);
            g.SetParent(this);
            SetGhost(g);

            // if logging in in the city
            //Connection.SetScopeObject(g);

            SetGhosted(true);

            // if vehicle is active
            GetVehicle().CreateGhost();
        }

        public static void GetCombatModeValues(CombatMode combatMode, out Single boost, out Single offense, out Single defense)
        {
            boost = 0.0f;
            offense = 0.0f;
            defense = 0.0f;

            switch (combatMode)
            {
                case CombatMode.Normal:
                    break;

                case CombatMode.Speed:
                    boost = MaxCombatModeBoost;
                    offense = -0.13333334f;
                    defense = -0.13333334f;
                    break;

                case CombatMode.Offense:
                    boost = -0.13333334f;
                    offense = MaxCombatModeOffense;
                    defense = -0.13333334f;
                    break;

                case CombatMode.Defense:
                    boost = -0.13333334f;
                    offense = -0.13333334f;
                    defense = MaxCombatModeDefense;
                    break;

                case CombatMode.SpeedSpeedDefense:
                    boost = 0.13333334f;
                    offense = -0.06666667f;
                    break;

                case CombatMode.SpeedDefenseDefense:
                    boost = 0.06666667f;
                    offense = -0.06666667f;
                    defense = 0.13333334f;
                    break;

                case CombatMode.SpeedSpeedOffense:
                    boost = 0.13333334f;
                    offense = 0.06666667f;
                    defense = -0.06666667f;
                    break;

                case CombatMode.SpeedOffenseOffense:
                    boost = 0.06666667f;
                    offense = 0.13333334f;
                    defense = -0.06666667f;
                    break;

                case CombatMode.DefenseDefenseOffense:
                    boost = -0.06666667f;
                    offense = 0.06666667f;
                    defense = 0.13333334f;
                    break;

                case CombatMode.DefenseOffenseOffense:
                    boost = -0.06666667f;
                    offense = 0.13333334f;
                    defense = 0.06666667f;
                    break;
            }
        }
    }
}