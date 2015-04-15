using System;
using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public struct VehicleSpecific
    {
        public Single AVDCollisionSpinDamping;
        public Single AVDCollisionThreshold;
        public Single AVDNormalSpinDamping;
        public Single AbsoluteTopSpeed;
        public Single AerodynamicsAirDensity;
        public Single AerodynamicsDrag;
        public Vector3 AerodynamicsExtraGravity;
        public Single AerodynamicsFrontalArea;
        public Single AerodynamicsLift;
        public Int16 ArmorAdd;
        public Single[] AxleScale;
        public FrontRear BrakesMaxTorque;
        public FrontRear BrakesMinBlockTime;
        public FrontRear BrakesPedalInput;
        public Vector3 CenterOfMassModifier;
        public Byte ClassType;
        public Single ClutchDelayTime;
        public Int16 CooldownAdd;
        public RGB[] DefaultColors;
        public Int32 DefaultDriver;
        public Int32 DefaultWheelset;
        public Single DefensivePercent;
        public Int16 DownshiftRPM;
        public Byte[] DrawAxles;
        public Byte[] DrawShocks;
        public Byte EngineType;
        public Single[] GearRatios;
        public Int32 HardPointFacing;
        public Vector3[] HardPoints;
        public Int32 HeatMaxAdd;
        public Vector3 HitchPoint;
        public Int16 InventorySlots;
        public Single MaxTorqueFactor;
        public Single MaxWtArmor;
        public Single MaxWtEngine;
        public Single MaxWtWeaponDrop;
        public Single MaxWtWeaponFront;
        public Single MaxWtWeaponTurret;
        public Single MaximumRPMMax;
        public Single MaximumResistance;
        public Single MeleeScaler;
        public Single MinTorqueFactor;
        public Single MinimumRPM;
        public Single MinimumResistance;
        public Byte NumberOfGears;
        public Byte NumberOfTricks;
        public Byte NumberOfTrims;
        public Single OptimumRPMMax;
        public Single OptimumRPMMin;
        public Single OptimumResistance;
        public Int32 PowerMaxAdd;
        public Single PushBottomUp;
        public Single RVExtraAngularImpulse;
        public Single RVExtraTorqueFactor;
        public Single RVFrictionEqualizer;
        public Single RVInertiaPitch;
        public Single RVInertiaRoll;
        public Single RVInertiaYaw;
        public Single RVSpinTorquePitch;
        public Single RVSpinTorqueRoll;
        public Single RVSpinTorqueYaw;
        public Single RearWheelFrictionScalar;
        public Single ReverseGearRation;
        public Vector3[] ShockAttachPoints;
        public Single ShockEffectThreshold;
        public Single[] ShockScale;
        public Vector3 SkirtExtents;
        public Single SpeedLimiter;
        public Single SteeringFullSpeedLimit;
        public Single SteeringMaxAngle;
        public FrontRear SuspensionDampeningCoefficientCompression;
        public FrontRear SuspensionDampeningCoefficientExtension;
        public FrontRear SuspensionLength;
        public FrontRear SuspensionStrength;
        public Int16 TorqueMax;
        public Single TransmissionRatio;
        public VehicleTrick[] Tricks;
        public Byte TurretSize;
        public Int16 UpshiftRPM;
        public Int16 VehicleFlags;
        public Byte VehicleType;
        public Byte WheelAxle;
        public Byte WheelExistance;
        public Vector3[] WheelHardPoints;
        public Single[] WheelRadius;
        public FrontRear WheelTorqueRatios;
        public Single[] WheelWidth;

        public static VehicleSpecific Read(BinaryReader br)
        {
            var vs = new VehicleSpecific
            {
                VehicleType = br.ReadByte(),
                ClassType = br.ReadByte(),
            };

            br.ReadBytes(2);

            vs.DefaultColors = new RGB[3];
            for (var i = 0; i < 3; ++i)
                vs.DefaultColors[i] = RGB.Read(br);

            vs.HardPoints = new Vector3[3];
            for (var i = 0; i < 3; ++i)
                vs.HardPoints[i] = Vector3.Read(br);

            vs.HardPointFacing = br.ReadInt32();
            vs.WheelExistance = br.ReadByte();
            vs.WheelAxle = br.ReadByte();

            br.ReadBytes(2);

            vs.WheelHardPoints = new Vector3[6];
            for (var i = 0; i < 6; ++i)
                vs.WheelHardPoints[i] = Vector3.Read(br);

            vs.SuspensionLength = FrontRear.Read(br);
            vs.SuspensionStrength = FrontRear.Read(br);
            vs.SuspensionDampeningCoefficientCompression = FrontRear.Read(br);
            vs.SuspensionDampeningCoefficientExtension = FrontRear.Read(br);
            vs.BrakesMaxTorque = FrontRear.Read(br);
            vs.BrakesMinBlockTime = FrontRear.Read(br);
            vs.BrakesPedalInput = FrontRear.Read(br);
            vs.SteeringMaxAngle = br.ReadSingle();
            vs.SteeringFullSpeedLimit = br.ReadSingle();
            vs.AerodynamicsFrontalArea = br.ReadSingle();
            vs.AerodynamicsDrag = br.ReadSingle();
            vs.AerodynamicsLift = br.ReadSingle();
            vs.AerodynamicsAirDensity = br.ReadSingle();
            vs.AerodynamicsExtraGravity = Vector3.Read(br);
            vs.AVDNormalSpinDamping = br.ReadSingle();
            vs.AVDCollisionSpinDamping = br.ReadSingle();
            vs.AVDCollisionThreshold = br.ReadSingle();
            vs.RVFrictionEqualizer = br.ReadSingle();
            vs.RVSpinTorqueRoll = br.ReadSingle();
            vs.RVSpinTorquePitch = br.ReadSingle();
            vs.RVSpinTorqueYaw = br.ReadSingle();
            vs.RVExtraAngularImpulse = br.ReadSingle();
            vs.RVExtraTorqueFactor = br.ReadSingle();
            vs.RVInertiaRoll = br.ReadSingle();
            vs.RVInertiaPitch = br.ReadSingle();
            vs.RVInertiaYaw = br.ReadSingle();
            vs.WheelTorqueRatios = FrontRear.Read(br);
            vs.VehicleFlags = br.ReadInt16();

            br.ReadBytes(2);

            vs.HitchPoint = Vector3.Read(br);
            vs.WheelRadius = br.Read<Single>(6);
            vs.WheelWidth = br.Read<Single>(6);
            vs.SpeedLimiter = br.ReadSingle();
            vs.AbsoluteTopSpeed = br.ReadSingle();

            vs.ShockAttachPoints = new Vector3[6];
            for (var i = 0; i < 6; ++i)
                vs.ShockAttachPoints[i] = Vector3.Read(br);

            vs.DrawAxles = br.ReadBytes(2);
            vs.DrawShocks = br.ReadBytes(2);
            vs.AxleScale = br.Read<Single>(2);
            vs.ShockScale = br.Read<Single>(2);
            vs.ShockEffectThreshold = br.ReadSingle();
            vs.EngineType = br.ReadByte();
            vs.NumberOfGears = br.ReadByte();
            vs.TorqueMax = br.ReadInt16();
            vs.DownshiftRPM = br.ReadInt16();
            vs.UpshiftRPM = br.ReadInt16();
            vs.MinTorqueFactor = br.ReadSingle();
            vs.MaxTorqueFactor = br.ReadSingle();
            vs.MinimumRPM = br.ReadSingle();
            vs.OptimumRPMMin = br.ReadSingle();
            vs.OptimumRPMMax = br.ReadSingle();
            vs.MaximumRPMMax = br.ReadSingle();
            vs.MinimumResistance = br.ReadSingle();
            vs.OptimumResistance = br.ReadSingle();
            vs.MaximumResistance = br.ReadSingle();
            vs.TransmissionRatio = br.ReadSingle();
            vs.ClutchDelayTime = br.ReadSingle();
            vs.ReverseGearRation = br.ReadSingle();
            vs.GearRatios = br.Read<Single>(5);
            vs.ArmorAdd = br.ReadInt16();

            br.ReadBytes(2);

            vs.PowerMaxAdd = br.ReadInt32();
            vs.HeatMaxAdd = br.ReadInt32();
            vs.CooldownAdd = br.ReadInt16();

            br.ReadBytes(2);

            vs.DefaultWheelset = br.ReadInt32();
            vs.DefaultDriver = br.ReadInt32();
            vs.MaxWtWeaponFront = br.ReadSingle();
            vs.MaxWtWeaponTurret = br.ReadSingle();
            vs.MaxWtWeaponDrop = br.ReadSingle();
            vs.MaxWtArmor = br.ReadSingle();
            vs.MaxWtEngine = br.ReadSingle();
            vs.DefensivePercent = br.ReadSingle();
            vs.TurretSize = br.ReadByte();
            vs.NumberOfTrims = br.ReadByte();
            vs.NumberOfTricks = br.ReadByte();

            br.ReadByte();

            vs.MeleeScaler = br.ReadSingle();
            vs.InventorySlots = br.ReadInt16();

            br.ReadBytes(6);

            vs.SkirtExtents = Vector3.Read(br);
            vs.PushBottomUp = br.ReadSingle();
            vs.CenterOfMassModifier = Vector3.Read(br);
            vs.RearWheelFrictionScalar = br.ReadSingle();

            return vs;
        }
    }
}
