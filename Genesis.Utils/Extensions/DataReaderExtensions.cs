using System.Data;

namespace Genesis.Utils.Extensions
{
    public static class DataReaderExtensions
    {
        public static ushort GetUInt16(this IDataReader reader, int column)
        {
            return (ushort) reader.GetValue(column);
        }

        public static uint GetUInt32(this IDataReader reader, int column)
        {
            return (uint) reader.GetValue(column);
        }

        public static ulong GetUInt64(this IDataReader reader, int column)
        {
            return (ulong) reader.GetValue(column);
        }
    }
}
