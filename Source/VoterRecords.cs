using System;
using System.Data;
using System.Data.SQLite;

namespace Source
{
    public class VoterRecords : IDisposable
    {
        private readonly VoteDBConnection _voteDbConnection = new VoteDBConnection();

        public void Dispose() => 
            _voteDbConnection.Dispose();

        public Voter Find(Passport passport)
        {
            string commandText = CreateCommand(passport);
            SQLiteDataAdapter sqLiteDataAdapter = _voteDbConnection.CreateAdapter(commandText);
            DataTable dataTable = CreateFilledDataTable(sqLiteDataAdapter);

            return CreateVoter(passport, dataTable);
        }

        private string CreateCommand(Passport passport) => 
            string.Format("select * from passports where num='{0}' limit 1;",
                (object)Form1.ComputeSha256Hash(passport.FormattedSeries));

        private DataTable CreateFilledDataTable(SQLiteDataAdapter sqLiteDataAdapter)
        {
            DataTable dataTable = new DataTable();
            sqLiteDataAdapter.Fill(dataTable);
            return dataTable;
        }

        private Voter CreateVoter(Passport passport, DataTable dataTable)
        {
            return HasVoter(dataTable) 
                ? new Voter(GetVoterRow(dataTable), passport)
                : new Voter(passport);
        }

        private  DataRow GetVoterRow(DataTable dataTable) => 
            dataTable.Rows[0];

        private bool HasVoter(DataTable dataTable) => 
            dataTable.Rows.Count > 0;
    }
}