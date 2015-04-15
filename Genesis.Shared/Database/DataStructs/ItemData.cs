using System;
using System.Data;

namespace Genesis.Shared.Database.DataStructs
{
    public class ItemData
    {
        public Int64 Coid { get; set; }
        public Int32 Cbid { get; set; }
        public String TableName { get; set; }

        public static ItemData Read(IDataReader reader)
        {
            if (reader.Read())
            {
                return new ItemData
                {
                    Coid = reader.GetInt64(0),
                    Cbid = reader.GetInt32(1),
                };
            }

            return null;
        }
    }
}
