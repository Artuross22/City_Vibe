﻿using City_Vibe.Models;

namespace City_Vibe.ViewModels.HomeViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Club>? Clubs { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public HomeUserCreateViewModel Register { get; set; } = new HomeUserCreateViewModel();
    }
}
