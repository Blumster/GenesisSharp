using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct HeadDetail
    {
        public Int32 CloneBase;
        public Int32 DisableHair;
        public String FileName;
        public Int32 HeadBody;
        public Int32 Id;
        public Int32 MaxTextures;
        public Byte Type;

        public static HeadDetail Read(BinaryReader br)
        {
            var hd = new HeadDetail
            {
                Id = br.ReadInt32(),
                HeadBody = br.ReadInt32(),
                CloneBase = br.ReadInt32(),
                FileName = br.ReadUnicodeString(65),
                Type = br.ReadByte()
            };

            br.ReadByte();

            hd.MaxTextures = br.ReadInt32();
            hd.DisableHair = br.ReadInt32();

            return hd;
        }

        public override string ToString()
        {
            return String.Format("Id: {0} | File: {1} ", Id, FileName);
        }
    }
}
