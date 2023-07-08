﻿using City_Vibe.ExtensionMethod;
using City_Vibe.ViewModels.EventController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using AutoMapper;
using City_Vibe.Contracts;

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
            var request = await eventService.Index(category, name);
            return View(request);
        }

     
        public async Task<IActionResult> DetailEvent(int currentEventId)
        {

            var request = await eventService.DetailEvent(currentEventId);
            return View(request);
        }

        [HttpGet]
        public IActionResult CreateEvent(int? clubId) 
        {
            var EventList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(EventList, "Id", "Name");

            var request =  eventService.CreateEventGet(clubId);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel eventVM)
        {
            if (ModelState.IsValid)
            {
                var request = await eventService.CreateEventPost(eventVM);

                if(request.PhotoSucceeded == false)
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

            var request = await eventService.EditGet(id);

            if(request.Succeeded == false)
            {
                return View("Error");
            }
            var EventList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(EventList, "Id", "Name");

            return View(request);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEventViewModel eventVM)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit event");
                return View("EditGet", eventVM);
            }

            var request = await eventService.EditPost(eventVM);

            if(request.PhotoSucceeded == false)
            {
                ModelState.AddModelError("", $"Could not delete photo, {request.Message}");
                return View(eventVM);
            }
            if(request.Succeeded == false) return View(eventVM);   
            
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
            var request = eventService.EventsSelectByTheUser();
            return View(request);
        }
    }
}
