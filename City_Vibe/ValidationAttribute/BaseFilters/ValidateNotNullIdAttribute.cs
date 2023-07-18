using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace City_Vibe.ValidationAttribute.BaseFilters
{
    public class ValidateNotNullIdAttribute : ActionFilterAttribute
    {
        public string nameId;
        public ValidateNotNullIdAttribute(string _nameId) => nameId = _nameId;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int? id;
            var checkItem = context.ActionArguments.Values.ToList();
            
            if (context.ActionArguments.ContainsKey($"{nameId}") && checkItem[0] != null)
            {
                id = (int)context.ActionArguments[$"{nameId}"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }

            if (id == null)
            {
                context.Result = new NotFoundResult();
                return;
            }
        }

    }
}
