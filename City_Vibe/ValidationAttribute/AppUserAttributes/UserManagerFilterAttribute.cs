using City_Vibe.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace City_Vibe.ValidationAttribute.AppUserAttributes
{
    public class UserManagerFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly UserManager<AppUser> userManager;

        public UserManagerFilterAttribute(UserManager<AppUser> _userManager)
        {
            userManager = _userManager;          
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = await userManager.GetUserAsync(context.HttpContext.User);

            if (user == null)
            {
                context.Result = new NotFoundResult();
                return;
            }

            await next();

            Debug.WriteLine("After Action Execution");
        }
    }
}

