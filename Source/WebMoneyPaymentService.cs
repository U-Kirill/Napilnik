using System;
using System.Threading;

namespace IMJunior
{
    public class WebMoneyPaymentService : IPaymentService
    {
        private readonly PaymentHandler _paymentHandler;

        public WebMoneyPaymentService(PaymentHandler paymentHandler, string systemId)
        {
            _paymentHandler = paymentHandler;
            SystemId = systemId;
        }

        public string SystemId { get; }

        public void BeginPayment(PaymentHandler paymentHandler)
        {
            Console.WriteLine("Вызов API WebMoney...");
            _paymentHandler.ShowPaymentResult(this);
        }

        public Result<IPaymentResultReason> IsCorrectPayment()
        {
            return Result<IPaymentResultReason>.CreateSuccessful(new AllCorrect());
        }
    }
}