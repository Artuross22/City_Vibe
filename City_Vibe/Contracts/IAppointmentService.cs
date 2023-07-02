using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AppointmentController;
using City_Vibe.ViewModels.EventController;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Contracts
{
    public interface IAppointmentService
    {
        AppointmentViewModel AddUserAppointmentGet(int eventId);
        AppointmentViewModel AddUserAppointmentPost(AppointmentViewModel appointmentModel);

        IEnumerable<ApplicationUserViewModel> AdmissionRequests(int eventId);
        public bool ReplayStatement(ReplyAppointment replyApp);
        bool AddAppointmentUpdate(AppointmentUpdateVM appointmentVM);

        IEnumerable<PersonalApplicationViewModel> UserApplications();
        IEnumerable<ApplicationUserViewModel> ViewParticipants(int eventId);
    }
}
