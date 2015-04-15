using System;
using System.Data;

namespace Genesis.Utils.Extensions
{
    public static class DataReaderExtensions
    {
        public static UInt16 GetUInt16(this IDataReader reader, Int32 column)
        {
            return (UInt16) reader.GetValue(column);
        }

        public static UInt32 GetUInt32(this IDataReader reader, Int32 column)
        {
            return (UInt32) reader.GetValue(column);
        }

        public static UInt64 GetUInt64(this IDataReader reader, Int32 column)
        {
            return (UInt64) reader.GetValue(column);
        }
    }
}
