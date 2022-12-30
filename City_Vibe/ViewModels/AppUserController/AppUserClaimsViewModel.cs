namespace City_Vibe.ViewModels.AppUserController
{
   
        public class AppUserClaimsViewModel
        {
            public AppUserClaimsViewModel()
            {
                Claims = new List<UserClaim>();
            }
            public string UserId { get; set; }
            public List<UserClaim> Claims { get; set; }
        }

        public class UserClaim
        {
            public string ClaimType { get; set; }
            public bool IsSelected { get; set; }
        }
    
}
