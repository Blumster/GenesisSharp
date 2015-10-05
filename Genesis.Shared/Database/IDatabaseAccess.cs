using System.Data;

namespace Genesis.Shared.Database
{
    public interface IDatabaseAccess
    {
        void Initialize(string connectionString);
        void Close();
        IDbTransaction StartTransaction();
        IDbConnection Connection { get; }
        IDbCommand CreateCommand(string cmdText);
    }
}
