using System.IO;

namespace Genesis.Shared.Structures
{
    using Utils.Extensions;

    public struct HeadBody
    {
        public int CloneBase;
        public string FileName;
        public int Id;
        public int IsBody;
        public int IsHead;
        public int MaxTextures;

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
            return $"Id: {Id} | File: {FileName} ";
        }
    }
}
