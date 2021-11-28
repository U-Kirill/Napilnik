using System;
using System.Collections.Generic;
using Source.PaymentSystem;

namespace Source
{
  public class Program
  {
    static void Main(string[] args)
    {
      string secretKey = "123456789";
      Order order = new Order(1, 12000);

      List<IPaymentSystem> paymentSystems = new List<IPaymentSystem>()
      {
        new Qiwi(),
        new WebMoney(),
        new PayPal(secretKey),
      };

      paymentSystems.ForEach(x => Console.WriteLine(x.GetPayingLink(order)));

      Console.ReadLine();
    }
  }
}

// Выведите платёжные ссылки для трёх разных систем платежа: 
// pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
// order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
// system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}