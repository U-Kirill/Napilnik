namespace Source.Builder.Info
{
    public class AmountInfo : IInfoProvider
    {
        private readonly int _amount;

        public AmountInfo(int amount)
        {
            _amount = amount;
        }

        public string Info => $"amount={_amount}";
    }
}