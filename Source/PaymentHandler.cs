using System;

namespace IMJunior
{
    public class PaymentHandler
    {
        public void ShowPaymentResult(IPaymentService service)
        {
            Console.WriteLine("Оплата прошла успешно!");
        }

        public void ShowStatus(IPaymentService service)
        {
            Console.WriteLine(service.Status);
        }
    }
}