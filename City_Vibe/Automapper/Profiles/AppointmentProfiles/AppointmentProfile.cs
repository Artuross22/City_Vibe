using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AppointmentController;
using City_Vibe.ViewModels.EventController;

namespace City_Vibe.Automapper.Profiles.AppointmentProfile
{
    public class AppointmentProfile : Profile
    {

        public AppointmentProfile()
        {
            CreateMap<AppointmentViewModel, Appointment>()
                .ForMember(x => x.AppUser, opt => opt.Ignore())
                  .ForMember(x => x.Event, opt => opt.Ignore())
                   .ForMember(x => x.Statement, opt => opt.Ignore())
                     .ForMember(x => x.ReplyAppointments, opt => opt.Ignore());

            CreateMap<Appointment, AppointmentViewModel>()
                 .ForMember(x => x.PhotoSucceeded, opt => opt.Ignore());


            CreateMap<Appointment, ApplicationUserViewModel>()
               .ForMember(x => x.UserName, opt => opt.Ignore())
                .ForMember(x => x.ProfileImageUrl, opt => opt.Ignore())
                 .ForMember(x => x.Email, opt => opt.Ignore());


            CreateMap<Appointment, PersonalApplicationViewModel>()
                .ForMember(x => x.StartEvent, opt => opt.Ignore())
                 .ForMember(x => x.UserName, opt => opt.Ignore())
                   .ForMember(x => x.Category, opt => opt.Ignore())
                       .ForMember(x => x.ProfileImageUrl, opt => opt.Ignore())
                           .ForMember(x => x.Email, opt => opt.Ignore());


            CreateMap<Appointment, Appointment>();

            CreateMap<ReplyAppointment, ReplyAppointment>();

        }
    }
}
