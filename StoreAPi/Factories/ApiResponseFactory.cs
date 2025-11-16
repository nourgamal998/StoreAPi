using Microsoft.AspNetCore.Mvc;

namespace StoreAPi.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(e => e.Value.Errors.Any())
                                           .Select(m => new Shared.ErrorModels.ValidationError()
                                           {
                                               Field = m.Key,
                                               Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                                           });
            var response = new Shared.ErrorModels.ValidationErrorToReturn()
            {
                ValidationErrors = errors
            };
            return new BadRequestObjectResult(response);

        }

    }
}
