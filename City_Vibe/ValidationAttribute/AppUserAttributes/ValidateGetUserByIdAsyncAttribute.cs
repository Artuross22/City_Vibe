using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AppUserController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace City_Vibe.ValidationAttribute.AppUserAttributes
{
    public class ValidateGetUserByIdAsyncAttribute : Attribute, IAsyncActionFilter
    {
    
        private readonly UserManager<AppUser> userManager;

        public ValidateGetUserByIdAsyncAttribute(UserManager<AppUser> _userManager) => userManager = _userManager;

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            var curUser = new AppUserClaimsViewModel();

            if (filterContext.ActionArguments.ContainsKey("userClaimsViewModel"))
            {
               curUser = (AppUserClaimsViewModel)filterContext.ActionArguments["userClaimsViewModel"];
            }
            else
            {
                filterContext.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }
            
            var user = await userManager.FindByIdAsync(curUser.UserId);
           
            if (user == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }

            await next();

            Debug.WriteLine("After Action Execution");
        }
    }
    
}
