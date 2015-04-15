using System.IO;

namespace Genesis.Shared.Clonebase
{
    using Structures;
    using Structures.Specifics;

    public class CloneBaseVehicle : CloneBaseObject
    {
        public VehicleSpecific VehicleSpecific;

        public CloneBaseVehicle(BinaryReader br)
            : base(br)
        {
            VehicleSpecific = VehicleSpecific.Read(br);

            VehicleSpecific.Tricks = new VehicleTrick[VehicleSpecific.NumberOfTricks];
            for (var i = 0; i < VehicleSpecific.NumberOfTricks; ++i)
                VehicleSpecific.Tricks[i] = VehicleTrick.Read(br);
        }
    }
}
