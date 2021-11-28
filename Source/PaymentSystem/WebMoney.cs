using System;
using Source.Builder;
using Source.Builder.Hash;

namespace Source.PaymentSystem
{
    public class WebMoney : IPaymentSystem
    {
        private const string RootUrl = "order.system2.ru";
        private const string OrderKeyword = "pay";

        public string GetPayingLink(Order order)
        {
            order = order ?? throw new ArgumentNullException(nameof(order));
            return GetLink(order);
        }

        private string GetLink(Order order) =>
            PaylinkBuilder.Create(RootUrl)
                .AddOrderKeyword(OrderKeyword)
                .AddHash(new FakeMd5Hash(order.Id))
                .AddHash(new IdentityHash(order.Amount.ToString()))
                .Build();
    }
}