using AutoMapper;
using HotelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetReviewByIdAsync(int reviewId)
        {
            var review = await _context.Reviews.Where(r => r.ReviewId == reviewId).FirstOrDefaultAsync();
            
            if(review == null)
                return null;

            return review;

        }


        public async Task CreateReviewAsync(Review review)
        {
            if(review == null)
                throw new ArgumentNullException(nameof(review));
            
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReviewAsync(Review review)
        {
            if(review == null)
                throw new ArgumentNullException(nameof(review));
                
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            if(reviewId == 0)
                throw new ArgumentNullException(nameof(reviewId));

            var review = await GetReviewByIdAsync(reviewId);
            if(review == null)
                throw new ArgumentNullException(nameof(review));
                
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ReviewExistsAsync(int id)
        {
            return await _context.Reviews.AnyAsync(c => c.ReviewId == id);
        }
    }
}