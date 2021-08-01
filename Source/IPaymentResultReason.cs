namespace IMJunior
{
    public interface IPaymentResultReason : IResultReason
    {
        void Accept(IPaymentResultReasonVisitor visitor);
    }
}