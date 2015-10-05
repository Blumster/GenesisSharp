using System.IO;

namespace Genesis.Shared.Structures
{
    public struct Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 Read(BinaryReader br)
        {
            return new Vector3
            {
                X = br.ReadSingle(),
                Y = br.ReadSingle(),
                Z = br.ReadSingle()
            };
        }

        public override string ToString()
        {
            return $"X: {X} | Y: {Y} | Z: {Z}";
        }
    }
}
