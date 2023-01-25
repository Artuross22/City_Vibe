﻿using City_Vibe.Data;
using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.Repository;
using City_Vibe.Services;
using City_Vibe.ViewModels.EventController;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace City_Vibe.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IPhotoService photoService;
        public readonly IHttpContextAccessor сontextAccessor;
        public readonly ICommentRepository commentRepository;
        private readonly ISaveEventRepository saveEventRepository;
        public readonly ApplicationDbContext dbContext;



        public EventController(IEventRepository eventRepo, IPhotoService photoSe, IHttpContextAccessor сontextAccsess, ICategoryRepository categoryRepo, ICommentRepository commentRepo
            , ISaveEventRepository saveEventRepo, ApplicationDbContext DbContexts)
        {
            eventRepository = eventRepo;
            photoService = photoSe;
            сontextAccessor = сontextAccsess;
            categoryRepository = categoryRepo;
            commentRepository = commentRepo;
            dbContext = DbContexts;
            saveEventRepository = saveEventRepo;
        }

        public async Task<IActionResult> Index()
        {
            var eventsList = await eventRepository.GetAll();
            return View(eventsList);
        }

        public async Task<IActionResult> Detail(int id)
        {

            Event eventDetail = await eventRepository.GetByIdIncludeCommentsAsync(id);

            return View(eventDetail);
        }

        public async Task<IActionResult> DetailEvent(int id)
        {
            Event eventDetail = await eventRepository.GetByIdAsync(id);

            var curSaveEvent = await saveEventRepository.FindEventByIdAsync(id);

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
            };
            var listofComment = commentRepository.GetAllCommentByEventId(id);
            viewModel.Comments = listofComment;

            var categoryEvent = categoryRepository.GetById(eventDetail.CategoryId);
            viewModel.Category = categoryEvent;

            return View(viewModel);

        }

        [HttpGet]
        public IActionResult CreateEvent(int clubId)
        {
            var EventList = categoryRepository.SelectList();
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
                    ClubId = eventVM.ClubId,
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
                CreatedDate = editEvent.Data,
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
                    Data = eventVM.CreatedDate,
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

        public async Task<ActionResult> EventsSelectByTheUser()
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var selectEvent = saveEventRepository.FindEventById(curUserId);
            var viewEvents = new TopUserEventsViewModel
            {
                SaveEvents = selectEvent.ToList()
            };
            return View(viewEvents);
        }
    }
}
