using System;
using Api.ViewModels;
using FluentValidation;

namespace Api.Validators
{
    public class AuthenticateValidator : AbstractValidator<AuthenticateVM>
    {
        public AuthenticateValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(a => a.Password)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(6);
        }
    }
}
