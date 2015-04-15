using System;
using System.Data;

namespace Genesis.Shared.Database
{
    public interface IDatabaseAccess
    {
        void Initialize(String connectionString);
        void Close();
        IDbTransaction StartTransaction();
        IDbConnection Connection { get; }
        IDbCommand CreateCommand(String cmdText);
    }
}
