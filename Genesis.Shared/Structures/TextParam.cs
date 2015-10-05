using System.IO;

namespace Genesis.Shared.Structures
{
    public struct TextParam
    {
        public float CachedValue;
        public uint Id;
        public byte Type;

        public static TextParam Read(BinaryReader br, uint mapVersion)
        {
            var tParam = new TextParam { Type = br.ReadByte() };

            br.ReadBytes(3);

            tParam.Id = br.ReadUInt32();
            tParam.CachedValue = mapVersion < 14 ? 0.0f : br.ReadSingle();

            return tParam;
        }
    }
}
