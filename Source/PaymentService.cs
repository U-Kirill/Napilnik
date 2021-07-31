using System;
using System.Data;

namespace IMJunior
{
    public abstract class PaymentService

    {
    private PaymentHandler _paymentHandler;

    public void BeginPayment(PaymentHandler paymentHandler)
    {
        _paymentHandler = paymentHandler;
        ShowPaymentInterface(OnConfirmPayment);
    }

    private void OnConfirmPayment()
    {
        ShowStatus(_paymentHandler);
        ProcessPayment(() => ShowResult(_paymentHandler));
        ShowStatus(_paymentHandler);
    }

    protected abstract void ShowPaymentInterface(Action onPaymentConfirmed);

    protected abstract void ProcessPayment(Action onCompleted);

    protected abstract void ShowStatus(PaymentHandler paymentHandler);

    protected abstract void ShowResult(PaymentHandler paymentHandler);
    }
}