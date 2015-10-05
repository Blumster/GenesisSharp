using System.Data;

namespace Genesis.Shared.Database.DataStructs
{
    public class ItemData
    {
        public long Coid { get; set; }
        public int Cbid { get; set; }
        public string TableName { get; set; }

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
