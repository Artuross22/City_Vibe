using AutoMapper;
using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.ExtensionMethod;
using City_Vibe.Services.Base;
using City_Vibe.ViewModels.EventController;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Services
{
    public class EventService : IEventService
    {

        private readonly IMapper mapper;
        private readonly IPhotoService photoService;
        public readonly IHttpContextAccessor сontextAccessor;
        public readonly IUnitOfWork unitOfWorkRepository;

        public EventService(
            IPhotoService photoSe,
            IHttpContextAccessor сontextAccsess,
            IUnitOfWork unitOfWorkRepo,
            IMapper mapp
            )
        {
            photoService = photoSe;
            сontextAccessor = сontextAccsess;
            unitOfWorkRepository = unitOfWorkRepo;
            mapper = mapp;
        }

        public async Task<EventFilterViewModel> Index(int? category, string? name)
        {
            IQueryable<Event> eventVM = unitOfWorkRepository.EventRepository
            .GetQueryable()
            .Include(x => x.Category)
            .OrderByDescending(x => x.Data);


            if (category != null && category != 0)
            {
                eventVM = eventVM.Where(p => p.CategoryId == category);
            }
            if (!string.IsNullOrEmpty(name))
            {
                eventVM = eventVM.Where(p => p.Name!.Contains(name));
            }

            List<Category> categories = await unitOfWorkRepository.CategoryRepository.GetAllAsync();

            categories.Insert(0, new Category { Name = "All", Id = 0 });

            EventFilterViewModel viewModel = new EventFilterViewModel
            {
                Events = eventVM.ToList(),
                Category = new SelectList(categories, "Id", "Name", category),
                Name = name
            };
            return viewModel;
        }

        public async Task<EventDetailViewModel> DetailEvent(int currentEventId)
        {
            var eventDetail = await unitOfWorkRepository.EventRepository.GetByIdIncludeCategoryAndAddressAsync(currentEventId);
            var currentUserId = сontextAccessor.HttpContext.User.GetUserId();
            var curSaveEvent = unitOfWorkRepository.SaveEventRepository.Find(c => c.EventId == currentEventId);

            var сheckAppointment = unitOfWorkRepository.EventRepository.CheckingTheExistenceOfAnAppointment(currentEventId, currentUserId);
            var replyAppointment = unitOfWorkRepository.EventRepository.ReplyAppointment(currentEventId, currentUserId);

            var viewModel = mapper.Map<EventDetailViewModel>(eventDetail);
            viewModel.SaveEvents = curSaveEvent.ToList();
            viewModel.CheckAppointment = сheckAppointment;

            var listofComment = unitOfWorkRepository.CommentRepository
                .Find(x => x.EventId == currentEventId)
                .Include(x => x.ReplyComment)
                .ThenInclude(x => x.AppUser)
                .OrderByDescending(x => x.DateTime)
                .ToList();

            viewModel.Comments = listofComment;

            var categoryEvent = unitOfWorkRepository.CategoryRepository.GetById(eventDetail.CategoryId);
            viewModel.Category = categoryEvent;


            if (replyAppointment != null)
            {
                viewModel.Statement = replyAppointment.Statement;
                if (replyAppointment.ReplyAppointments != null)
                {
                    viewModel.ReplyAppointments = replyAppointment.ReplyAppointments.ToList();
                }
            }

            return viewModel;

        }

        public CreateEventViewModel CreateEventGet(int? clubId)
        {

            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var createEventViewModel = new CreateEventViewModel { AppUserId = curUserId };

            if (clubId != null)
            {
                createEventViewModel.ClubId = clubId;
            }

            return createEventViewModel;
        }

        public async Task<Response> CreateEventPost(CreateEventViewModel eventVM)
        {
            Response response = new Response();

            var addPhoto = await photoService.AddPhotoAsync(eventVM.Image);

            if (addPhoto.Error != null)
            {
                response.PhotoSucceeded = false;
                return response;
            }

            var eventAdd = mapper.Map<Event>(eventVM);
            eventAdd.Image = addPhoto.Url.ToString();
            eventAdd.Data = DateTime.Now;

            if (eventAdd.ClubId != null)
            {
                eventAdd.ClubId = eventVM.ClubId;
            }

            var result = unitOfWorkRepository.EventRepository.Add(eventAdd);
            response.Succeeded = result;
            return response;
        }


        public async Task<EditEventViewModel> EditGet(int id)
        {

            var eventVM = new EditEventViewModel();


            var editEvent = await unitOfWorkRepository.EventRepository.GetByIdIncludeCategoryAndAddressAsync(id);
            if (editEvent == null)
            {
                eventVM.Succeeded = false;
                return eventVM;
            }

            var eventAdd = mapper.Map<EditEventViewModel>(editEvent);
            return eventAdd;
        }

        public async Task<Response> EditPost(EditEventViewModel eventVM)
        {
            Response response = new Response();

            var userEvent = await unitOfWorkRepository.EventRepository.Find(x => x.Id == eventVM.Id).AsNoTracking().Include(c => c.Category).FirstOrDefaultAsync();

            if (userEvent != null)
            {

                try
                {
                    if (eventVM.Image != null)
                    {
                        await photoService.DeletePhotoAsync(userEvent.Image);
                    }

                }
                catch (Exception ex)
                {
                    response.Message = ex.ToString();
                    response.PhotoSucceeded = false;
                    return response;

                }

                string newImage = new Event().Image;

                if (eventVM.Image != null)
                {
                    var photoResult = await photoService.AddPhotoAsync(eventVM.Image);

                    if (photoResult.Error != null)
                    {
                        response.PhotoSucceeded = false;
                        return response;
                    }

                    // newImage = photoResult.Uri.ToString();
                    newImage = photoResult.Url.ToString();
                }


                var eventUpdate = mapper.Map<Event>(eventVM);

                if (newImage != null) eventUpdate.Image = newImage;

                var result = unitOfWorkRepository.EventRepository.Update(eventUpdate);
                response.Succeeded = result;
                return response;
            }
            else
            {
                response.Succeeded = false;
                return response;
            }
        }

        public async Task<Event> DeleteGet(int id)
        {
            var eventDetails = await unitOfWorkRepository.EventRepository.GetByIdIncludeCategoryAndAddressAsync(id);
            return eventDetails;
        }

        public async Task<Response> DeleteEventPost(int id)
        {

            Response response = new Response();
            var eventDelete = await unitOfWorkRepository.EventRepository.GetByIdIncludeCategoryAndAddressAsync(id);


            if (eventDelete == null)
            {
                response.Succeeded = false;
                return response;
            }

            if (!string.IsNullOrEmpty(eventDelete.Image))
            {
                _ = photoService.DeletePhotoAsync(eventDelete.Image);
            }
            var result = unitOfWorkRepository.EventRepository.Delete(eventDelete);
            response.Success = result;
            return response;
        }


        public async Task<Response> AddInterestingEvent(int eventId)
        {
            Response response = new Response();

            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var selectEvent = unitOfWorkRepository.SaveEventRepository.FindSafeEventsinUserAndEvent(eventId);
            if (selectEvent.Count != 0)
            {
                var deleteEvent = await unitOfWorkRepository.SaveEventRepository.Find(c => c.EventId == eventId).FirstOrDefaultAsync();
                if (deleteEvent == null)
                {
                    response.Succeeded = false;
                    return response;
                }
                var result = unitOfWorkRepository.SaveEventRepository.Delete(deleteEvent);
                response.Succeeded = result;
                return response;
            }
            else
            {
                var saveEvent = new SaveEvent
                {
                    AppUserId = curUserId,
                    EventId = eventId,
                };
                var result = unitOfWorkRepository.SaveEventRepository.Add(saveEvent);
                response.Succeeded = result;
                return response;
            }
        }

        public TopUserEventsViewModel EventsSelectByTheUser()
        {
            Response result = new Response();

            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var selectEvent = unitOfWorkRepository.SaveEventRepository.Find(x => x.AppUserId == curUserId).Include(c => c.Event).ToList();

            var viewEvents = new TopUserEventsViewModel
            {
                SaveEvents = selectEvent.ToList(),
            };

            return viewEvents;

        }
    }
}
