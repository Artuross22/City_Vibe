using System.ComponentModel.DataAnnotations;

namespace City_Vibe.RequestModel.BaseModel
{
    public class BaseRequestModel
    {
        [Required]
        public int? Id { get; set; }
    }
}
