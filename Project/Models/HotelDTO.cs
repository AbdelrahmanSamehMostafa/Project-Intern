using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    public class HotelDto : HotelBaseDto
    {
        public int HotelId { get; set; }

        [Required]
        // [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public override string? Name { get; set; }
        public double Rating { get; set; }
        
        [Required]
        public override string? ContactInfo { get; set; }
        public Address? Address { get; set; }
        

    }
}