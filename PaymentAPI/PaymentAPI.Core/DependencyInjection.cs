
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PaymentAPI.Core.Interfaces.Gateways;
using PaymentAPI.Core.Services;
using PaymentAPI.Domain.Interfaces;
using System.Reflection;
using System;
using Polly.Extensions.Http;
using Polly.Timeout;
using System.Net;
using Polly;

namespace PaymentAPI.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
