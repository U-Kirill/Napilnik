using System;
using Source.Builder;
using Source.Builder.Hash;
using Source.Builder.Info;

namespace Source.PaymentSystem
{
    public class Qiwi : IPaymentSystem
    {
        private const string RootUrl = "pay.system1.ru";
        private const string OrderKeyword = "order";

        public string GetPayingLink(Order order)
        {
            order = order ?? throw new ArgumentNullException(nameof(order));
            return GetLink(order);
        }

        private string GetLink(Order order) =>
            PaylinkBuilder.Create(RootUrl)
                .WithOrderKeyword(OrderKeyword)
                .WithInfo(new CombinedAmountCurrencyInfo(order.Amount, "RUB"))
                .WithHash(new FakeMd5Hash(order.Id))
                .Build();
    }
}