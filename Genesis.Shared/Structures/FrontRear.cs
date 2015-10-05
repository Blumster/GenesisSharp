using System.IO;

namespace Genesis.Shared.Structures
{
    public struct FrontRear
    {
        public float Front;
        public float Rear;

        public static FrontRear Read(BinaryReader br)
        {
            return new FrontRear
            {
                Front = br.ReadSingle(),
                Rear = br.ReadSingle()
            };
        }
    }
}
