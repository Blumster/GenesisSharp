using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct HeadDetail
    {
        public int CloneBase;
        public int DisableHair;
        public string FileName;
        public int HeadBody;
        public int Id;
        public int MaxTextures;
        public byte Type;

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
            return $"Id: {Id} | File: {FileName} ";
        }
    }
}
