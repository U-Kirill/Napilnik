namespace Source.Builder.Info
{
    public class CombinedAmountCurrencyInfo : IInfoProvider
    {
        public CombinedAmountCurrencyInfo(int amount, string currency)
        {
            Info = $"amount={amount}{currency}";
        }

        public string Info { get; }
    }
}