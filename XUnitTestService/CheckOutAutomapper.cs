using AutoMapper;
using City_Vibe.Automapper.Profiles.AppointmentProfile;
using City_Vibe.Automapper.Profiles.AppUserProfile;
using City_Vibe.Automapper.Profiles.CategoryProfile;
using City_Vibe.Automapper.Profiles.ClubCommentProfiles;
using City_Vibe.Automapper.Profiles.ClubProfiles;
using City_Vibe.Automapper.Profiles.EventProfiles;
using City_Vibe.Automapper.Profiles.UserProfiles;

namespace XUnitTestService
{
    public class CheckOutAutomapper
    {
        [Fact]
        public void MappingConfigurationTests()
        {
           var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AppointmentProfile>();
                cfg.AddProfile<AppUserProfile>();
                cfg.AddProfile<CategoryProfile>();
                cfg.AddProfile<ClubCommentProfile>();
                cfg.AddProfile<ClubProfile>();
                cfg.AddProfile<EventProfile>();
                cfg.AddProfile<UserProfile>();              
            });

            config.AssertConfigurationIsValid();

        }
    }
}