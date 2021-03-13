using PaymentAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAPI.Core.Services
{
    public interface IPaymentService
    {
        PaymentResponse ProcessPayment(Payment payment);
    }
}
