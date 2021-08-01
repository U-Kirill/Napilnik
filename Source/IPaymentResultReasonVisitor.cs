namespace IMJunior
{
    public interface IPaymentResultReasonVisitor
    {
        void Visit(IPaymentResultReason visit);
    }
}