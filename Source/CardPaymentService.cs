using System;
using System.Threading;

namespace IMJunior
{
    public class CardPaymentService : IPaymentService
    {
        private readonly PaymentHandler _paymentHandler;

        public CardPaymentService(PaymentHandler paymentHandler, string systemId)
        {
            _paymentHandler = paymentHandler;
            SystemId = systemId;
        }

        public string SystemId { get; }

        public void BeginPayment(PaymentHandler paymentHandler)
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
            _paymentHandler.ShowPaymentResult(this);
        }

        public Result<IPaymentResultReason> IsCorrectPayment() => 
            Result<IPaymentResultReason>.CreateSuccessful(new AllCorrect());
    }
}