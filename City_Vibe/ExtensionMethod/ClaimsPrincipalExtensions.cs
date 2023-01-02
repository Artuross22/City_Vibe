using System.Security.Claims;

namespace City_Vibe.ExtensionMethod
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
            //  return string.Empty;
        }
    }
}
