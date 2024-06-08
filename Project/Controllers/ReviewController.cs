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
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public ReviewContoller(IReviewRepository reviewRepository, ICustomerRepository customerRepository, IMapper mapper, IHotelRepository hotelRepository)
        {
            _reviewRepository = reviewRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _hotelRepository = hotelRepository;
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

        [HttpGet("Customer/{customerId}")]
        public async Task<IActionResult> GetReviewsByCustomerId(int customerId)
        {
            var reviews = await _reviewRepository.GetReviewsByCustomerIdAsync(customerId);
            if (reviews == null || !reviews.Any())
            {
                return NotFound("No reviews found for this customer.");
            }

            var reviewsDTO = _mapper.Map<IEnumerable<ReviewWithHotelIdDTO>>(reviews);
            return Ok(reviewsDTO);
        }

        [HttpGet("Hotel/{hotelId}")]
        public async Task<IActionResult> GetReviewsByHotelId(int hotelId)
        {
            var reviews = await _reviewRepository.GetReviewsByHotelIdAsync(hotelId);
            if (reviews == null || !reviews.Any())
            {
                return NotFound("No reviews found for this hotel.");
            }

            var hotel = await _hotelRepository.GetHotelById(hotelId);
            if (hotel == null)
            {
                return NotFound("Hotel not found.");
            }

            var reviewsDTO = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
            var result = new
            {
                Reviews = reviewsDTO,
                AverageRating = hotel.averageRating
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("{CustomerId}/{hotelId}")]
        public async Task<IActionResult> CreateReview(int CustomerId, int hotelId, [FromBody] ReviewForCreationDTO reviewdto)
        {
            if (!await _customerRepository.CustomerExistsAsync(CustomerId))
            {
                return NotFound("Customer Not Found");
            }

            if (!await _hotelRepository.HotelExistsAsync(hotelId))
            {
                return NotFound("Hotel Not Found");
            }

            var reviewEntity = _mapper.Map<Review>(reviewdto);
            reviewEntity.CustomerId = CustomerId;
            reviewEntity.HotelId = hotelId;

            await _reviewRepository.CreateReviewAsync(reviewEntity);

            var reviews = await _reviewRepository.GetReviewsByHotelIdAsync(hotelId);
            if (reviews == null || !reviews.Any())
            {
                return NotFound("No reviews found for this hotel.");
            }

            // Calculate averageRating
            double averageRating = reviews.Average(r => r.Rating);

            // Update hotel's averageRating and Rating
            var hotel = await _hotelRepository.GetHotelById(hotelId);
            if (hotel == null)
            {
                return NotFound("Hotel not found.");
            }

            hotel.averageRating = averageRating;
            hotel.Rating = hotel.averageRating;

            // Update hotel in the database
            await _hotelRepository.UpdateHotel(hotel.HotelId, _mapper.Map<HotelUpdateDto>(hotel));

            var reviewToReturn = _mapper.Map<ReviewWithIdDTO>(reviewEntity);
            return CreatedAtRoute("GetReviewById", new { reviewId = reviewToReturn.ReviewId }, reviewToReturn);
        }


        [HttpDelete]
        [Route("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            if (!await _reviewRepository.ReviewExistsAsync(reviewId))
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

            if (reviewId != reviewWithIdDTO.ReviewId)
                return BadRequest();


            _mapper.Map(reviewWithIdDTO, existingReview);

            await _reviewRepository.UpdateReviewAsync(existingReview);

            return NoContent();
        }


    }

}