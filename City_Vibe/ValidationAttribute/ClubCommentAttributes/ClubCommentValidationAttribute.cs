using City_Vibe.Infrastructure.ExtensionMethod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace City_Vibe.ValidationAttribute.ClubCommentAttributes
{

    public class ClubCommentValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }
  
            if (!filterContext.ModelState.IsValid)
            {
                filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);          
            }

            

            var userId = filterContext.HttpContext.User.GetUserId();
            if (userId == null)
            {
                filterContext.Result = new NotFoundResult();
            }

        }
    }    
}
