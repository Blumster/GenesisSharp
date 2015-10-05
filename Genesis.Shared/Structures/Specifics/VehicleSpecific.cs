using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public struct VehicleSpecific
    {
        public float AVDCollisionSpinDamping;
        public float AVDCollisionThreshold;
        public float AVDNormalSpinDamping;
        public float AbsoluteTopSpeed;
        public float AerodynamicsAirDensity;
        public float AerodynamicsDrag;
        public Vector3 AerodynamicsExtraGravity;
        public float AerodynamicsFrontalArea;
        public float AerodynamicsLift;
        public short ArmorAdd;
        public float[] AxleScale;
        public FrontRear BrakesMaxTorque;
        public FrontRear BrakesMinBlockTime;
        public FrontRear BrakesPedalInput;
        public Vector3 CenterOfMassModifier;
        public byte ClassType;
        public float ClutchDelayTime;
        public short CooldownAdd;
        public RGB[] DefaultColors;
        public int DefaultDriver;
        public int DefaultWheelset;
        public float DefensivePercent;
        public short DownshiftRPM;
        public byte[] DrawAxles;
        public byte[] DrawShocks;
        public byte EngineType;
        public float[] GearRatios;
        public int HardPointFacing;
        public Vector3[] HardPoints;
        public int HeatMaxAdd;
        public Vector3 HitchPoint;
        public short InventorySlots;
        public float MaxTorqueFactor;
        public float MaxWtArmor;
        public float MaxWtEngine;
        public float MaxWtWeaponDrop;
        public float MaxWtWeaponFront;
        public float MaxWtWeaponTurret;
        public float MaximumRPMMax;
        public float MaximumResistance;
        public float MeleeScaler;
        public float MinTorqueFactor;
        public float MinimumRPM;
        public float MinimumResistance;
        public byte NumberOfGears;
        public byte NumberOfTricks;
        public byte NumberOfTrims;
        public float OptimumRPMMax;
        public float OptimumRPMMin;
        public float OptimumResistance;
        public int PowerMaxAdd;
        public float PushBottomUp;
        public float RVExtraAngularImpulse;
        public float RVExtraTorqueFactor;
        public float RVFrictionEqualizer;
        public float RVInertiaPitch;
        public float RVInertiaRoll;
        public float RVInertiaYaw;
        public float RVSpinTorquePitch;
        public float RVSpinTorqueRoll;
        public float RVSpinTorqueYaw;
        public float RearWheelFrictionScalar;
        public float ReverseGearRation;
        public Vector3[] ShockAttachPoints;
        public float ShockEffectThreshold;
        public float[] ShockScale;
        public Vector3 SkirtExtents;
        public float SpeedLimiter;
        public float SteeringFullSpeedLimit;
        public float SteeringMaxAngle;
        public FrontRear SuspensionDampeningCoefficientCompression;
        public FrontRear SuspensionDampeningCoefficientExtension;
        public FrontRear SuspensionLength;
        public FrontRear SuspensionStrength;
        public short TorqueMax;
        public float TransmissionRatio;
        public VehicleTrick[] Tricks;
        public byte TurretSize;
        public short UpshiftRPM;
        public short VehicleFlags;
        public byte VehicleType;
        public byte WheelAxle;
        public byte WheelExistance;
        public Vector3[] WheelHardPoints;
        public float[] WheelRadius;
        public FrontRear WheelTorqueRatios;
        public float[] WheelWidth;

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
            vs.WheelRadius = br.Read<float>(6);
            vs.WheelWidth = br.Read<float>(6);
            vs.SpeedLimiter = br.ReadSingle();
            vs.AbsoluteTopSpeed = br.ReadSingle();

            vs.ShockAttachPoints = new Vector3[6];
            for (var i = 0; i < 6; ++i)
                vs.ShockAttachPoints[i] = Vector3.Read(br);

            vs.DrawAxles = br.ReadBytes(2);
            vs.DrawShocks = br.ReadBytes(2);
            vs.AxleScale = br.Read<float>(2);
            vs.ShockScale = br.Read<float>(2);
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
            vs.GearRatios = br.Read<float>(5);
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
