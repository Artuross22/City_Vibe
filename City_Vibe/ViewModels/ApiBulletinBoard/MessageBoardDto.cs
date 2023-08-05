using System.ComponentModel.DataAnnotations;

namespace City_Vibe.ViewModels.ApiBulletinBoard
{
    public class MessageBoardDto
    {
        public Guid? Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; }  = string.Empty;

    }
}
