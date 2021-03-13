using System;
using System.Threading.Tasks;
using PaymentAPI.Domain.Models;

namespace PaymentAPI.Core.Interfaces.Gateways
{
    public interface IExpensivePaymentGateway
    {
        PaymentResponse PostPayment(ExpensivePaymentRequest request);
    }
}
