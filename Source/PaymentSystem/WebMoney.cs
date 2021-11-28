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
                .WithOrderKeyword(OrderKeyword)
                .WithHash(new FakeMd5Hash(order.Id))
                .WithHash(new IdentityHash(order.Amount.ToString()))
                .Build();
    }
}