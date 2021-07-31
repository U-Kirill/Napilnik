namespace IMJunior
{
    public interface IPaymentService
    {
        string SystemId { get; }
        string Status { get; }
        void BeginPayment(PaymentHandler paymentHandler);
    }
}