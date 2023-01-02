using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.Repository;
using City_Vibe.Services;
using City_Vibe.ViewModels.EventController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace City_Vibe.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IPhotoService photoService;
        public readonly IHttpContextAccessor сontextAccessor;

        public EventController(IEventRepository eventRepo, IPhotoService photoSe, IHttpContextAccessor сontextAccsess , ICategoryRepository categoryRepo)
        {
            eventRepository = eventRepo;
            photoService = photoSe;
            сontextAccessor = сontextAccsess;
            categoryRepository =  categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            var eventsList = await eventRepository.GetAll();
            return View(eventsList);
        }

        public async Task<IActionResult> Detail(int id)
        {

            Event eventDetail = await eventRepository.GetByIdAsync(id);
            return View(eventDetail);
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            var EventList = categoryRepository.SelectList();
            ViewBag.Categories = new SelectList(EventList, "Id", "Name");
        
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var createEventViewModel = new CreateEventViewModel { AppUserId = curUserId };    
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
                    CategoryId = eventVM.CategoryId,
                    AppUserId = eventVM.AppUserId,
                    Address = new Address
                    {
                        Street = eventVM.Address.Street,
                        City = eventVM.Address.City,
                        Region = eventVM.Address.Region,
                    }
                };
                eventRepository.Add(eventAdd);
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
            var editEvent = await eventRepository.GetByIdAsync(id);
            if (editEvent == null) return View("Error");

            var eventVM = new EditEventViewModel
            {
                Name = editEvent.Name,
                Description = editEvent.Desciption,
                AddressId = editEvent.AddressId,            
                URL = editEvent.Image,
                CategoryId = editEvent.CategoryId,
                Category = editEvent.Category,
                Address = new Address
                {
                    Street = editEvent.Address.Street,
                    City = editEvent.Address.City,
                    Region = editEvent.Address.Region,
                }
            };

            var EventList = categoryRepository.SelectList();
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

            var userEvent = await eventRepository.GetByIdAsyncNoTracking(id);

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
                    CategoryId = eventVM.CategoryId,
                    Address = new Address
                    {
                        Street = eventVM.Address.Street,
                        City = eventVM.Address.City,
                        Region = eventVM.Address.Region,
                    }
                };

                eventRepository.Update(eventUpdate);

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
            var eventDetails = await eventRepository.GetByIdAsync(id);

            var EventList = categoryRepository.SelectList();
            ViewBag.Categories = new SelectList(EventList, "Id", "Name");

            if (eventDetails == null) return View("Error");
            return View(eventDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventDetails = await eventRepository.GetByIdAsync(id);

            if (eventDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(eventDetails.Image))
            {
               _ = photoService.DeletePhotoAsync(eventDetails.Image);
            }

            eventRepository.Delete(eventDetails);
            return RedirectToAction("Index");
        }



    }
}
