using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.EventController;

namespace City_Vibe.Automapper.Profiles.EventProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDetailViewModel>();
            CreateMap<CreateEventViewModel, Event>();
        }

    }
}
