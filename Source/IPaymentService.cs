namespace IMJunior
{
    public interface IPaymentService
    {
        string SystemId { get; }
        void BeginPayment(PaymentHandler paymentHandler);
        Result<IPaymentResultReason> IsCorrectPayment();
    }
}