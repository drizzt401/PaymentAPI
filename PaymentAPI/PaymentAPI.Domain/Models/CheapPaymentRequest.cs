using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentAPI.Domain.Models
{
    public class CheapPaymentRequest
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
    }
}
