using City_Vibe.Data;
using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.Repository;
using City_Vibe.ViewModels.AppointmentController;
using City_Vibe.ViewModels.EventController;
using CityVibe.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public readonly IHttpContextAccessor сontextAccessor;
        private readonly ICategoryRepository categoryRepository;
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentController(ApplicationDbContext applicationDbContext, IHttpContextAccessor сontextAcces, ICategoryRepository categoryRepos, IAppointmentRepository appointmentRepos)
        {
            dbContext = applicationDbContext;
            сontextAccessor = сontextAcces;
            categoryRepository = categoryRepos;
            appointmentRepository = appointmentRepos;
        }

        [HttpGet]
        public async Task<IActionResult> AddUserAppointment(int eventId)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var userappointment = new AppointmentViewModel();
            userappointment.AppUserId = curUserId;
            userappointment.EventId = eventId;
            return View(userappointment);
        }


        [HttpPost]
        public async Task<IActionResult> AddUserAppointment(AppointmentViewModel appointmentModel)
        {
            if (ModelState.IsValid)
            {
                var addAppointmentModel = new Appointment
                {
                    AppUserId = appointmentModel.AppUserId,
                    EventId = appointmentModel.EventId,
                    CreatedDate = DateTime.Now,
                    Description = appointmentModel.Description,
                    Title = appointmentModel.Title,
                    Phone = appointmentModel.Phone,
                };

                appointmentRepository.Add(addAppointmentModel);

                return RedirectToAction("" ,"Event", new { id = appointmentModel.EventId });
            }
            return View(appointmentModel);
        }


        public async Task<IActionResult> AdmissionRequests(int eventId)
        {
            var application = appointmentRepository.GetAppointmentsByEventId(eventId);

            List<ApplicationUserViewModel> result = new List<ApplicationUserViewModel>();
            foreach (var appointment in application)
            {
                var viewApplication = new ApplicationUserViewModel
                {
                    Id = appointment.Id,
                    Title = appointment.Title,
                    Description = appointment.Description,
                    Phone = appointment.Phone,
                    UserName = appointment.AppUser.UserName,
                    ProfileImageUrl = appointment.AppUser.ProfileImageUrl,
                    Email = appointment.AppUser.Email,
                    EventId = appointment.EventId,
                    AppUserId  = appointment.AppUserId,
                    
                };
                result.Add(viewApplication);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult ReplayStatement(ReplyAppointment replyApp)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var result = new ReplyAppointment
            {
                AppointmentId = replyApp.AppointmentId,
                Description = replyApp.Description,
                Reason = replyApp.Reason,
                AppUserId = curUserId,
                EventId = replyApp.EventId,
            };
            appointmentRepository.AddReplyAppointment(result);
            return RedirectToAction("", "Event");
        }

        public IActionResult AddAppointmentUpdate(AppointmentUpdateVM appointmentVM)
        {

            var result = appointmentRepository.GetAppointmentByIdAsNoTracking(appointmentVM.AppointmentId);
            var updateAppointment = new Appointment
            {
                Id = appointmentVM.AppointmentId,
                Title = result.Title,
                Description = result.Description,
                CreatedDate = result.CreatedDate,
                Phone = result.Phone,
                AppUserId = result.AppUserId,
                EventId = result.EventId,
                Statement = appointmentVM.Statement
            };

            if (result.ReplyAppointments != null)
            {
                updateAppointment.ReplyAppointments = result.ReplyAppointments.ToList();
            }

            appointmentRepository.Update(updateAppointment);
            return RedirectToAction("", "Event");
        }



        public async Task<IActionResult> UserApplications()
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var getappointment = appointmentRepository.GetAppointmentByIdUser(curUserId);

            List <PersonalApplicationViewModel> result = new List<PersonalApplicationViewModel>();

            foreach (var appointment in getappointment)
            {
                var categoryEvent = categoryRepository.GetById(appointment.Event.CategoryId);


                var viewModel = new PersonalApplicationViewModel
                {
                    Id = appointment.Id,
                    EventName = appointment.Event.Name,

                    Title = appointment.Title,
                    Category = categoryEvent.Name,
                    StartEvent = appointment.Event.Data,
                    Description = appointment.Description,
                    Phone = appointment.Phone,
                    UserName = appointment.AppUser.UserName,
                    ProfileImageUrl = appointment.Event.Image,
                    Email = appointment.AppUser.Email,
                    CreateStatement = appointment.CreatedDate,
                    EventId = appointment.EventId,
                };
                result.Add(viewModel);

            }
            return View(result);
        }

        public async Task<IActionResult> ViewParticipants(int eventId)
        {
            var application = appointmentRepository.GetAppointmentsByEventId(eventId);

            List<ApplicationUserViewModel> result = new List<ApplicationUserViewModel>();
            foreach (var appointment in application)
            {
                var viewApplication = new ApplicationUserViewModel
                {
                    AppUserId = appointment.AppUserId,
                    Id = appointment.Id,
                    Title = appointment.Title,
                    Phone = appointment.Phone,
                    UserName = appointment.AppUser.UserName,
                    ProfileImageUrl = appointment.AppUser.ProfileImageUrl,
                    Email = appointment.AppUser.Email,
                };
                result.Add(viewApplication);
            }
            return View(result);
        }


    }
}
