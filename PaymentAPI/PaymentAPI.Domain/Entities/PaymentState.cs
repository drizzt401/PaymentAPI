using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentAPI.Domain.Entities
{
    public class PaymentState : BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar(30)")]
        public string State { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
