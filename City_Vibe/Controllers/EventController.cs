using City_Vibe.ViewModels.EventController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Contracts;
using City_Vibe.ValidationAttribute.BaseFilters;

namespace City_Vibe.Controllers
{
    public class EventController : Controller
    {
        public readonly IUnitOfWork unitOfWorkRepository;
        public readonly IEventService eventService;

        public EventController(IUnitOfWork unitOfWorkRepo, IEventService _eventService)
        { 
            unitOfWorkRepository = unitOfWorkRepo;
            eventService = _eventService;
        }

        public async Task<IActionResult> Index(int? category, string? name)
        {
            var response = await eventService.Index(category, name);
            return View(response);
        }


        public async Task<IActionResult> DetailEvent(int currentEventId)
        {
            var response = await eventService.DetailEvent(currentEventId);
            return View(response);
        }

        [HttpGet]
        public IActionResult CreateEvent(int? clubId) 
        {
            var EventList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(EventList, "Id", "Name");

            var response =  eventService.CreateEventGet(clubId);
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel eventVM)
        {
            if (ModelState.IsValid)
            {
                var response = await eventService.CreateEventPost(eventVM);

                if(response.PhotoSucceeded == false)
                {
                    ModelState.AddModelError("", "Photo upload failed");
                    return View(eventVM);
                }            
                return RedirectToAction("Index");
            }
            return View(eventVM);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var response = await eventService.EditGet(id);

            if(response.Succeeded == false)
            {
                return View("Error");
            }
            var EventList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(EventList, "Id", "Name");

            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEventViewModel eventVM)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit event");
                return View("EditGet", eventVM);
            }

            var response = await eventService.EditPost(eventVM);

            if(response.PhotoSucceeded == false)
            {
                ModelState.AddModelError("", $"Could not delete photo, {response.Message}");
                return View(eventVM);
            }
            if(response.Succeeded == false) return View(eventVM);   
            
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var eventDetails = await eventService.DeleteGet(id);

            var EventList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(EventList, "Id", "Name");

            if (eventDetails == null) return View("Error");
            return View(eventDetails);
        }

        [HttpPost, ActionName("DeleteGet")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventDetails = await eventService.DeleteEventPost(id);
         
            if (eventDetails.Succeeded == false) return View("Error");

            return RedirectToAction("Index");
        }



        public async Task<ActionResult> AddInterestingEvent(int eventId, SaveEvent saveEventModel)
        {
            var eventDetails = await eventService.AddInterestingEvent(eventId);
            if(eventDetails.Succeeded == false) return View("Error");

            return RedirectToAction("DetailEvent", new { currentEventId = eventId });
        }

        public ActionResult EventsSelectByTheUser()
        {
            var response = eventService.EventsSelectByTheUser();
            return View(response);
        }
    }
}
