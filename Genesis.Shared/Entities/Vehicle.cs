using System;
using System.Diagnostics;

namespace Genesis.Shared.Entities
{
    using Constant;
    using Database;
    using Database.DataStructs;
    using Map;
    using Misc;
    using Structures;
    using Structures.Model;
    using Structures.XML;
    using TNL;
    using TNL.Ghost;

    public class Vehicle : SimpleObject
    {
        #region Declarations
        // ReSharper disable NotAccessedField.Local
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        // ReSharper disable UnassignedField.Local
        private Single _speedLimit;
        private Single _possibleMaxSpeed;
        private Boolean _canMelee;
        private Boolean _isParked;
        private Boolean _flipperActivated;
        private Boolean _localCar;
        private Boolean _justActivated;
        private Boolean _drawTacArc;
        private Boolean _drawTargets;
        private Boolean _isManuallyAimingTurret;
        private Boolean _hasAnyTargets;
        public Boolean BrakeLock { get; private set; }
        private Boolean _lockdown;
        public Boolean IsTrailer { get; private set; }
        public Boolean VehicleIsActive { get; private set; }
        private StuntState _stuntState;
        private TFID _lastSentTarget;
        private Single _kmTravelled;
        private Single _speed;
        private Single _speedLastFrame;
        private Single _wantedTurretDirection;
        private Single _specialArmDirection;
        private Single _maxWtWeaponFront;
        private Single _maxWtWeaponTurret;
        private Single _maxWtWeaponRear;
        private Single _maxWtArmor;
        private Single _maxWtPowerPlant;
        private Boolean _lastSentBreaking;
        private Boolean _lastSendFiring;
        public Int32 Shield { get; private set; }
        public Int32 MaxShield { get; private set; }
        public Int32 VehicleTemplateId { get; private set; }
        public Int32 HeatCurrent { get; private set; }
        private Int32 _heatAccumulator;
        private Boolean _packetOverride;
        public UInt32 PrimaryColor { get; private set; }
        public UInt32 SecondaryColor { get; private set; }
        private UInt32 _dirtyBits;
        private Int16 _armorAdd;
        private Int32 _powerMaxAdd;
        private Int32 _heatMaxAdd;
        private Int16 _cooldownAdd;
        private Int16 _inventorySlots;
        public Single SpeedAdd { get; private set; }
        public Single BrakesMaxTorqueFrontMultiplier { get; private set; }
        public Single BrakesMaxTorqueRearAdjustMultiplier { get; private set; }
        public Single SteeringMaxAngleMultiplier { get; private set; }
        public Single SteeringFullSpeedLimitMultiplier { get; private set; }
        public Single AVDNormalSpinDampeningMultiplier { get; private set; }
        public Single AVDCollisionSpinDampeningAdjust { get; private set; }
        private Int32 _torqueMaxAdd;
        private Single _armorAdjustVariance;
        private Single _powerMaxAdjustVariance;
        private Single _cooldownRateVariance;
        private Single _hHeatMaxVariance;
        private Single _speedAdjustVariance;
        private Single _maxWtWeaponFrontVariance;
        private Single _maxWtWeaponTurretVariance;
        private Single _maxWtWeaponRearVariance;
        private Single _maxWtArmorVariance;
        private Single _maxWtPowerplantVariance;
        public Byte Trim { get; private set; }
        private Inventory _invetory;
        private Boolean _hasInventory;
        private UInt64 _extraCredits;
        public Single TurretDirection { get; private set; }
        public Single Acceleration { get; private set; }
        public Single Steering { get; private set; }
        public Boolean Braking { get; private set; }
        private Int64 _powerPlantCoid;
        private Int64 _wheelSetCoid;
        private Int64 _armorCoid;
        private Int64 _trailerCoid;
        private Int64[] _weaponsCoid;
        public String VehicleName { get; private set; }
        private Vector4 _target3DPoint;
        public Int32[] TrickIds { get; private set; }
        private Vector4 _turret;
        private Boolean[] _wheelWasOffGround;
        private Boolean _playSuspensionEffect;
        private Single _nextSkid;
        private Single _nextSuspension;
        private Boolean _loadedLight;
        private Int32 _wheelPhysicsBodyIndex;

