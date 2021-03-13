using AutoMapper;
using MediatR;
using PaymentAPI.Core.Commands;
using PaymentAPI.Core.Services;
using PaymentAPI.Domain.Entities;
using PaymentAPI.Domain.Interfaces;
using PaymentAPI.Domain.Models;
using PaymentAPI.Domain.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentAPI.Core.Handlers
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, PaymentResponseMV>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Domain.Entities.Payment> _unitOfWork;
        public ProcessPaymentHandler(IPaymentService paymentService, IMapper mapper, IUnitOfWork<Domain.Entities.Payment> unitOfWork)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaymentResponseMV> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var PaymentRef = "";
                var Status = "";
                var Description = "";

                //1. Validate Request - Using FluentAPI

                var res =_paymentService.ProcessPayment(_mapper.Map<Domain.Models.Payment>(request));

                //5. Map to DB entity - Automapper
                var paymentEntity =  _mapper.Map<Domain.Entities.Payment>(request);

                //7. set response;
                switch (res.ResponseCode)
                {
                    case "00": // successful
                        paymentEntity.TransactionRef = res.PaymentRef;
                        paymentEntity.PaymentStateId = 1;
                        Status = "00";
                        Description = "Payment is Processed";
                        break;

                    case "99": //failed
                        paymentEntity.TransactionRef = res.PaymentRef;
                        paymentEntity.PaymentStateId = 2;
                        Status = "99";
                        Description = "Failed to process payment";
                        break;
                }

                //6. Save to db using unit work
                await _unitOfWork.Repository.Add(paymentEntity);
                await _unitOfWork.Commit();
                var currentPayment = _unitOfWork.Repository.Include(p => p.paymentStates).Where(p => p.TransactionRef == res.PaymentRef).ToList();
                string paymentState = "";
                foreach(Domain.Entities.Payment p in currentPayment)
                {
                    paymentState = p.paymentStates.State;
                }
                return new PaymentResponseMV {PaymentReference = res.PaymentRef, ResponseCode = Status , ResponseDescription = Description,  PaymentState = paymentState};
            }
            catch
            {
                throw;
            }
        }
    }
}
