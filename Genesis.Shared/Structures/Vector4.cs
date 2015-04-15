using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    public struct Vector4
    {
        private Single _x, _y, _z, _w;

        public Single X { get { return _x; } set { _x = value; } }
        public Single Y { get { return _y; } set { _y = value; } }
        public Single Z { get { return _z; } set { _z = value; } }
        public Single W { get { return _w; } set { _w = value; } }

        public Vector4(Single x, Single y, Single z, Single w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
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

        public override String ToString()
        {
            return String.Format("X: {0} | Y: {1} | Z: {2} | W: {3}", X, Y, Z, W);
        }
    }
}
