using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAPI.Domain.Models
{
    public class PaymentResponse
    {
        public string PaymentRef { get; set; }
        public string ResponseCode { get; set; }
        public string Description { get; set; }
    }
}
