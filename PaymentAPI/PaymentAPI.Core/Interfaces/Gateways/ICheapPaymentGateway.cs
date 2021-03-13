using System;
using PaymentAPI.Domain.Models;

namespace PaymentAPI.Core.Interfaces.Gateways
{
    public interface ICheapPaymentGateway
    {
        PaymentResponse PostPayment(CheapPaymentRequest request);
    }
}
