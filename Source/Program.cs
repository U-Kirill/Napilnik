using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace Source
{
    public class Program
    {
        private const string FileIsAbsentErrorText = "Файл db.sqlite не найден. Положите файл в папку вместе с exe.";

        public static void Main(string[] args)
        {
        }

        private void OnDisplayVoteOpportunity(object sender, EventArgs e)
        {
            if (HasPassportData())
                DisplayVoteOpportunity(GetRawPassportData());
            else
                AskPassportData();
        }

        private void DisplayVoteOpportunity(string rawPassportData)
        {
            if (IsValidPassportData(rawPassportData))
                DisplayOpportunity(rawPassportData);
            else
                UpdateResult("Неверный формат серии или номера паспорта");
        }

        private void DisplayOpportunity(string rawPassportData)
        {
            try
            {
                UpdateVoteOpportunityResult(rawPassportData);
            }
            catch (SQLiteException ex)
            {
                TryReportAboutAbsetnDBFile(ex);
            }
        }

        private void UpdateVoteOpportunityResult(string rawPassportData)
        {
            string result = GetOpportunityResult(rawPassportData);

            UpdateResult(result);
        }

        private void TryReportAboutAbsetnDBFile(SQLiteException ex)
        {
            int fileIsAbsentError = 1;
            
            if (ex.ErrorCode != fileIsAbsentError)
                return;

            int num2 = (int) MessageBox.Show(FileIsAbsentErrorText);
        }

        private string GetOpportunityResult(string rawPassportData)
        {
            DataTable votingTable = GetVotingTable(rawPassportData);

            if (HasResults(votingTable))
                return GetVotingMessage(votingTable);

            return "Паспорт «" + this.passportTextbox.Text + "» в списке участников дистанционного голосования НЕ НАЙДЕН";

        }

        private string GetVotingMessage(DataTable votingTable)
        {
            if (CanVote(votingTable))
                return "По паспорту «" + this.passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
            
            return "По паспорту «" + this.passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
        }

        private bool CanVote(DataTable dataTable)
        {
            int rowWithUser = 0;
            int itemWithVoteOpportunity = 1;
            
            return Convert.ToBoolean(dataTable.Rows[rowWithUser].ItemArray[itemWithVoteOpportunity]);
        }

        private bool HasPassportData() =>
            GetRawPassportData() == string.Empty;

        private bool HasResults(DataTable dataTable) => 
            dataTable.Rows.Count > 0;

        private bool IsValidPassportData(string rawData) => 
            rawData.Length >= 10;

        private void UpdateResult(string result) => 
            this.textResult.Text = result;

        private string GetRawPassportData() => 
            this.passportTextbox.Text.Trim().Replace(" ", string.Empty);

        private void AskPassportData()
        {
            int num1 = (int) MessageBox.Show("Введите серию и номер паспорта");
        }

        private DataTable GetVotingTable(string rawData)
        {
            string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object) Form1.ComputeSha256Hash(rawData));
            string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");
            DataTable dataTable = GetTable(connectionString, commandText);
            return dataTable;
        }

        private DataTable GetTable(string connectionString, string commandText)
        {
            using var connection = CreateConnection(connectionString);
            var sqLiteDataAdapter = CreateAdapter(commandText, connection);

            return GetFilledTable(sqLiteDataAdapter);
        }

        private SQLiteConnection CreateConnection(string connectionString)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }

        private SQLiteDataAdapter CreateAdapter(string commandText, SQLiteConnection connection)
        {
            SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
            return sqLiteDataAdapter;
        }

        private DataTable GetFilledTable(SQLiteDataAdapter sqLiteDataAdapter)
        {
            DataTable dataTable = new DataTable();
            sqLiteDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}