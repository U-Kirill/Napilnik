using System;
using Source.Builder;
using Source.Builder.Hash;
using Source.Builder.Info;

namespace Source.PaymentSystem
{
    public class PayPal : IPaymentSystem
    {
        private const string RootUrl = "system3.com";
        private const string OrderKeyword = "pay";

        private readonly string _secretKey;

        public PayPal(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GetPayingLink(Order order)
        {
            order = order ?? throw new ArgumentNullException(nameof(order));
            return GetLink(order);
        }

        private string GetLink(Order order) =>
            PaylinkBuilder.Create(RootUrl)
                .AddOrderKeyword(OrderKeyword)
                .AddInfo(new AmountInfo(order.Amount))
                .AddInfo(new CurrencyInfo("RUB"))
                .AddHash(new FakeSha1Hash(order.Amount))
                .AddHash(new IdentityHash(order.Id.ToString()))
                .AddHash(new IdentityHash(_secretKey))
                .Build();
    }
}