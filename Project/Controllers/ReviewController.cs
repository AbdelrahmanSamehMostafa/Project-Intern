using AutoMapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Reviews")]
    [ApiController]
    public class ReviewContoller : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public ReviewContoller(IReviewRepository reviewRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            // if (reviews == null)
            // {
            //     return NotFound();
            // }
            return Ok(_mapper.Map<IEnumerable<ReviewDTO>>(reviews));
        }

        [HttpGet]
        [Route("{reviewId}", Name = "GetReviewById")]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ReviewDTO>(review));
        }

        [HttpPost]
        [Route("{CustomerId}/{HotelId}")]
        public async Task<IActionResult> CreateReview(int CustomerId, int HotelId, [FromBody] ReviewForCreationDTO reviewdto)
        {
            if(!await _customerRepository.CustomerExistsAsync(CustomerId))
                return NotFound();

            //comment this check until we implement the hotel repository
            
            // if(!await _hotelRepository.HotelExistsAsync(HotelId))
            //     return NotFound();

            var reviewEntity = _mapper.Map<Review>(reviewdto);
            reviewEntity.CustomerId = CustomerId;
            reviewEntity.HotelId = HotelId;

            await _reviewRepository.CreateReviewAsync(reviewEntity);

            var reviewToReturn = _mapper.Map<ReviewWithIdDTO>(reviewEntity);
            return CreatedAtRoute("GetReviewById", new { reviewId = reviewToReturn.ReviewId }, reviewToReturn);
        }

        [HttpDelete]
        [Route("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            if(!await _reviewRepository.ReviewExistsAsync(reviewId))
                return NotFound();
                
            await _reviewRepository.DeleteReviewAsync(reviewId);
            return NoContent();

        }

        [HttpPut]
        [Route("{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewWithIdDTO reviewWithIdDTO)
        {
            var existingReview = await _reviewRepository.GetReviewByIdAsync(reviewId);
            if (existingReview == null)
            {
                return NotFound();
            }

            if(reviewId != reviewWithIdDTO.ReviewId)
                return BadRequest();

            
            _mapper.Map(reviewWithIdDTO, existingReview);

            await _reviewRepository.UpdateReviewAsync(existingReview);

            return NoContent();
        }


    }

}