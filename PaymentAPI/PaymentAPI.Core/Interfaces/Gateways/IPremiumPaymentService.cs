using PaymentAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAPI.Core.Interfaces.Gateways
{
    public interface IPremiumPaymentService
    {
        PaymentResponse PostPayment(PremiumPaymentRequest request);
    }
}
