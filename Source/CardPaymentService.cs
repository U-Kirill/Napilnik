using System;
using System.Threading;

namespace IMJunior
{
    public class CardPaymentService : IPaymentService
    {
        private readonly PaymentHandler _paymentHandler;

        public CardPaymentService(PaymentHandler paymentHandler)
        {
            _paymentHandler = paymentHandler;
        }

        public string SystemId => "Card";

        public void BeginPayment(PaymentHandler paymentHandler)
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
        }

        public Result<IPaymentResultReason> IsCorrectPayment()
        {
            return Result<IPaymentResultReason>.CreateSuccessful(new AllCorrect());
        }
    }
}