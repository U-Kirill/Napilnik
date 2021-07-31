using System;

namespace IMJunior
{
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
}