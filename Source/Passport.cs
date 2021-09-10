using System;

namespace Source
{
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
}