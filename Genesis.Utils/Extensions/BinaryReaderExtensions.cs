using System;
using System.IO;
using System.Text;

namespace Genesis.Utils.Extensions
{
    public static class BinaryReaderExtensions
    {
        public delegate T ReadFunction<out T>();

        public static string ReadLengthedString(this BinaryReader br)
        {
            var size = br.ReadInt32();
            return size > 0 ? br.ReadUtf8StringOn(size) : string.Empty;
        }

        public static string ReadUnicodeString(this BinaryReader br, int size)
        {
            if (size > 0)
            {
                var buff = br.ReadBytes(size * 2);

                for (var i = 0; i < buff.Length; i += 2)
                    if (buff[i] == 0 && buff[i + 1] == 0)
                        return Encoding.Unicode.GetString(buff, 0, i);

                return Encoding.Unicode.GetString(buff);
            }

            return string.Empty;
        }

        public static string ReadLineOn(this BinaryReader br, int size)
        {
            if (size <= 0)
                return null;

            try
            {
                var i = 0;
                char ch;
                var sb = new StringBuilder();
                while (i++ < size && (ch = br.ReadChar()) != '\n')
                    sb.Append(ch);

                return sb.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string ReadUtf8StringOn(this BinaryReader br, int size)
        {
            if (size > 0)
            {
                var buff = br.ReadBytes(size);

                for (var i = 0; i < size; ++i)
                    if (buff[i] == 0)
                        return Encoding.UTF8.GetString(buff, 0, i);

                return Encoding.UTF8.GetString(buff);
            }

            return string.Empty;
        }

        public static string ReadUtf8StringNull(this BinaryReader br)
        {
            char c;
            var sb = new StringBuilder();

            while (br.BaseStream.Position + 1 < br.BaseStream.Length && (c = br.ReadChar()) != '\0')
                sb.Append(c);

            return sb.ToString();
        }

        public static string ReadUtf16StringNull(this BinaryReader br)
        {
            var sb = new StringBuilder();

            while (br.BaseStream.Position + 2 < br.BaseStream.Length)
            {
                var b = br.ReadUInt16();
                if (b == 0)
                    break;

                sb.Append((char)b);
            }

            return sb.ToString();
        }

        public static T[] Read<T>(this BinaryReader br, int count)
        {
            var arr = new T[count];

            ReadFunction<T> func;

            switch (typeof(T).Name)
            {
                case "Single":
                    func = () => (T)(object)br.ReadSingle();
                    break;

                case "Double":
                    func = () => (T)(object)br.ReadDouble();
                    break;

                case "Byte":
                    func = () => (T)(object)br.ReadByte();
                    break;

                case "UInt16":
                    func = () => (T)(object)br.ReadUInt16();
                    break;

                case "UInt32":
                    func = () => (T)(object)br.ReadUInt32();
                    break;

                case "UInt64":
                    func = () => (T)(object)br.ReadUInt64();
                    break;

                case "SByte":
                    func = () => (T)(object)br.ReadSByte();
                    break;

                case "Int16":
                    func = () => (T)(object)br.ReadInt16();
                    break;

                case "Int32":
                    func = () => (T)(object)br.ReadInt32();
                    break;

                case "Int64":
                    func = () => (T)(object)br.ReadInt64();
                    break;

                case "String":
                    func = () => (T)(object)br.ReadString();
                    break;

                case "Decimal":
                    func = () => (T)(object)br.ReadDecimal();
                    break;

                case "Boolean":
                    func = () => (T)(object)br.ReadBoolean();
                    break;

                default:
                    func = () => default(T);
                    break;
            }

            for (var i = 0; i < count; ++i)
                arr[i] = func();

            return arr;
        }

        public static BinaryReader ReadPadding(this BinaryReader br, int count)
        {
            br.BaseStream.Position += count;
            return br;
        }
    }
}
