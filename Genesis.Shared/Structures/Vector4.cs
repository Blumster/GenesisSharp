using System.IO;

namespace Genesis.Shared.Structures
{
    public struct Vector4
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static Vector4 Read(BinaryReader br)
        {
            return new Vector4
            {
                X = br.ReadSingle(),
                Y = br.ReadSingle(),
                Z = br.ReadSingle(),
                W = br.ReadSingle()
            };
        }

        public override string ToString()
        {
            return $"X: {X} | Y: {Y} | Z: {Z} | W: {W}";
        }
    }
}
