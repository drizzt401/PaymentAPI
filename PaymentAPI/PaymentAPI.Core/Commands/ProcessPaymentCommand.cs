using MediatR;
using PaymentAPI.Domain.ModelView;
using System;

namespace PaymentAPI.Core.Commands
{
    public class ProcessPaymentCommand : IRequest<PaymentResponseMV>
    {

        public string CreditCardNumber { get; set; }

        public string CardHolder { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string SecurityCode { get; set; }

        public decimal Amount { get; set; }

    }
}
