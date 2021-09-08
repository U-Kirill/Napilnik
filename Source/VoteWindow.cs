using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace Source
{
    public class VoteWindow
    {
        private readonly VoterRecords _voterRecords;
        
        public VoteWindow()
        {
            try
            {
                _voterRecords = new VoterRecords();
            }
            catch (Exception e)
            {
                ShowWindow(e.Message);
            }
        }
        
        private void checkButton_Click(object sender, EventArgs e)
        {
            Passport passport = new Passport(this.passportTextbox.Text);
            
            if (!passport.HasSeries)
            {
                ShowWindow("Введите серию и номер паспорта");
                return;
            }
            
            if (!passport.IsValid)
            {
                RefreshResult("Неверный формат серии или номера паспорта");
                return;
            }

            Voiter voiter = _voterRecords.Find(passport);

            if (voiter == null)
            {
                RefreshResult("Паспорт «" + passport.RawSeries + "» в списке участников дистанционного голосования НЕ НАЙДЕН");
                return;
            }

            if (voiter.CanVote)
                RefreshResult("По паспорту «" + passport.RawSeries + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН");
            else
                RefreshResult("По паспорту «" + passport.RawSeries + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ");
        }

        private void RefreshResult(string result)
        {
            this.textResult.Text = result;
        }

        private void ShowWindow(string message)
        {
            MessageBox.Show(message);
        }
    }

    public class Passport
    {
        private const int MinSerialLenght = 10;
        
        public readonly string FormattedSeries;
        public readonly string RawSeries;
        
        public Passport(string passportSeries)
        {
            RawSeries = passportSeries ?? throw new ArgumentNullException(nameof(passportSeries));
            FormattedSeries = RawSeries.Trim().Replace(" ", string.Empty);
        }

        public bool HasSeries => FormattedSeries != string.Empty;

        public bool IsValid => FormattedSeries.Length >= MinSerialLenght;
    }

    public class VoterRecords : IDisposable
    {
        private readonly VoteDBConnection _voteDbConnection = new VoteDBConnection();

        public void Dispose()
        {
            _voteDbConnection.Dispose();
        }

        public Voiter Find(Passport passport)
        {
            string commandText = CreateCommand(passport);

            SQLiteDataAdapter sqLiteDataAdapter = _voteDbConnection.CreateAdapter(commandText);
            DataTable dataTable = new DataTable();
            sqLiteDataAdapter.Fill(dataTable);


            if (dataTable.Rows.Count > 0)
                return new Voiter(dataTable.Rows[0], passport);

            return null;
        }

        private string CreateCommand(Passport passport) => 
            string.Format("select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(passport.FormattedSeries));
    }

    public class Voiter
    {
        public Voiter(DataRow dataTableRow, Passport passport)
        {
            Pasport = passport;
            CanVote = Convert.ToBoolean(dataTableRow.ItemArray[1]);
        }

        public bool CanVote { get; }

        public Passport Pasport { get; }
    }

    public class VoteDBConnection : IDisposable
    {
        private readonly string _connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");
        
        private SQLiteConnection _connection;

        public VoteDBConnection()
        {
            OpenConnection(_connectionString);
        }

        public void Dispose()
        {
            _connection.Close();
        }

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
                if (e.ErrorCode == 1)
                    throw new MissingDataBaseFileException(_connectionString, e);

                throw;
            }
        }

        private void OpenSqlConnection(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
        }
    }

    public sealed class MissingDataBaseFileException : Exception
    {
        private const string _message = "Файл db.sqlite не найден. Положите файл в папку вместе с exe.";

        public MissingDataBaseFileException(string connectionString, Exception inner)
            : base(_message, inner)
        {
            Data["Connection String"] = connectionString;
        }
    }
}