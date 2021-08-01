using System;
using System.Threading;
using System.Threading.Tasks;

namespace IMJunior
{
    public class QiwiPaymentService : IPaymentService
    {
        private readonly PaymentHandler _paymentHandler;

        public QiwiPaymentService(PaymentHandler paymentHandler, string systemId)
        {
            _paymentHandler = paymentHandler;
            SystemId = systemId;
        }

        public string SystemId { get; }

        public void BeginPayment(PaymentHandler paymentHandler)
        {
            Console.WriteLine("Перевод на страницу QIWI...");
            _paymentHandler.ShowPaymentResult(this);
        }

        public Result<IPaymentResultReason> IsCorrectPayment()
        {
            return Result<IPaymentResultReason>.CreateSuccessful(new AllCorrect());
        }
    }
}