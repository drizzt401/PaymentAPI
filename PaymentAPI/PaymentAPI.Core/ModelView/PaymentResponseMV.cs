using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAPI.Domain.ModelView
{
    public class PaymentResponseMV
    {
        public string PaymentReference { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }

        public string PaymentState { get; set; }
    }
}
