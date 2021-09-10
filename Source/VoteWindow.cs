using System;

namespace Source
{
    public class VoteWindow
    {
        private const string PassportNotExistMessage = "Паспорт «{0}» в списке участников дистанционного голосования НЕ НАЙДЕН";
        private const string VotingAccessMessage = "По паспорту «{0}» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";

        private VoterRecords _voterRecords;
        private string VotingDeclineMessage = "По паспорту «{0}» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";

        public VoteWindow()
        {
            CreateVoterRecords();
        }

        public void OnCheckButton(object sender, EventArgs e)
        {
            var passport = new Passport(this.passportTextbox.Text);

            if (TryInformAboutBadSerial(passport))
                return;

            Voter voter = _voterRecords.Find(passport);

            DisplayVoteOpportunity(voter);
        }

        private bool TryInformAboutBadSerial(Passport passport)
        {
            if (!passport.HasSeries)
            {
                ShowWindow("Введите серию и номер паспорта");
                return true;
            }

            if (!passport.IsValid)
            {
                RefreshResult("Неверный формат серии или номера паспорта");
                return true;
            }

            return false;
        }

        private void DisplayVoteOpportunity(Voter voter) => 
            RefreshResult(GetVoteOpportunityMessage(voter));

        private string GetVoteOpportunityMessage(Voter voter) =>
            voter.Registred 
                ? GetVoteAccessMessage(voter) 
                : string.Format(PassportNotExistMessage, voter.Passport.RawSeries);

        private string GetVoteAccessMessage(Voter voter) =>
            voter.CanVote
                ? string.Format(VotingAccessMessage, voter.Passport.RawSeries)
                : string.Format(VotingDeclineMessage, voter.Passport.RawSeries);

        private void CreateVoterRecords()
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

        private void RefreshResult(string result) => 
            this.textResult.Text = result;

        private void ShowWindow(string message) => 
            MessageBox.Show(message);
    }
}