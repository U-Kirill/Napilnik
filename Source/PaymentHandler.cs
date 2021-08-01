using System;

namespace IMJunior
{
    public class PaymentHandler
    {
        private IPaymentResultReasonVisitor _paymentErrorHandler = new PaymentErrorHandler();

        public void ShowPaymentResult(IPaymentService service)
        {
            ShowCommonInfo(service);
            ShowPaymentSummary(service);
        }

        private void ShowCommonInfo(IPaymentService service)
        {
            Console.WriteLine($"Вы оплатили с помощью {service.SystemId}");
            Console.WriteLine($"Проверка платежа через {service.SystemId}...");
        }

        private void ShowPaymentSummary(IPaymentService service)
        {
            Result<IPaymentResultReason> result = service.IsCorrectPayment();

            if (result)
                Console.WriteLine("Оплата прошла успешно!");
            else
                HandleError(result);
        }

        private void HandleError(Result<IPaymentResultReason> result) =>
            result.Reson.Accept(_paymentErrorHandler);
    }

    public class PaymentErrorHandler : IPaymentResultReasonVisitor
    {
        public void Visit(IPaymentResultReason visit)
        {
            throw new NotImplementedException();
        }
    }
}