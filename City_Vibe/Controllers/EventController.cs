using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.ViewModels.EventController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace City_Vibe.Controllers
{
    public class EventController : Controller
    {
        private readonly IPhotoService photoService;
        public readonly IHttpContextAccessor сontextAccessor;
        public readonly IUnitOfWork unitOfWorkRepository;
        private readonly ISaveEventRepository saveEventRepository;

        public EventController(
            IPhotoService photoSe, 
            IHttpContextAccessor сontextAccsess,
            ISaveEventRepository saveEventRepo,
            IUnitOfWork unitOfWorkRepo
            )
        {
            photoService = photoSe;
            сontextAccessor = сontextAccsess;
            saveEventRepository = saveEventRepo;
            unitOfWorkRepository = unitOfWorkRepo;
        }

        public async Task<IActionResult> Index(int? category, string? name)
        {
            IQueryable<Event> eventVM = unitOfWorkRepository.EventRepository.ActiveEventAllIQueryable();

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
            return View(viewModel);
        }

     
        public async Task<IActionResult> DetailEvent(int currentEventId)
        {
         
            Event eventDetail = await unitOfWorkRepository.EventRepository.GetByIdIncludeCategoryAndAddressAsync(currentEventId);
            var currentUserId = сontextAccessor.HttpContext.User.GetUserId();
            var curSaveEvent = await saveEventRepository.FindEventByIdAsync(currentEventId);

            var сheckAppointment = unitOfWorkRepository.EventRepository.CheckingTheExistenceOfAnAppointment(currentEventId , currentUserId);
            var replyAppointment = unitOfWorkRepository.EventRepository.ReplyAppointment(currentEventId, currentUserId);

            var viewModel = new EventDetailViewModel
            {
                Id = eventDetail.Id,
                Title = eventDetail.Name,
                Desciption = eventDetail.Desciption,
                AppUser = eventDetail.AppUser,
                Data = eventDetail.Data,
                Image = eventDetail.Image,
                Address = new Address
                {
                    Street = eventDetail.Address.Street,
                    City = eventDetail.Address.City,
                    Region = eventDetail.Address.Region,
                },
                SaveEvents = curSaveEvent.ToList(),
                CheckAppointment = сheckAppointment,
            };
            var listofComment = unitOfWorkRepository.CommentRepository.GetAllCommentByEventId(currentEventId);
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

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateEvent(int clubId)
        {
            var EventList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(EventList, "Id", "Name");

            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var createEventViewModel = new CreateEventViewModel { AppUserId = curUserId };

            if (clubId != null)
            {
                createEventViewModel.ClubId = clubId;
            }

            return View(createEventViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel eventVM)
        {
            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhotoAsync(eventVM.Image);
                var eventAdd = new Event
                {
                    Name = eventVM.Name,
                    Desciption = eventVM.Description,
                    Image = result.Url.ToString(),
                    Data = eventVM.CreatedDate,
                    CategoryId = eventVM.CategoryId,
                    AppUserId = eventVM.AppUserId,
                    Address = new Address
                    {
                        Street = eventVM.Address.Street,
                        City = eventVM.Address.City,
                        Region = eventVM.Address.Region,
                    }
                };

                if (eventAdd.ClubId != null)
                {
                    eventAdd.ClubId = eventVM.ClubId;
                }

                unitOfWorkRepository.EventRepository.Add(eventAdd);
                unitOfWorkRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(eventVM);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editEvent = await unitOfWorkRepository.EventRepository.GetByIdIncludeCategoryAndAddressAsync(id);
            if (editEvent == null) return View("Error");

            var eventVM = new EditEventViewModel
            {
                Name = editEvent.Name,
                Description = editEvent.Desciption,
                AddressId = editEvent.AddressId,
                CreatedDate = editEvent.Data,
                URL = editEvent.Image,
                CategoryId = editEvent.CategoryId,
                Category = editEvent.Category,
                Address = editEvent.Address,
                AppUserId = editEvent.AppUserId,
                 
               
            };

            var EventList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(EventList, "Id", "Name");

            return View(eventVM);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEventViewModel eventVM)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit event");
                return View("Edit", eventVM);
            }

            var userEvent = await unitOfWorkRepository.EventRepository.GetByIdAsyncNoTracking(id);



            if (userEvent != null)
            {
                try
                {
                    await photoService.DeletePhotoAsync(userEvent.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(eventVM);
                }
                var photoResult = await photoService.AddPhotoAsync(eventVM.Image);

                var eventUpdate = new Event
                {
                    Id = id,
                    Name = eventVM.Name,
                    Desciption = eventVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = eventVM.AddressId,
                    Data = eventVM.CreatedDate,
                    CategoryId = eventVM.CategoryId,
                    Address = eventVM.Address,
                    AppUserId = eventVM.AppUserId,
                };

                unitOfWorkRepository.EventRepository.Update(eventUpdate);
                unitOfWorkRepository.Save();

                return RedirectToAction("Index");
            }
            else
            {
                return View(eventVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var eventDetails = await unitOfWorkRepository.EventRepository.GetByIdIncludeCategoryAndAddressAsync(id);

            var EventList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(EventList, "Id", "Name");

            if (eventDetails == null) return View("Error");
            return View(eventDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventDetails = await unitOfWorkRepository.EventRepository.GetByIdIncludeCategoryAndAddressAsync(id);
            

            if (eventDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(eventDetails.Image))
            {
                _ = photoService.DeletePhotoAsync(eventDetails.Image);
            }


            unitOfWorkRepository.EventRepository.Delete(eventDetails);
            unitOfWorkRepository.Save();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddInterestingEvent(int eventId, SaveEvent saveEventModel)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();


            var selectEvent = saveEventRepository.FindSafeEventsinUserAndEvent(eventId);
            if (selectEvent.Count != 0)
            {
                var deleteEvent = await saveEventRepository.FindEventById(eventId);
                saveEventRepository.Delete(deleteEvent);
            }
            else
            {
                var saveEvent = new SaveEvent
                {
                    AppUserId = curUserId,
                    EventId = eventId,
                };
                saveEventRepository.Add(saveEvent);
            }
            return RedirectToAction("DetailEvent", new { id = eventId });
        }

        public async Task<ActionResult> EventsSelectByTheUser(int eventId)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var selectEvent = saveEventRepository.FindEventById(curUserId);

            var viewEvents = new TopUserEventsViewModel
            {
                SaveEvents = selectEvent.ToList(),
            };
            return View(viewEvents);
        }
    }
}
