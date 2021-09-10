using System;
using System.Data;

namespace Source
{
    public class Voter
    {
        public Voter(DataRow dataTableRow, Passport passport)
        {
            Passport = passport;
            CanVote = Convert.ToBoolean(dataTableRow.ItemArray[1]);
            Registred = true;
        }

        public Voter(Passport passport)
        {
            Passport = passport;
            CanVote = false;
            Registred = false;
        }

        public bool Registred { get; }

        public bool CanVote { get; }

        public Passport Passport { get; }
    }
}