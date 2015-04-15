using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    public struct Vector3
    {
        private Single _x, _y, _z;
        public Single X { get { return _x; } set { _x = value; } }
        public Single Y { get { return _y; } set { _y = value; } }
        public Single Z { get { return _z; } set { _z = value; } }

        public Vector3(Single x, Single y, Single z)
        {
            _x = x;
            _y = y;
            _z = z;
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
            return String.Format("X: {0} | Y: {1} | Z: {2}", X, Y, Z);
        }
    }
}
