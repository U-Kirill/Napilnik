namespace Source.Builder.Info
{
    public class CurrencyInfo : IInfoProvider
    {
        private readonly string _currency;

        public CurrencyInfo(string currency)
        {
            _currency = currency;
        }

        public string Info => $"currency={_currency}";
    }
}