using AutoMapper;
using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.ExtensionMethod;
using City_Vibe.ViewModels.AppointmentController;
using City_Vibe.ViewModels.EventController;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Azure.Core;

namespace City_Vibe.Services
{
    public class AppointmentService : IAppointmentService
    {
      
        public readonly IHttpContextAccessor сontextAccessor;
        public readonly IUnitOfWork unitOfWorkRepository;
        public readonly IMapper mapper;

        public AppointmentService(
            IHttpContextAccessor _сontextAccessor,
            IUnitOfWork _unitOfWorkRepository,
            IMapper _mapper)
        {
            сontextAccessor = _сontextAccessor;
            unitOfWorkRepository = _unitOfWorkRepository;
            mapper = _mapper;

        }

        public AppointmentViewModel AddUserAppointmentGet(int eventId)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var userappointment =  new AppointmentViewModel();
            userappointment.AppUserId = curUserId;
            userappointment.EventId = eventId;
            return userappointment;
        }

        public AppointmentViewModel AddUserAppointmentPost(AppointmentViewModel appointmentModel)
        {
            appointmentModel.CreatedDate = DateTime.Now;
            Regex regex = new Regex(@"\+[0-9]{2}[0-9]{3}[0-9]{3}[0-9]{4}$");
            MatchCollection matches = regex.Matches(appointmentModel.Phone);
            if (matches.Count <= 0)
            {
               // addPhone = matches[0].Value;
                appointmentModel.PhotoSucceeded = false;
                return appointmentModel;
            }
        
            var addAppointmentModel =  mapper.Map<Appointment>(appointmentModel);
            addAppointmentModel.CreatedDate = DateTime.Now;

            unitOfWorkRepository.AppointmentRepository.Add(addAppointmentModel);
            unitOfWorkRepository.Save();

            return appointmentModel;

        }


        public IEnumerable<ApplicationUserViewModel> AdmissionRequests(int eventId)
        {
            var application = unitOfWorkRepository.AppointmentRepository.GetAppointmentsByEventId(eventId);

            List<ApplicationUserViewModel> result = new List<ApplicationUserViewModel>();
            foreach (var appointment in application)
            {
                var viewApplication = mapper.Map<ApplicationUserViewModel>(appointment);
                viewApplication.Email = appointment.AppUser.Email;
                viewApplication.ProfileImageUrl = appointment.AppUser.ProfileImageUrl;
                viewApplication.UserName = appointment.AppUser.UserName;
                result.Add(viewApplication);
            }
            return result;
        }

       

        public bool ReplayStatement(ReplyAppointment replyApp)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var result = mapper.Map<ReplyAppointment>(replyApp);
            result.AppUserId = curUserId;

            var response =  unitOfWorkRepository.AppointmentRepository.AddReplyAppointment(result);
            return response;

        }

        public bool AddAppointmentUpdate(AppointmentUpdateVM appointmentVM)
        {
            var result = unitOfWorkRepository.AppointmentRepository.Find(x => x.Id == appointmentVM.AppointmentId).AsNoTracking().FirstOrDefault();

            var updateAppointment = mapper.Map<Appointment>(result);
            updateAppointment.Statement = appointmentVM.Statement;

            if (result?.ReplyAppointments != null)
            {
                updateAppointment.ReplyAppointments = result.ReplyAppointments.ToList();
            }

           var response = unitOfWorkRepository.AppointmentRepository.Update(updateAppointment);

           return response;
        }

        public IEnumerable<PersonalApplicationViewModel> UserApplications()
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var getappointment = unitOfWorkRepository.AppointmentRepository.Find(x => x.AppUserId == curUserId).Include(x => x.AppUser).Include(e => e.Event);

            List<PersonalApplicationViewModel> result = new List<PersonalApplicationViewModel>();

            foreach (var appointment in getappointment)
            {
                var categoryEvent = unitOfWorkRepository.CategoryRepository.GetById(appointment.Event.CategoryId);

                var viewModel = mapper.Map<PersonalApplicationViewModel>(appointment);
                viewModel.EventName = appointment.Event.Name;
                viewModel.UserName = appointment.AppUser?.UserName;
                viewModel.ProfileImageUrl = appointment.Event.Image;
                viewModel.Email = appointment.AppUser?.Email;
                viewModel.StartEvent = appointment.Event.Data;
                viewModel.Category = categoryEvent.Name;

                result.Add(viewModel);

            }
            return result;
        }

        public IEnumerable<ApplicationUserViewModel> ViewParticipants(int eventId)
        {
            var application = unitOfWorkRepository.AppointmentRepository.GetAppointmentsByEventId(eventId);

            List<ApplicationUserViewModel> result = new List<ApplicationUserViewModel>();
            foreach (var appointment in application)
            {
                var viewApplication = mapper.Map<ApplicationUserViewModel>(appointment);
                viewApplication.ProfileImageUrl = appointment.AppUser.ProfileImageUrl;
                viewApplication.Email = appointment.AppUser.Email;
                viewApplication.UserName = appointment.AppUser.UserName;

                result.Add(viewApplication);
            }

            return result;
        }

     
    }
}