        private PowerPlant _powerPlant;
        private Armor _armor;
        private WheelSet _wheelSet;
        private Weapon _meleeWeapon;
        private Weapon[] _weapons;
        private SimpleObject _ornament;
        private SimpleObject _raceItem;
        public Int64 CoidCurrentOwner { get; private set; }
        public Int32 CurrentPathId { get; private set; }
        public Int32 ExtraPathId { get; private set; }
        public Single PatrolDistance { get; private set; }
        public Boolean PathReversing { get; private set; }
        public Boolean PathIsRoad { get; private set; }
        public Int32 SpawnOwnerCoid { get; private set; }
        // ReSharper restore UnassignedField.Local
        // ReSharper restore FieldCanBeMadeReadOnly.Local
        // ReSharper restore NotAccessedField.Local
        // ReSharper restore UnusedAutoPropertyAccessor.Local
        #endregion Declarations

        public Vehicle()
        {
            _speedLimit = 76.0f;
            _possibleMaxSpeed = 0.0f;
            _canMelee = false;
            _isParked = false;
            _flipperActivated = false;
            _localCar = false;
            _justActivated = false;
            _drawTacArc = false;
            _drawTargets = false;
            _isManuallyAimingTurret = false;
            _hasAnyTargets = false;
            BrakeLock = false;
            _lockdown = false;
            VehicleIsActive = true;
            _stuntState = StuntState.None;
            _lastSentTarget = new TFID
            {
                Coid = -1L,
                Global = false
            }; ;
            _kmTravelled = 0.0f;
            _speed = 0.0f;
            _speedLastFrame = 0.0f;
            TurretDirection = 0.0f;
            _specialArmDirection = 0.0f;
            _maxWtWeaponFront = 0.0f;
            _maxWtWeaponTurret = 0.0f;
            _maxWtArmor = 0.0f;
            _maxWtPowerPlant = 0.0f;
            _lastSentBreaking = false;
            _lastSendFiring = false;
            Shield = 0x7FFFFFFF;
            MaxShield = 0;
            VehicleTemplateId = -1;
            HeatCurrent = 0;
            _heatAccumulator = 0;
            _packetOverride = false;
            PrimaryColor = 0;
            SecondaryColor = 0;
            _dirtyBits = 0U;
            _armorAdd = 0;
            _powerMaxAdd = 0;
            _heatMaxAdd = 0;
            _cooldownAdd = 0;
            _inventorySlots = 0;
            SpeedAdd = 1.0f;
            BrakesMaxTorqueFrontMultiplier = 1.0f;
            BrakesMaxTorqueRearAdjustMultiplier = 1.0f;
            SteeringMaxAngleMultiplier = 1.0f;
            SteeringFullSpeedLimitMultiplier = 1.0f;
            AVDNormalSpinDampeningMultiplier = 1.0f;
            AVDCollisionSpinDampeningAdjust = 1.0f;
            _torqueMaxAdd = 0;
            _armorAdjustVariance = 1.0f;
            _powerMaxAdjustVariance = 1.0f;
            _cooldownRateVariance = 1.0f;
            _hHeatMaxVariance = 1.0f;
            _speedAdjustVariance = 1.0f;
            _maxWtWeaponFrontVariance = 1.0f;
            _maxWtWeaponTurretVariance = 1.0f;
            _maxWtWeaponRearVariance = 1.0f;
            _maxWtArmorVariance = 1.0f;
            _maxWtPowerplantVariance = 1.0f;
            Trim = 0;
            _invetory = null;
            _hasInventory = true;
            _extraCredits = 0UL;
            Acceleration = 0.0f;
            Steering = 0.0f;
            Braking = true;
            _powerPlantCoid = -1L;
            _wheelSetCoid = -1L;
            _weaponsCoid = new[] { -1L, -1L, -1L };
            _armorCoid = -1L;
            _trailerCoid = -1L;
            VehicleName = "";
            _target3DPoint.X = 0.0f;
            _target3DPoint.Y = 0.0f;
            _target3DPoint.Z = 0.0f;
            _target3DPoint.W = 0.0f;
            TrickIds = new[] { -1, -1, -1, -1, -1, -1, -1, -1 };
            TurretDirection = 0.0f;
            _wantedTurretDirection = 0.0f;
            _turret.X = 0.0f;
            _turret.Y = 0.0f;
            _turret.Z = 0.0f;
            _turret.W = 0.0f;
            _wheelWasOffGround = new[] { false, false, false, false, false, false };
            _playSuspensionEffect = false;
            _nextSkid = 0.0f;
            _nextSuspension = 0.0f;
            _loadedLight = false;
            _wheelPhysicsBodyIndex = 0;
            _weapons = new Weapon[] { null, null, null };
            CoidCurrentOwner = -1L;
            CurrentPathId = -1;
            SpawnOwnerCoid = -1;
            IsTrailer = false;
        }

