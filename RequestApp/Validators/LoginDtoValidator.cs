using Dto;
using FluentValidation;

namespace RequestApp.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(model => model).NotNull().WithMessage("Invalid model");
            RuleFor(model => model.Email).NotEmpty().WithMessage("Email shouldn't be empty");
            RuleFor(model => model.Password).NotEmpty().WithMessage("Password shouldn't be empty");
        }
    }
}
