using System.Collections.Generic;
using System.Linq;

namespace IMJunior
{
    public class PaymentServiceStorage
    {
        private readonly Dictionary<string, IPaymentService> _paymentServices;

        public PaymentServiceStorage(params IPaymentService[] paymentServices)
        {
            _paymentServices = paymentServices
                .ToDictionary(x => x.SystemId, x => x);
        }

        public IEnumerable<IPaymentService> All => _paymentServices.Values;

        public IPaymentService Get(string forId) =>
            _paymentServices[forId];
    }
}