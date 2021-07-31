using System;
using System.Diagnostics.CodeAnalysis;

namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            var paymentServiceStorage = CreatePaymentServiceStorage();
            var orderForm = new OrderForm(paymentServiceStorage);
            var paymentHandler = new PaymentHandler();

            var systemId = orderForm.ShowForm();

            IPaymentService paymentService = paymentServiceStorage.Get(systemId);
            paymentService.BeginPayment(paymentHandler);

            Console.ReadLine();
        }

        private static PaymentServiceStorage CreatePaymentServiceStorage() =>
            new PaymentServiceStorage(new QiwiPaymentService(), new WebMoneyPaymentService(), new CardPaymentService());
    }
}