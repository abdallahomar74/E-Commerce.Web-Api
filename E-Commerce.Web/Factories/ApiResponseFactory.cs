using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenrateApiValidationErrorsResponse(ActionContext Context) 
        {
            var Errors = Context.ModelState.Where(m => m.Value.Errors.Any())
                    .Select(m => new ValidationError()
                    {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(E => E.ErrorMessage)
                    });
            var Response = new ValidationErrorToReturn()
            {
                Errors = Errors
            };
            return new BadRequestObjectResult(Response);
        }
    }
}
