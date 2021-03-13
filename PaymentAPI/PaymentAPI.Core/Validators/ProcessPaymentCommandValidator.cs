using FluentValidation;
using PaymentAPI.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;


namespace PaymentAPI.Core.Validators
{
    public class ProcessPaymentCommandValidator : AbstractValidator<ProcessPaymentCommand>
    {
        public ProcessPaymentCommandValidator()
        {
            RuleFor(v => v.CreditCardNumber).NotNull().CreditCard();
            RuleFor(v => v.SecurityCode).NotNull().MinimumLength(3).MaximumLength(3);
            RuleFor(v => v.Amount).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(v => v.CardHolder).Length(100);
        }
    }
}
