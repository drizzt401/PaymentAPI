using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentAPI.Domain.Entities
{
    public class Payment : BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar(16)")]
        public string CreditCardNumber { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string CardHolder { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string TransactionRef { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public string SecurityCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required, ForeignKey("Id")]
        public int PaymentStateId { get; set; }

        public PaymentState paymentStates { get; set; }

    }
}
