namespace IMJunior
{
    // Nullabale payment result
    public class AllCorrect : IPaymentResultReason
    {
        public void Accept(IPaymentResultReasonVisitor visitor) => 
            visitor.Visit(this);
    }
}