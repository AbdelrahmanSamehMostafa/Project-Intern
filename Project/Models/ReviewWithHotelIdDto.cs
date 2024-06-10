namespace HotelBookingSystem.Models
{
    public record ReviewWithHotelIdDTO : ReviewBaseDto
    {

        public int HotelId { get; set; }


    }
    
}