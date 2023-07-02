using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AppointmentController;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService appointmentService;

        public AppointmentController( IAppointmentService _appointmentService) => appointmentService = _appointmentService;

        [HttpGet]
        public IActionResult AddUserAppointment(int eventId)
        {
            var response = appointmentService.AddUserAppointmentGet(eventId);
            return View(response);
        }

        [HttpPost]
        public IActionResult AddUserAppointment(AppointmentViewModel appointmentModel)
        {
            if (ModelState.IsValid)
            {
                AppointmentViewModel request = appointmentService.AddUserAppointmentPost(appointmentModel);

                if (request.Success == false)
                {
                    ModelState.AddModelError("Phone", "This is not a valid phone number");
                    return View(appointmentModel);
                }

                return RedirectToAction("", "Event", new { id = appointmentModel.EventId });
            }
            return View(appointmentModel);
        }

        public IActionResult AdmissionRequests(int eventId)
        {
            var result = appointmentService.AdmissionRequests(eventId);
            return View(result);
        }

        public IActionResult AddAppointmentUpdate(AppointmentUpdateVM appointmentVM)
        {
            var result = appointmentService.AddAppointmentUpdate(appointmentVM);
            return RedirectToAction("", "Event", result);
        }

        public IActionResult UserApplications()
        {
            var result = appointmentService.UserApplications();
            return View(result);
        }

        public IActionResult ViewParticipants(int eventId)
        {
            var result = appointmentService.ViewParticipants(eventId);
            return View(result);
        }


        [HttpPost]
        public IActionResult ReplayStatement(ReplyAppointment replyApp)
        {

            var result = appointmentService.ReplayStatement(replyApp);
            if(result)
            {
                return RedirectToAction("", "Event");
            }
            else
            {
                return View(replyApp);
            }
         
        }
    }
}
