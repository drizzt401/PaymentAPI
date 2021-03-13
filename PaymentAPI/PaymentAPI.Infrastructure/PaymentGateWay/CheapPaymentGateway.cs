
using Newtonsoft.Json;
using PaymentAPI.Core.Interfaces.Gateways;
using PaymentAPI.Domain.Models;
using System;
using System.Net.Http;
using System.Text;

namespace PaymentAPI.Infrastructure.PaymentGateWay
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        private readonly HttpClient client;

        public CheapPaymentGateway(HttpClient client)
        {
            this.client = client;
        }
        public PaymentResponse PostPayment(CheapPaymentRequest request)
        {
            try
            {
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                var response = client.PostAsync("https://unity1.unitybankng.com/PaymentGatewayTest/api/Payment/CheapPaymentGateWay", content).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<PaymentResponse>(responseContent);
                return res;
            }
            catch(HttpRequestException)
            {
                throw;
            }
        }
    }
}