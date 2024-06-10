namespace HotelBookingSystem.Models
{
    public record ReviewDTO : ReviewBaseDto
    {
        public DateTime Date { get; set; } = DateTime.Now;

    }
    
}