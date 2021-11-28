using System;
using System.Collections.Generic;

namespace Source
{

  class Program
  {
    static void Main(string[] args)
    {
      string secretKey = "123456789";
      Order order = new Order(1, 12000);
      IHashGenerator hashGenerator = new FakeHashGenerator();


      List<IPaymentSystem> paymentSystems = new List<IPaymentSystem>()
      {
        new Qiwi(),
        new WebMoney(),
        new PayPal(secretKey),
      };


      paymentSystems.ForEach(x => Console.WriteLine(x.GetPayingLink(order)));
      Console.ReadLine();

      //Выведите платёжные ссылки для трёх разных систем платежа: 
      //pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
      //order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
      //system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}
    }
  }

  public class Order
  {
    public readonly int Id;
    public readonly int Amount;

    public Order(int id, int amount) => (Id, Amount) = (id, amount);
  }

  public interface IPaymentSystem
  {
    public string GetPayingLink(Order order);
  }

  public class Qiwi : IPaymentSystem
  {
    private const string RootUrl = "pay.system1.ru";
    private const string OrderKeyword = "order";

    public string GetPayingLink(Order order) =>
      PaylinkBuilder.Create(RootUrl)
        .AddOrderKeyword(OrderKeyword)
        .AddInfo(new CombinedAmountCurrencyInfo(order.Amount, "RUB"))
        .AddHash(new FakeMd5Hash(order.Id))
        .Build();
  }

  public class WebMoney : IPaymentSystem
  {
    private const string RootUrl = "order.system2.ru";
    private const string OrderKeyword = "pay";

    public string GetPayingLink(Order order) =>
      PaylinkBuilder.Create(RootUrl)
        .AddOrderKeyword(OrderKeyword)
        .AddHash(new FakeMd5Hash(order.Id))
        .AddHash(new IdentityHash(order.Amount.ToString()))
        .Build();
  }

  public class PayPal : IPaymentSystem
  {
    private const string RootUrl = "system3.com";
    private const string OrderKeyword = "pay";
    
    private string _secretKey;

    public PayPal(string secretKey)
    {
      _secretKey = secretKey;
    }

    public string GetPayingLink(Order order) =>
      PaylinkBuilder.Create(RootUrl)
        .AddOrderKeyword(OrderKeyword)
        .AddInfo(new AmountInfo(order.Amount))
        .AddInfo(new CurrencyInfo("RUB"))
        .AddHash(new FakeSha1Hash(order.Amount))
        .AddHash(new IdentityHash(order.Id.ToString()))
        .AddHash(new IdentityHash(_secretKey))
        .Build();
  }

  public class FakeSha1Hash : IHashProvider
  {
    private readonly int _originalValue;

    public FakeSha1Hash(int originalValue)
    {
      _originalValue = originalValue;
    }

    public string GetHash() => "0a4d55a8d778e5022fab701977c5d840bbc486d0"; //_originalValueHash
  }

  public class CurrencyInfo : IInfoProvider
  {
    private readonly string _currency;

    public CurrencyInfo(string currency)
    {
      _currency = currency;
    }

    public string Info => $"currency={_currency}";
  }

  public class AmountInfo : IInfoProvider
  {
    private readonly int _amount;

    public AmountInfo(int amount)
    {
      _amount = amount;
    }

    public string Info => $"amount={_amount}";
  }


  public class IdentityHash : IHashProvider
  {
    private readonly string _hash;

    public IdentityHash(string hash)
    {
      _hash = hash;
    }

    public string GetHash() => _hash;
  }

  public class CombinedHash : IHashProvider
  {
    private List<IHashProvider> _providers;

    public CombinedHash(params IHashProvider[] providers)
    {
      _providers = new List<IHashProvider>(providers);
    }

    public string GetHash()
    {
      var hash = String.Empty;

      foreach (IHashProvider provider in _providers)
        hash += provider.GetHash() + "+";

      return hash.Trim('+');
    }
  }

  public class FakeMd5Hash : IHashProvider
  {
    private readonly int _originalValue;

    public FakeMd5Hash(int originalValue)
    {
      _originalValue = originalValue;
    }

    public string GetHash()
    {
      return "b10a8db164e0754105b7a99be72e3fe5"; //_originalValueHash
    }
  }

  public class CombinedAmountCurrencyInfo : IInfoProvider
  {
    public CombinedAmountCurrencyInfo(int amount, string currency)
    {
      Info = $"amount={amount}{currency}";
    }

    public string Info { get; }
  }

  public interface IHashGenerator
  {
    string GenerateMd5(string value);

    string GenerateSha1(string value);
  }

  public class FakeHashGenerator : IHashGenerator
  {
    private const string ExampleMd5Hash = "b10a8db164e0754105b7a99be72e3fe5";
    private const string ExampleSha1Hash = "0a4d55a8d778e5022fab701977c5d840bbc486d0";

    public string GenerateMd5(string value)
    {
      return ExampleMd5Hash;
    }

    public string GenerateSha1(string value)
    {
      return ExampleSha1Hash;
    }
  }
}