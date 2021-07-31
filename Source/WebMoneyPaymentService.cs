using System;
using System.Threading;

namespace IMJunior
{
    public class WebMoneyPaymentService : PaymentService, IPaymentService
    {
        public string SystemId => "WebMoney";

        public string Status { get; private set; }

        protected override void ShowPaymentInterface(Action onPaymentConfirmed)
        {
            Console.WriteLine("Вызов API WebMoney...");

            Status = $"Вы оплатили с помощью {SystemId}";

            onPaymentConfirmed();
        }

        protected override void ProcessPayment(Action onCompleted)
        {
            Status = "Проверка платежа через Web Money...";
            Thread.Sleep(5000);
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