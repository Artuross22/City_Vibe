﻿
using City_Vibe.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace City_Vibe.ViewModels.EventController
{
    public class CreateEventViewModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }
        public Category? EventCategory { get; set; }

        public IEnumerable<SelectListItem>? EventList { get; set; }
        public string AppUserId { get; set; }

        public int? ClubId { get; set; }
    }
}
