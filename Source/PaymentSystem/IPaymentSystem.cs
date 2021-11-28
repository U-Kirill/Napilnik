namespace Source.PaymentSystem
{
    public interface IPaymentSystem
    {
        public string GetPayingLink(Order order);
    }
}