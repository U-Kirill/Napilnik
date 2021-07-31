using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

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

    public abstract class PaymentService
    {
        public void BeginPayment(PaymentHandler paymentHandler)
        {
            ShowPaymentInterface(OnConfirmPayment);

            void OnConfirmPayment() => ProcessPayment(OnPaymentComplete);
            void OnPaymentComplete() => ShowResult(paymentHandler);
        }

        protected abstract void ShowPaymentInterface(Action onConfirmPayment);

        protected abstract void ProcessPayment(Action onComplete);

        protected abstract void ShowResult(PaymentHandler paymentHandler);
    }

    internal class CardPaymentService : PaymentService, IPaymentService
    {
        public string SystemId => "Card";
        
        public string Status { get; private set; }

        protected override void ShowPaymentInterface(Action onConfirmPayment)
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
            onConfirmPayment();
        }

        protected override void ProcessPayment(Action onComplete)
        {
            Status = "Проверка платежа через Card...";
            Thread.Sleep(5000);
            onComplete();
        }

        protected override void ShowResult(PaymentHandler paymentHandler)
        {
            paymentHandler.ShowPaymentResult(this);
        }
    }

    internal class WebMoneyPaymentService : PaymentService, IPaymentService
    {
        public string SystemId => "WebMoney";
        public string Status { get; private set; }
        
        protected override void ShowPaymentInterface(Action onConfirmPayment)
        {
            Console.WriteLine("Вызов API WebMoney...");
            onConfirmPayment();
        }

        protected override void ProcessPayment(Action onComplete)
        {
            Status = "Проверка платежа через Web Money...";
            Thread.Sleep(5000);
            onComplete();
        }

        protected override void ShowResult(PaymentHandler paymentHandler)
        {
            paymentHandler.ShowPaymentResult(this);
        }
    }

    internal class QiwiPaymentService : PaymentService, IPaymentService
    {
        public string SystemId => "QIWI";
        public string Status { get; private set; }

        protected override void ShowPaymentInterface(Action onConfirmPayment)
        {
            Console.WriteLine("еревод на страницу QIWI...");
            onConfirmPayment();
        }

        protected override void ProcessPayment(Action onComplete)
        {
            Status = "Проверка платежа через QIWI...";
            Thread.Sleep(5000);
            onComplete();
        }

        protected override void ShowResult(PaymentHandler paymentHandler)
        {
            paymentHandler.ShowPaymentResult(this);
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
        public void ShowPaymentResult(IPaymentService service)
        {
            Console.WriteLine($"Вы оплатили с помощью {service.SystemId}");

            Console.WriteLine(service.Status);
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
       void BeginPayment(PaymentHandler paymentHandler);
    }
}