using Domain.Models;
using Dto.ViewModels;
using FluentValidation;

namespace RequestApp.Validators
{
    public class RequestFormValidator:AbstractValidator<RequestFormViewModel>
    {
        public RequestFormValidator()
        {
            RuleFor(model => model).NotNull().WithMessage("Invalid model");
            RuleFor(model => model.Body).NotEmpty().WithMessage("Body shouldn't be empty").MaximumLength(100).WithMessage("Body length must be less than 100");
            RuleFor(model => model.Subject).NotEmpty().WithMessage("Subject shouldn't be empty").MaximumLength(100).WithMessage("Subject length must be less than 100");
            RuleFor(model => model.RequestTypeId).GreaterThan(0).WithMessage("Must choose Request Type");
        }
    }
}
