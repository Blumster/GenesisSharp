using System;
using System.Data;

using MySql.Data.MySqlClient;

namespace Genesis.Shared.Database.MySql
{
    public class MySqlDatabase : IDatabaseAccess
    {
        private MySqlConnection _mySqlConnection;

        public IDbConnection Connection { get { return _mySqlConnection; } }

        public void Initialize(String connectionString)
        {
            _mySqlConnection = new MySqlConnection(connectionString);
            _mySqlConnection.Open();
        }

        public IDbCommand CreateCommand(String cmdText)
        {
            return new MySqlCommand(cmdText, _mySqlConnection);
        }

        public IDbTransaction StartTransaction()
        {
            return _mySqlConnection.BeginTransaction();
        }

        public void Close()
        {
            _mySqlConnection.Close();
        }
    }
}
