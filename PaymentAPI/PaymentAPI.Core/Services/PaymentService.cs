using AutoMapper;
using PaymentAPI.Core.Interfaces.Gateways;
using PaymentAPI.Domain.Models;
using Polly;
using Polly.Fallback;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace PaymentAPI.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICheapPaymentGateway cheapPaymentGateway;
        private readonly IExpensivePaymentGateway expensivePaymentGateway;
        private readonly IPremiumPaymentService premiumPaymentService;
        private readonly IMapper mapper;
        private FallbackPolicy<PaymentResponse> fallbackpolicy;
        private RetryPolicy retryPolicy;

        public PaymentService(ICheapPaymentGateway cheapPaymentGateway, IExpensivePaymentGateway expensivePaymentGateway,
            IPremiumPaymentService premiumPaymentService,IMapper mapper)
        {
            this.cheapPaymentGateway = cheapPaymentGateway;
            this.expensivePaymentGateway = expensivePaymentGateway;
            this.premiumPaymentService = premiumPaymentService;
            this.mapper = mapper;
        }

        public PaymentResponse ProcessPayment(Payment payment)
        {
            var res = new PaymentResponse();

            this.retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetry(3, times => TimeSpan.FromSeconds(1));

            this.fallbackpolicy = Policy<PaymentResponse>
            .Handle<HttpRequestException>()
            .Fallback<PaymentResponse>((cancellationToken) => cheapPaymentGateway.PostPayment(mapper.Map<CheapPaymentRequest>(payment)));

            //2. Validate Amount, Map to Payment  - Automapper and Call payment service 

            if (payment.Amount <= 20)
                res = cheapPaymentGateway.PostPayment(mapper.Map<CheapPaymentRequest>(payment));

            else if (payment.Amount > 20 && payment.Amount <= 500)
                res = fallbackpolicy.Execute(() => expensivePaymentGateway.PostPayment(mapper.Map<ExpensivePaymentRequest>(payment)));


            else res = retryPolicy.Execute(() => premiumPaymentService.PostPayment(mapper.Map<PremiumPaymentRequest>(payment)));

            return res;
        }
    }
}
