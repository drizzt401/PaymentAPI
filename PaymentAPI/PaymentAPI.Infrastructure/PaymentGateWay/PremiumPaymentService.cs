using Newtonsoft.Json;
using PaymentAPI.Core.Interfaces.Gateways;
using PaymentAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PaymentAPI.Infrastructure.PaymentGateWay
{
    class PremiumPaymentService : IPremiumPaymentService
    {
        private readonly HttpClient client;
        Random Random = new Random();
        public PremiumPaymentService(HttpClient client)
        {
            this.client = client;
        }
        public PaymentResponse PostPayment(PremiumPaymentRequest request)
        {
            try
            {

                if (Random.Next(1, 3) == 1)
                throw new HttpRequestException("This is a simulated exception");
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("https://unity1.unitybankng.com/PaymentGatewayTest/api/Payment/PremiumPaymentService", content).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<PaymentResponse>(responseContent);
                return res;
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }
    }
}
