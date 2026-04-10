using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MeuPlantao.Communication.Dto.Responses;

namespace MeuPlantao.Filters
{

    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response = ServiceResponse<object>.ValidationError(errors);

                context.Result = new BadRequestObjectResult(response.Errors);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}