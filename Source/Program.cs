using System;
using System.Collections.Generic;
using System.Linq;

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
            
            paymentService.BeginPayment();
            
            //if (systemId == "QIWI")
            //    Console.WriteLine("Перевод на страницу QIWI...");
            //else if (systemId == "WebMoney")
            //    Console.WriteLine("Вызов API WebMoney...");
            //else if (systemId == "Card")
            //    Console.WriteLine("Вызов API банка эмитера карты Card...");

            paymentHandler.ShowPaymentResult(paymentService);
        }

        private static PaymentServiceStorage CreatePaymentServiceStorage()
        {
            return new PaymentServiceStorage(new QiwiPaymentService(), new WebMoneyPaymentService(), new CardPaymentService());
        }
    }

    internal class CardPaymentService : IPaymentService
    {
        public string SystemId => "Card";
        public string Status { get; }
        public void BeginPayment()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
        }
    }

    internal class WebMoneyPaymentService : IPaymentService
    {
        public string SystemId => "WebMoney";
        public string Status { get; }
        public void BeginPayment()
        {
            Console.WriteLine("Вызов API WebMoney...");
        }
    }

    internal class QiwiPaymentService : IPaymentService
    {
        public string SystemId => "QIWI";
        public string Status { get; }
        public void BeginPayment()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
        }
    }

    public class PaymentServiceStorage
    {
        private Dictionary<string, IPaymentService> _paymentServices;

        public PaymentServiceStorage(params IPaymentService[] paymentServices)
        {
            _paymentServices = paymentServices
                .ToDictionary(x => x.SystemId, x => x);
        }

        public IEnumerable<IPaymentService> All => _paymentServices.Values;
        
        public IPaymentService Get(string forId) =>
            _paymentServices[forId];
    }

    public class OrderForm
    {
        private readonly PaymentServiceStorage _paymentServiceStorage;

        public OrderForm(PaymentServiceStorage paymentServiceStorage) => 
            _paymentServiceStorage = paymentServiceStorage ?? throw new ArgumentNullException(nameof(paymentServiceStorage));

        public string ShowForm()
        {
            PrintAllServicesId();
            
            //симуляция веб интерфейса
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }

        private void PrintAllServicesId()
        {
            Console.WriteLine("Мы принимаем:");
            foreach (IPaymentService service in _paymentServiceStorage.All)
            {
                Console.WriteLine($"{service.SystemId}");
            }
        }
    }

    public class PaymentHandler
    {
        public void ShowPaymentResult(IPaymentService systemId)
        {
            Console.WriteLine($"Вы оплатили с помощью {systemId.SystemId}");

            Console.WriteLine(systemId.Status);
            //if (systemId == "QIWI")
            //    Console.WriteLine("Проверка платежа через QIWI...");
            //else if (systemId == "WebMoney")
            //    Console.WriteLine("Проверка платежа через WebMoney...");
            //else if (systemId == "Card")
            //    Console.WriteLine("Проверка платежа через Card...");

            Console.WriteLine("Оплата прошла успешно!");
        }
    }

    public interface IPaymentService
    {
       string SystemId { get; }
       string Status { get; }
       void BeginPayment();
    }
}