using City_Vibe.Infrastructure.ExtensionMethod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace City_Vibe.ValidationAttribute.BaseFilters
{
    public class ValidateGetUserIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var userId = filterContext.HttpContext.User.GetUserId();
            if (userId == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}
