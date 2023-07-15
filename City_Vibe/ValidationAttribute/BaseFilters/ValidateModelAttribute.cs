using City_Vibe.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace City_Vibe.ValidationAttribute.BaseFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var sendError = new ErrorResponse();
                sendError.StatusCode = 400;
                sendError.StatusPhrase = "Bad Request";
                sendError.Timestamp = DateTime.Now;
                var errors = context.ModelState.AsEnumerable();

                foreach (var error in errors)
                {
                    foreach (var inner in error.Value.Errors)
                    {
                        sendError.Errors.Add(inner.ErrorMessage);
                    }
                }

               context.Result = new BadRequestObjectResult(sendError);
            }
        }
    }
}
