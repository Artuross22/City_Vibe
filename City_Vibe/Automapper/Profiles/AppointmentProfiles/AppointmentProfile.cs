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
            CreateMap<AppointmentViewModel, Appointment>();

            CreateMap<Appointment, AppointmentViewModel>();

            CreateMap<Appointment, ApplicationUserViewModel>();

            CreateMap<Appointment, PersonalApplicationViewModel>();

            CreateMap<AppointmentUpdateVM, Appointment>();

            CreateMap<Appointment, Appointment>();

            CreateMap<ReplyAppointment, ReplyAppointment>();
        }

        

    }
}
