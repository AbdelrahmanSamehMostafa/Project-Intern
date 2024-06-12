using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    public record HotelDto : HotelBaseDto
    {
        public int HotelId { get; set; }

        public double Rating { get; set; }
        
        [Required]
        public override string? ContactInfo { get; set; }
        public Address? Address { get; set; }
        

    }
}