using System;

namespace IMJunior
{
    public class OrderForm
    {
        private readonly PaymentServiceFactory _paymentServiceFactory;

        public OrderForm(PaymentServiceFactory paymentServiceFactory) => 
            _paymentServiceFactory = paymentServiceFactory ?? throw new ArgumentNullException(nameof(paymentServiceFactory));

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
            
            foreach (string serviceId in _paymentServiceFactory.GetAllServiceIDs()) 
                Console.WriteLine(serviceId);
        }
    }
}