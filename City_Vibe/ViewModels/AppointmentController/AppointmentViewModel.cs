using City_Vibe.Domain.Models;
using System.ComponentModel.DataAnnotations;


namespace City_Vibe.ViewModels.AppointmentController
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Descriptionis required")]
        public string? Description { get; set; }

        [Phone]
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? AppUserId { get; set; }
        public int? EventId { get; set; }
    }
}