        public void SetPowerPlant(PowerPlant plant)
        {
            _powerPlant = plant;
        }

        public PowerPlant GetPowerPlant()
        {
            return _powerPlant;
        }

        public void SetWheelSet(WheelSet wheelset)
        {
            _wheelSet = wheelset;
        }

        public WheelSet GetWheelSet()
        {
            return _wheelSet;
        }

        public void SetOrnament(SimpleObject ornament)
        {
            _ornament = ornament;
        }

        public SimpleObject GetOrnament()
        {
            return _ornament;
        }

        public void SetRaceItem(SimpleObject raceItem)
        {
            _raceItem = raceItem;
        }

        public SimpleObject GetRaceItem()
        {
            return _raceItem;
        }

        public void SetArmorEquipped(Armor armor)
        {
            _armor = armor;
        }

        public Armor GetArmorEquipped()
        {
            return _armor;
        }

        public void AddWeapon(Int32 t, Weapon wep)
        {
            if (t >= 3)
                _meleeWeapon = wep;
            else
                _weapons[t] = wep;
        }

        public Weapon GetWeapon(Int32 t)
        {
            return _weapons[t];
        }

        public Int16 CalculateMaximumMana()
        {
            if (true)
            {
                if (true)
                {
                    return 0; // TODO
                }
                else
                    return 10;
            }
            else
                return 10;
        }

        public void SetMeleeWeapon(Weapon weapon)
        {
            _meleeWeapon = weapon;
        }

        public Weapon GetMeleeWeapon()
        {
            return _meleeWeapon;
        }

        public Boolean LoadFromDB(VehicleData data, Int64 vehCoid = 0)
        {
            if (data == null)
                data = DataAccess.Vehicle.GetVehicle(vehCoid);

            if (data == null)
                return false;

            if (data.Ornament != -1L)
            {
                _ornament = new SimpleObject();
                _ornament.LoadFromDB(data.Ornament);
            }

            if (data.RaceItem != -1L)
            {
                _raceItem = new SimpleObject();
                _raceItem.LoadFromDB(data.RaceItem);
            }

            if (data.PowerPlant != -1L)
            {
                _powerPlant = new PowerPlant();
                _powerPlant.LoadFromDB(data.PowerPlant);
            }

            if (data.Wheelset != -1L)
            {
                _wheelSet = new WheelSet();
                _wheelSet.LoadFromDB(data.Wheelset);
            }

            if (data.Armor != -1L)
            {
                _armor = new Armor();
                _armor.LoadFromDB(data.Armor);
            }

            if (data.MeleeWeapon != -1L)
            {
                _meleeWeapon = new Weapon();
                _meleeWeapon.LoadFromDB(data.MeleeWeapon);
            }

            if (data.Front != -1L)
            {
                _weapons[0] = new Weapon();
                _weapons[0].LoadFromDB(data.Front);
            }

            if (data.Turret != -1L)
            {
                _weapons[1] = new Weapon();
                _weapons[1].LoadFromDB(data.Turret);
            }

            if (data.Rear != -1L)
            {
                _weapons[2] = new Weapon();
                _weapons[2].LoadFromDB(data.Rear);
            }

            InitializeFromCBID(data.Cbid, null);
            SetCOID(data.Coid);
            CoidCurrentOwner = data.OwnerCoid;
            TeamFaction = data.TeamFaction;
            Position = new Vector3(data.X, data.Y, data.Z);
            Rotation = new Vector4(data.Q1, data.Q2, data.Q3, data.Q4);
            Velocity = new Vector3();
            AngularVelocity = new Vector3();
            Trim = data.Trim;
            PrimaryColor = data.PrimaryColor;
            SecondaryColor = data.SecondaryColor;
            VehicleName = data.Name;

            IsInDB = true;

            return true;
        }

        public override Boolean LoadFromDB(Int64 coid)
        {
            return LoadFromDB(null, coid);
        }

