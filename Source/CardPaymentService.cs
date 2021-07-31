using System;
using System.Threading;

namespace IMJunior
{
    public class CardPaymentService : PaymentService, IPaymentService
    {
        public string SystemId => "Card";

        public string Status { get; private set; }

        protected override void ShowPaymentInterface(Action onPaymentConfirmed)
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
            
            Status = $"Вы оплатили с помощью {SystemId}";
            
            onPaymentConfirmed();
        }

        protected override void ProcessPayment(Action onCompleted)
        {
            Status = "Проверка платежа через Card...";
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