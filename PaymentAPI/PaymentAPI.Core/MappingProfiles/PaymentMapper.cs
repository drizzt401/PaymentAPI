using AutoMapper;
using PaymentAPI.Core.Commands;
using PaymentAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAPI.Core.MappingProfiles
{
    public class PaymentMapper : Profile
    {
        public PaymentMapper()
        {
            CreateMap<ProcessPaymentCommand, Payment>();
            CreateMap<CheapPaymentRequest, Payment>().ReverseMap();
            CreateMap<ExpensivePaymentRequest, Payment>().ReverseMap();
            CreateMap<ProcessPaymentCommand, Domain.Entities.Payment>();
            CreateMap<PremiumPaymentRequest, Payment>().ReverseMap();
        }

    }
}
