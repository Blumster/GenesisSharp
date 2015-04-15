using System;
using System.IO;

namespace Genesis.Shared.Structures.Asset
{
    public class FileEntry
    {
        public UInt32 Offset;
        public UInt32 Size;
        public UInt32 RealSize;
        public UInt32 ModifiedTime;
        public UInt16 Scheme;
        public UInt32 PackFile;
        public String Name;

        public Boolean IsRead { get; set; }
        public String FileName { get; set; }

        public void Read(BinaryReader br, String fName)
        {
            FileName = fName;

            Offset = br.ReadUInt32();
            Size = br.ReadUInt32();
            RealSize = br.ReadUInt32();
            ModifiedTime = br.ReadUInt32();
            Scheme = br.ReadUInt16();
            PackFile = br.ReadUInt32();
        }

        public override String ToString()
        {
            return String.Format("FileName: {0} | Name: {1}", FileName, Name);
        }
    }
}
