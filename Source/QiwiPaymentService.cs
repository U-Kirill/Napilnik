using System;
using System.Threading;
using System.Threading.Tasks;

namespace IMJunior
{
    public class QiwiPaymentService : PaymentService, IPaymentService
    {
        public string SystemId => "QIWI";

        public string Status { get; private set; }

        protected override void ShowPaymentInterface(Action onPaymentConfirmed)
        {
            Console.WriteLine("Перевод на страницу QIWI...");

            Status = $"Вы оплатили с помощью {SystemId}";

            onPaymentConfirmed();
        }

        protected override async void ProcessPayment(Action onCompleted)
        {
            Status = "Проверка платежа через QIWI...";
            await Task.Run(() => Thread.Sleep(5000));
            onCompleted();
        }

        protected override void ShowStatus(PaymentHandler paymentHandler)
        {
            paymentHandler.ShowStatus(this);
        }

        protected override void ShowResult(PaymentHandler paymentHandler)
        {
            paymentHandler.ShowPaymentResult(this);
        }
    }
}