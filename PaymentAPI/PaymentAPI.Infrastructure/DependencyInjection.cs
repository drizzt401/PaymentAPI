using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentAPI.Core.Interfaces.Gateways;
using PaymentAPI.Domain.Interfaces;
using PaymentAPI.Domain.Models;
using PaymentAPI.Infrastructure.Data.EntityFramework;
using PaymentAPI.Infrastructure.Data.Repositories;
using PaymentAPI.Infrastructure.PaymentGateWay;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace PaymentAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
 
                 .AddJsonFile("appsettings.json", optional: false)
                 .AddJsonFile($"appsettings.{envName}.json", optional: false)
                 .Build();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConn"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddHttpClient<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddHttpClient<IExpensivePaymentGateway, ExpensivePaymentGateway>();

            services.AddHttpClient<IPremiumPaymentService, PremiumPaymentService>();
            return services;
        }
    }
}