        public override void SaveToDB()
        {
            var vd = new VehicleData
            {
                Coid = COID.Coid,
                Cbid = CBID,
                OwnerCoid = CoidCurrentOwner,
                TeamFaction = TeamFaction,
                X = Position.X,
                Y = Position.Y,
                Z = Position.Z,
                Q1 = Rotation.X,
                Q2 = Rotation.Y,
                Q3 = Rotation.Z,
                Q4 = Rotation.W,
                Ornament = _ornament != null ? _ornament.GetCOID() : -1L,
                RaceItem = _raceItem != null ? _raceItem.GetCOID() : -1L,
                PowerPlant = _powerPlant != null ? _powerPlant.GetCOID() : -1L,
                Armor = _armor != null ? _armor.GetCOID() : -1L,
                Wheelset = _wheelSet != null ? _wheelSet.GetCOID() : -1L,
                MeleeWeapon = _meleeWeapon != null ? _meleeWeapon.GetCOID() : -1L,
                Front = _weapons[0] != null ? _weapons[0].GetCOID() : -1L,
                Turret = _weapons[1] != null ? _weapons[1].GetCOID() : -1L,
                Rear = _weapons[2] != null ? _weapons[2].GetCOID() : -1L,
                Name = VehicleName,
                Trim = Trim,
                PrimaryColor = PrimaryColor,
                SecondaryColor = SecondaryColor,
            };

            if (IsInDB)
                DataAccess.Vehicle.UpdateVehicle(vd);
            else
            {
                DataAccess.Vehicle.InsertVehicle(vd);
                IsInDB = true;
            }

            if (_ornament != null)
                _ornament.SaveToDB();

            if (_raceItem != null)
                _raceItem.SaveToDB();

            if (_powerPlant != null)
                _powerPlant.SaveToDB();

            if (_wheelSet != null)
                _wheelSet.SaveToDB();

            if (_armor != null)
                _armor.SaveToDB();

            if (_meleeWeapon != null)
                _meleeWeapon.SaveToDB();

            if (_weapons[0] != null)
                _weapons[0].SaveToDB();

            if (_weapons[1] != null)
                _weapons[1].SaveToDB();

            if (_weapons[2] != null)
                _weapons[2].SaveToDB();
        }

        public Int32 CalculateMaximumHeat()
        {
            return 0; // TODO
        }

        public override Vehicle GetAsVehicle()
        {
            return this;
        }

        public override void CreateGhost()
        {
            SetGhosted(false);

            var g = GhostObject.CreateObject(GetTFID(), GhostType.Vehicle);
            g.SetParent(this);
            SetGhost(g);

            // if logging in at a map where u sit in the vehicle
            GetSuperCharacter(false).Connection.SetScopeObject(g);
            (GetSuperCharacter(false).Connection.GetInterface() as TNLInterface).AddGhost(GetSuperCharacter(false).Connection, g);

            //GhostingManager.Instance.CreateGhost(this, g, EnumGhostType.GVehicle);

            g.SetMaskBits(0x0100000000UL); // WheelSet
            g.SetMaskBits(0x0000000008UL); // Health
            g.SetMaskBits(0x0000000040UL); // HealthMax
            g.SetMaskBits(0x0000000002UL); // Position
            g.SetMaskBits(0x0000000004UL); // Target
            g.SetMaskBits(0x0020000000UL); // HeatCurrent
            g.SetMaskBits(0x0002000000UL); // MaxShield
            g.SetMaskBits(0x0004000000UL); // Shield

            var arr = new[] { 0x0400000000UL, 0x0800000000UL, 0x1000000000UL }; // WeaponsMask

            for (var i = 0; i < 3; ++i)
            {
                var w = GetWeapon(i);
                if (w != null)
                    g.SetMaskBits(arr[i]);
            }

            if (_meleeWeapon != null)
                g.SetMaskBits(0x2000000000UL);

            if (_ornament != null)
                g.SetMaskBits(0x4000000000UL);

            if (_armor != null)
                g.SetMaskBits(0x0040000000UL);

            SetGhosted(true);
        }

