using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.EventController;

namespace City_Vibe.Automapper.Profiles.EventProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDetailViewModel>()
                 .ForMember(x => x.ProfileImageUrl, opt => opt.Ignore())
                     .ForMember(x => x.SaveEventId, opt => opt.Ignore())
                         .ForMember(x => x.SaveEvents, opt => opt.Ignore())
                                 .ForMember(x => x.ReplyAppointments, opt => opt.Ignore())
                                      .ForMember(x => x.Statement, opt => opt.Ignore())
                                           .ForMember(x => x.CheckAppointment, opt => opt.Ignore());


            CreateMap<CreateEventViewModel, Event>()
                  .ForMember(x => x.Id, opt => opt.Ignore())
                     .ForMember(x => x.Data, opt => opt.Ignore())
                         .ForMember(x => x.City, opt => opt.Ignore())
                                 .ForMember(x => x.Category, opt => opt.Ignore())
                                      .ForMember(x => x.AppUser, opt => opt.Ignore())
                                           .ForMember(x => x.RepliesId, opt => opt.Ignore())
                                           .ForMember(x => x.Replies, opt => opt.Ignore())
                                            .ForMember(x => x.Comments, opt => opt.Ignore())
                                             .ForMember(x => x.Club, opt => opt.Ignore())
                                              .ForMember(x => x.Appointments, opt => opt.Ignore());



            CreateMap<EditEventViewModel, Event>()
                .ForMember(x => x.Image, opt => opt.MapFrom(src => src.URL))
                .ForMember(x => x.Desciption, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.Data, opt => opt.MapFrom(src => src.CreatedDate))
                 .ForMember(x => x.City, opt => opt.Ignore())
                     .ForMember(x => x.AppUser, opt => opt.Ignore())
                         .ForMember(x => x.RepliesId, opt => opt.Ignore())
                         .ForMember(x => x.Replies, opt => opt.Ignore())
                            .ForMember(x => x.Comments, opt => opt.Ignore())
                               .ForMember(x => x.ClubId, opt => opt.Ignore())
                                  .ForMember(x => x.Club, opt => opt.Ignore())
                                       .ForMember(x => x.Appointments, opt => opt.Ignore());


            CreateMap<Event, EditEventViewModel>()
                .ForMember(x => x.URL, opt => opt.MapFrom(src => src.Image))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Desciption))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.Data))
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ForMember(x => x.Succeeded, opt => opt.Ignore());        
        }

    }
}







