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
                _voterRecords =
                    new VoterRecords(string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite"));
            }
            catch (Exception e)
            {
                if (e.ErrorCode == 1)
                    ShowWindow("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
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

            DataRow voiterRow = _voterRecords.Find(passport);

            if (voiterRow == null)
            {
                RefreshResult("Паспорт «" + passport.RawSeries + "» в списке участников дистанционного голосования НЕ НАЙДЕН");
                return;
            }

            if (Convert.ToBoolean(voiterRow.ItemArray[1]))
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
        private string _connectionString;
        private SQLiteConnection _connection;

        private VoteDBConnection _voteDbConnection;

        public VoterRecords(string connectionString)
        {
            OpenConnection(connectionString);
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public DataRow Find(Passport passport)
        {
            string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(passport.FormattedSeries));

            SQLiteDataAdapter sqLiteDataAdapter = _voteDbConnection.CreateAdapter(commandText);
            DataTable dataTable = new DataTable();
            sqLiteDataAdapter.Fill(dataTable);


            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0];

            return null;
        }

        private void OpenConnection(string connectionString)
        {
            try
            {
                OpenSqlConnection(connectionString);
            }
            catch (Exception e)
            {
                if (e.ErrorCode == 1)
                    int num2 = (int)MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");

                throw;
            }
        }

        private void OpenSqlConnection(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
        }
    }

    public class VoteDBConnection : IDisposable
    {
        public SQLiteConnection Connection { get; private set; }
        private string _connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");

        public VoteDBConnection()
        {
            OpenConnection(_connectionString);
        }

        public void Dispose()
        {
            Connection.Close();
        }

        public SQLiteDataAdapter CreateAdapter(string commandText) => 
            new SQLiteDataAdapter(new SQLiteCommand(commandText, Connection));

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
            Connection = new SQLiteConnection(connectionString);
            Connection.Open();
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