namespace HotelBookingSystem.Data.Models
{
    public class ReviewWithIdDTO : ReviewBaseDto
    {

        public int ReviewId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;


    }
    
}