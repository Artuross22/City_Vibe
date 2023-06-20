using System.ComponentModel.DataAnnotations;

namespace City_Vibe.Domain.Models
{
    public class Address 
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public int ZipCode { get; set; }
    }
}