        public override void WriteToCreatePacket(Packet packet, Boolean extended = false)
        {
            base.WriteToCreatePacket(packet, extended);

            #region Create Vehicle
            packet.WriteLong(CoidCurrentOwner); // current owner coid
            packet.WriteInteger(SpawnOwnerCoid); // spawn owner coid

            for (var i = 0; i < 8; ++i) // arr id tricks
                packet.WriteInteger(TrickIds[i]);

            packet.WriteInteger(PrimaryColor); // color primary
            packet.WriteInteger(SecondaryColor); // color secondary
            packet.WriteShort(_armorAdd); // armor add

            packet.WritePadding(2);

            packet.WriteInteger(_powerMaxAdd); // power max add
            packet.WriteInteger(_heatMaxAdd); // heat max add
            packet.WriteShort(_cooldownAdd); // cooldownadd
            packet.WriteShort(_inventorySlots); // inventoryslots
            packet.WriteSingle(_maxWtWeaponFront); // max wt weapon front
            packet.WriteSingle(_maxWtWeaponTurret); // max wt weapon turret
            packet.WriteSingle(_maxWtWeaponRear); // max wt weapon rear
            packet.WriteSingle(_maxWtArmor); // max wt armor
            packet.WriteSingle(_maxWtPowerPlant); // max wt power plant
            packet.WriteSingle(SpeedAdd); // speed add
            packet.WriteSingle(BrakesMaxTorqueFrontMultiplier); // brakes max torque front multiplier
            packet.WriteSingle(BrakesMaxTorqueRearAdjustMultiplier); // brakes max torque rear adjust multiplies
            packet.WriteSingle(SteeringMaxAngleMultiplier); // steering max angle multiplier
            packet.WriteSingle(SteeringFullSpeedLimitMultiplier); // steering full speed limit multiplier
            packet.WriteSingle(AVDNormalSpinDampeningMultiplier); // AVD normal spin dampening multiplier
            packet.WriteSingle(AVDCollisionSpinDampeningAdjust); // AVD collision spin dampening multiplier
            packet.WriteSingle(_kmTravelled); // km travelled
            packet.WriteBoolean(IsTrailer); // is trailer
            packet.WriteBoolean(false); // is in inventory
            packet.WriteBoolean(VehicleIsActive); // is active
            packet.WriteByte(Trim); // trim

            packet.WritePadding(4);

            #region Ornament
            packet.WriteOpcode(Opcode.CreateSimpleObject);

            if (_ornament != null) // Ornament
                _ornament.WriteToCreatePacket(packet, extended);
            else
                WriteEmptyObjectToPacket(packet);
            #endregion

            #region Race Item
            packet.WriteOpcode(Opcode.CreateSimpleObject);

            if (_raceItem != null && !TNLInterface.Instance.Adaptive) // Race Item
                _raceItem.WriteToCreatePacket(packet, extended);
            else
                WriteEmptyObjectToPacket(packet);
            #endregion

            #region Power Plant
            packet.WriteOpcode(Opcode.CreatePowerPlant);

            if (_powerPlant != null) // Power Plant
                _powerPlant.WriteToCreatePacket(packet);
            else
                PowerPlant.WriteEmptyObjectToPacket(packet);
            #endregion

            #region Wheel Set
            packet.WriteOpcode(Opcode.CreateWheelSet);

            if (_wheelSet != null) // Wheel set
                _wheelSet.WriteToCreatePacket(packet);
            else
                Debug.Assert(false, "WHEELSETNEK KELL LENNIE!");
            #endregion

            #region Armor
            packet.WriteOpcode(Opcode.CreateArmor);

            if (_armor != null) // Armor
                _armor.WriteToCreatePacket(packet);
            else
                Armor.WriteEmptyObjectToPacket(packet);
            #endregion

            #region Melee Weapon
            packet.WriteOpcode(Opcode.CreateWeapon);

            if (_meleeWeapon != null) // Weapon Melee
                _meleeWeapon.WriteToCreatePacket(packet);
            else
                Weapon.WriteEmptyObjectToPacket(packet);
            #endregion

            #region Front Weapon
            packet.WriteOpcode(Opcode.CreateWeapon);

            if (_weapons[0] != null) // Weapon Front
                _weapons[0].WriteToCreatePacket(packet);
            else
                Weapon.WriteEmptyObjectToPacket(packet);
            #endregion

            #region Turret Weapon
            packet.WriteOpcode(Opcode.CreateWeapon);

            if (_weapons[1] != null) // Weapon Turret
                _weapons[1].WriteToCreatePacket(packet);
            else
                Weapon.WriteEmptyObjectToPacket(packet);
            #endregion

            #region Rear Weapon
            packet.WriteOpcode(Opcode.CreateWeapon);

            if (_weapons[2] != null) // Weapon Rear
                _weapons[2].WriteToCreatePacket(packet);
            else
                Weapon.WriteEmptyObjectToPacket(packet);
            #endregion

            packet.WriteInteger(CurrentPathId); // current path id
            packet.WriteInteger(ExtraPathId); // extra path id
            packet.WriteSingle(PatrolDistance); // patrol distance
            packet.WriteBoolean(PathReversing); // path reversing
            packet.WriteBoolean(PathIsRoad); // path is road

            packet.WritePadding(2);

            packet.WriteInteger(VehicleTemplateId);

            packet.WritePadding(4);

            packet.WriteLong(GetTFIDMurderer().Coid);

            if (_weapons[0] != null)
                packet.WriteInteger(_weapons[0].GetCBID());
            else
                packet.WriteInteger(-1); // front weapon cbid

            if (_weapons[1] != null)
                packet.WriteInteger(_weapons[1].GetCBID());
            else
                packet.WriteInteger(-1); // turret weapon cbid

            if (_weapons[2] != null)
                packet.WriteInteger(_weapons[2].GetCBID());
            else
                packet.WriteInteger(-1); // rear weapon cbid

            packet.WriteUtf8StringOn(VehicleName, 33);

            packet.WritePadding(3);
            #endregion Create Vehicle

            if (!extended)
                return;

            #region Create Vehicle Extended
            packet.WriteShort(0); // num inventory slots
            packet.WriteShort(0); // inventory size

            packet.WritePadding(4);

            for (var i = 0; i < 512; ++i)
                packet.WriteLong(0);

            #endregion Create Vehicle Extended
        }

