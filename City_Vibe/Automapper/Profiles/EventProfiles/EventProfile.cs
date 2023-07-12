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

            CreateMap<EditEventViewModel, Event>()
                .ForMember(x => x.Image, opt => opt.MapFrom(src => src.URL))
                .ForMember(x => x.Desciption, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.Data, opt => opt.MapFrom(src => src.CreatedDate));


            CreateMap<Event, EditEventViewModel>()
                .ForMember(x => x.URL, opt => opt.MapFrom(src => src.Image))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Desciption))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.Data))
                .ForMember(x => x.Image, opt => opt.Ignore());
        }

    }
}
