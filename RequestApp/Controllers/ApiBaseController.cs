using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Dto.ViewModels;
namespace RequestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        public ApiBaseController( )
        {
        }
        public bool Validate<T>(T dto, IValidator<T> validator)
        {
            List<FluentErrorMessage> results = new();

            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                    results.Add(new FluentErrorMessage() { PropertyName = failure.PropertyName, ErrorMessage = failure.ErrorMessage });

                if (results.Count > 0)
                    throw new BusinessException(Newtonsoft.Json.JsonConvert.SerializeObject(results));
            }
            return true;
        }
    }
}
