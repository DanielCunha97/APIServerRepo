using APIServer.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Persistence
{
    public class ShareEmailValidator : AbstractValidator<EmailModel> // FluentValidation know that this validation is for the EmailModel class.
    {
        public ShareEmailValidator()
        {
            RuleFor(model => model.EmailToName).NotEmpty().NotNull().WithMessage("The email must have a sender!");
            RuleFor(model => model.EmailToName).Length(5, 20);

            RuleFor(model => model.EmailBody).NotEmpty().NotNull().WithMessage("Empty Email");
        }
    }
}
