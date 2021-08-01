using System;
using System.Threading;
using System.Threading.Tasks;

namespace IMJunior
{
    public class QiwiPaymentService : IPaymentService
    {
        private readonly PaymentHandler _paymentHandler;

        public QiwiPaymentService(PaymentHandler paymentHandler)
        {
            _paymentHandler = paymentHandler;
        }

        public string SystemId => "QIWI";

        public void BeginPayment(PaymentHandler paymentHandler)
        {
            Console.WriteLine("Перевод на страницу QIWI...");
        }

        public Result<IPaymentResultReason> IsCorrectPayment()
        {
            return Result<IPaymentResultReason>.CreateSuccessful(new AllCorrect());
        }
    }
}