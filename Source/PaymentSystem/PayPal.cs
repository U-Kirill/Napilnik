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
                .WithOrderKeyword(OrderKeyword)
                .WithInfo(new AmountInfo(order.Amount))
                .WithInfo(new CurrencyInfo("RUB"))
                .WithHash(new FakeSha1Hash(order.Amount))
                .WithHash(new IdentityHash(order.Id.ToString()))
                .WithHash(new IdentityHash(_secretKey))
                .Build();
    }
}