        public void InitNewVehicle(CreateCharacterModel model, ConfigNewCharacter newCharEntry, SectorMap map, Int64 charCoid, Int64 vehicleCoid)
        {
            SetCOID(vehicleCoid);

            CoidCurrentOwner = charCoid;
            Position = new Vector3(map.MapEntry.EntryX, map.MapEntry.EntryY, map.MapEntry.EntryZ);
            Rotation = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            Velocity = new Vector3();
            AngularVelocity = new Vector3();
            VehicleName = model.VehicleName;

            Trim = model.VehicleTrim;
            PrimaryColor = model.VehiclePrimaryColor;
            SecondaryColor = model.SecondaryColor;

            var armor = AllocateNewObjectFromCBID(newCharEntry.Armor);
            if (armor != null)
            {
                armor.SetCOID(CoidManager.NextCOID);
                armor.InitializeFromCBID(newCharEntry.Armor, map);
                armor.InitializeBaseData();

                SetArmorEquipped(armor.GetAsArmor());
            }

            var wheelSet = AllocateNewObjectFromCBID(model.CBidWheelset);
            if (wheelSet != null)
            {
                wheelSet.SetCOID(CoidManager.NextCOID);
                wheelSet.InitializeFromCBID(model.CBidWheelset, map);
                wheelSet.InitializeBaseData();

                SetWheelSet(wheelSet.GetAsWheelSet());
            }

            var powerPlant = AllocateNewObjectFromCBID(newCharEntry.PowerPlant);
            if (powerPlant != null)
            {
                powerPlant.SetCOID(CoidManager.NextCOID);
                powerPlant.InitializeFromCBID(newCharEntry.PowerPlant, map);
                powerPlant.InitializeBaseData();

                SetPowerPlant(powerPlant.GetAsPowerPlant());
            }

            var raceItem = AllocateNewObjectFromCBID(newCharEntry.RaceItem);
            if (raceItem != null)
            {
                raceItem.SetCOID(CoidManager.NextCOID);
                raceItem.InitializeFromCBID(newCharEntry.RaceItem, map);
                raceItem.InitializeBaseData();

                SetRaceItem(raceItem.GetAsSimpleObject());
            }

            var turret = AllocateNewObjectFromCBID(newCharEntry.Weapon);
            if (turret != null)
            {
                turret.SetCOID(CoidManager.NextCOID);
                turret.InitializeFromCBID(newCharEntry.Weapon, map);
                turret.InitializeBaseData();

                AddWeapon(1, turret.GetAsWeapon());
            }
        }

        public Boolean SkipCurrentOwner()
        {
            return true;
        }

        public override void HandleMove(Packet packet)
        {
            base.HandleMove(packet);

            Acceleration = packet.ReadSingle();
            Steering = packet.ReadSingle();
            TurretDirection = packet.ReadSingle();

            var vehFlags = packet.ReadUInteger();
            var firing = packet.ReadBoolean();

            var target = packet.ReadPadding(2).ReadTFID();

            var targetObj = GetMap().GetObject(target);

            if (targetObj != null)
                SetTargetObject(targetObj);
        }
    }
}
