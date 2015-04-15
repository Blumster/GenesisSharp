using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct HeadBody
    {
        public Int32 CloneBase;
        public String FileName;
        public Int32 Id;
        public Int32 IsBody;
        public Int32 IsHead;
        public Int32 MaxTextures;

        public static HeadBody Read(BinaryReader br)
        {
            var hb = new HeadBody
            {
                Id = br.ReadInt32(),
                CloneBase = br.ReadInt32(),
                IsHead = br.ReadInt32(),
                IsBody = br.ReadInt32(),
                FileName = br.ReadUnicodeString(65)
            };

            br.ReadBytes(2);

            hb.MaxTextures = br.ReadInt32();

            return hb;
        }

        public override string ToString()
        {
            return String.Format("Id: {0} | File: {1} ", Id, FileName);
        }
    }
}
