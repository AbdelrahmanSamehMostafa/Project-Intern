

namespace HotelBookingSystem.Services
{
    public interface IReviewRepository
    {
        public Task<bool> ReviewExistsAsync(int id);
        public Task<IEnumerable<Review>> GetAllReviewsAsync();
        public Task<Review?> GetReviewByIdAsync(int reviewId);
        public Task CreateReviewAsync(Review review);
        public Task UpdateReviewAsync(Review review);
        public Task DeleteReviewAsync(int reviewId);
        public Task<IEnumerable<Review>> GetReviewsByHotelIdAsync(int hotelId); 
        public Task<IEnumerable<Review>> GetReviewsByCustomerIdAsync(int customerId);
    }
}