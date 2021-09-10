using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace Source
{
    public class VoteDBConnection : IDisposable
    {
        private readonly string _connectionString = 
            string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");

        private SQLiteConnection _connection;

        public VoteDBConnection()
        {
            OpenConnection(_connectionString);
        }

        public void Dispose() =>
            _connection.Close();

        public SQLiteDataAdapter CreateAdapter(string commandText) => 
            new SQLiteDataAdapter(new SQLiteCommand(commandText, _connection));

        private void OpenConnection(string connectionString)
        {
            try
            {
                OpenSqlConnection(connectionString);
            }
            catch (SQLiteException e)
            {
                ThrowExeption(e);
            }
        }

        private void OpenSqlConnection(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
        }

        private void ThrowExeption(SQLiteException e)
        {
            if (e.ErrorCode == 1)
                throw new MissingDataBaseFileException(_connectionString, e);

            throw e;
        }
    }
}