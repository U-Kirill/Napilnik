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

            var serviceId = orderForm.ShowForm();
            PaymentServices paymentServices = new PaymentServices(paymentHandler);

            IPaymentService paymentService = paymentServices.Create(serviceId);
            paymentService.BeginPayment(paymentHandler);

            Console.ReadLine();
        }

        private static PaymentServiceStorage CreatePaymentServiceStorage() =>
            new PaymentServiceStorage(new QiwiPaymentService(), new WebMoneyPaymentService(), new CardPaymentService());
    }

    public class PaymentServices
    {
        private readonly PaymentHandler _paymentHandler;

        public PaymentServices(PaymentHandler paymentHandler)
        {
            _paymentHandler = paymentHandler;
            throw new NotImplementedException();
        }

        public IPaymentService Create(string serviceId)
        {
            switch (serviceId.ToLowerInvariant())
            {
                case "qiwi":
                    return new QiwiPaymentService(_paymentHandler);
                case "card":
                    return new CardPaymentService(_paymentHandler);
                case "webmoney":
                    return new WebMoneyPaymentService(_paymentHandler);
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceId));
            }
        }
    }
}