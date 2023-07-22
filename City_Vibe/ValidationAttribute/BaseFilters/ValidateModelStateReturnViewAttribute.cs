using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace City_Vibe.ValidationAttribute.BaseFilters
{
    public class ValidateModelStateReturnViewAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var result = filterContext.ActionArguments.FirstOrDefault().Value;

            if (!filterContext.ModelState.IsValid)
            {
                if (filterContext.Controller is Controller controller)
                {
                    filterContext.Result = controller.View(result);
                    return;
                }
            }
        }
    }
}
