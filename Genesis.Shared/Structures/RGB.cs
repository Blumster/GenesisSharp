using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    public struct RGB
    {
        public Single B;
        public Single G;
        public Single R;

        public static RGB Read(BinaryReader br)
        {
            return new RGB
            {
                R = br.ReadSingle(),
                G = br.ReadSingle(),
                B = br.ReadSingle()
            };
        }

        public override string ToString()
        {
            return String.Format("R: {0} | G: {1} | B: {2}", R, G, B);
        }
    }
}
