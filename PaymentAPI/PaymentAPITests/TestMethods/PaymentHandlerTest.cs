using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PaymentAPI.Controllers;
using PaymentAPI.Core.Commands;
using PaymentAPI.Core.Handlers;
using PaymentAPI.Core.Services;
using PaymentAPI.Domain.Interfaces;
using PaymentAPI.Domain.Models;
using PaymentAPI.Domain.ModelView;
using PaymentAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentAPITests.TestMethods
{
    public class PaymentHandlerTest
    {
        private ProcessPaymentCommand payment;
        private Mock<IMediator> mediator;
        private Mock<IMapper> mapper;
        private Mock<IPaymentService> paymentService;
        private Mock<IUnitOfWork<PaymentAPI.Domain.Entities.Payment>> paymentRepository;
        [SetUp]
        public void Setup()
        {
            mediator = new Mock<IMediator>();
            mapper = new Mock<IMapper>();
            paymentService = new Mock<IPaymentService>();
            paymentRepository = new Mock<IUnitOfWork<PaymentAPI.Domain.Entities.Payment>>();
        }

        [Test]
        public async Task Can_Post_Payment()
        {
            //Arrange
            mediator.Setup(m => m.Send(It.IsAny<ProcessPaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PaymentResponseMV());
            paymentService.Setup(ps => ps.ProcessPayment(It.IsAny<Payment>())).Returns(new PaymentResponse() {ResponseCode = "00", Description ="Successful" });
            mapper.Setup(map => map.Map<PaymentAPI.Domain.Entities.Payment>(It.IsAny<ProcessPaymentCommand>())).Returns(new PaymentAPI.Domain.Entities.Payment());
            paymentRepository.Setup(pr => pr.Repository.Add(It.IsAny<PaymentAPI.Domain.Entities.Payment>())).Returns(Task.FromResult(Task.CompletedTask));
            payment = new ProcessPaymentCommand() { Amount = 20, CardHolder = "Faith Udoh", CreditCardNumber = "5500 0000 0000 0004", ExpirationDate = DateTime.Now, SecurityCode = "345" };
            ProcessPaymentHandler handler = new ProcessPaymentHandler(paymentService.Object, mapper.Object, paymentRepository.Object);
            
            //Act            
            var response = await handler.Handle(payment, new CancellationToken());

            //Assert
            Assert.AreEqual("00", response.ResponseCode);
        }

    }
}
