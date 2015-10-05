using System.IO;

namespace Genesis.Shared.Structures.Asset
{
    public class FileEntry
    {
        public uint Offset;
        public uint Size;
        public uint RealSize;
        public uint ModifiedTime;
        public ushort Scheme;
        public uint PackFile;
        public string Name;

        public bool IsRead { get; set; }
        public string FileName { get; set; }

        public void Read(BinaryReader br, string fName)
        {
            FileName = fName;

            Offset = br.ReadUInt32();
            Size = br.ReadUInt32();
            RealSize = br.ReadUInt32();
            ModifiedTime = br.ReadUInt32();
            Scheme = br.ReadUInt16();
            PackFile = br.ReadUInt32();
        }

        public override string ToString()
        {
            return $"FileName: {FileName} | Name: {Name}";
        }
    }
}
