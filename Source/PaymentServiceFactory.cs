using System;
using System.Collections.Generic;

namespace IMJunior
{
    public class PaymentServiceFactory
    {
        private readonly PaymentHandler _paymentHandler;

        public PaymentServiceFactory(PaymentHandler paymentHandler)
        {
            _paymentHandler = paymentHandler;
        }

        public IPaymentService Create(string serviceId)
        {
            serviceId = serviceId.ToLowerInvariant();

            if (IsSuitableId(serviceId, PaymentServiceType.Qiwi))
                return new QiwiPaymentService(_paymentHandler, serviceId);

            if (IsSuitableId(serviceId, PaymentServiceType.Card))
                return new CardPaymentService(_paymentHandler, serviceId);

            if (IsSuitableId(serviceId, PaymentServiceType.WebMoney))
                return new WebMoneyPaymentService(_paymentHandler, serviceId);

            throw new ArgumentOutOfRangeException(nameof(serviceId));
        }

        public IEnumerable<string> GetAllServiceIDs()
        {
            foreach (PaymentServiceType type in Enum.GetValues<PaymentServiceType>())
                yield return GetServiceId(type);
        }

        private bool IsSuitableId(string serviceId, PaymentServiceType paymentServiceType) =>
            serviceId == GetServiceId(paymentServiceType);

        private string GetServiceId(PaymentServiceType paymentServiceType) =>
            Enum.GetName(paymentServiceType).ToLowerInvariant();
    }
}