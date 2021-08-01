using System;
using System.Diagnostics.CodeAnalysis;

namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            var paymentHandler = new PaymentHandler();
            var paymentServiceFactory = new PaymentServiceFactory(paymentHandler);
            var orderForm = new OrderForm(paymentServiceFactory);

            var serviceId = orderForm.ShowForm();

            IPaymentService paymentService = paymentServiceFactory.Create(serviceId);
            paymentService.BeginPayment(paymentHandler);

            Console.ReadLine();
        }
    }
}