using System;
using System.Threading;

namespace IMJunior
{
    public class WebMoneyPaymentService : IPaymentService
    {
        private readonly PaymentHandler _paymentHandler;

        public WebMoneyPaymentService(PaymentHandler paymentHandler)
        {
            _paymentHandler = paymentHandler;
        }

        public string SystemId => "WebMoney";

        public void BeginPayment(PaymentHandler paymentHandler)
        {
            Console.WriteLine("Вызов API WebMoney...");
        }

        public Result<IPaymentResultReason> IsCorrectPayment()
        {
            return Result<IPaymentResultReason>.CreateSuccessful(new AllCorrect());
        }
    }
}