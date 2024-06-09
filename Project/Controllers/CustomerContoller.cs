using AutoMapper;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Customer")]
    [Authorize]
    [ApiController]
    public class CustomerContoller : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private IHotelRepository _hotelRepository;
        private IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CustomerContoller> _logger;
        public CustomerContoller(ICustomerRepository customerRepository, IMapper mapper, IEmailService emailService, IMemoryCache memoryCache, IHotelRepository hotelRepository, ILogger<CustomerContoller> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _memoryCache = memoryCache;
            _logger = logger;
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            if (customers == null)
            {
                return NotFound();
            }
            var customerDTOs = customers.Select(customer => _mapper.Map<CustomerDTO>(customer)).ToList();
            return Ok(customerDTOs);
        }

        [HttpGet("{customerId}", Name = "GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            var customerDtoToReturn = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDtoToReturn);
        }


        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer(CustomerForCreationDTO customerDto)
        {
            _logger.LogInformation("Received request to create a new customer.");

            var customerEntity = _mapper.Map<Customer>(customerDto);
            customerEntity.IsEmailVerified = false; // Set default to false

            // Generate OTP
            string otp = GenerateOTP();
            _logger.LogInformation($"Generated OTP: {otp}");

            // Store OTP in memory cache with expiration (e.g., valid for 10 minutes)
            _memoryCache.Set(customerEntity.Email, otp, TimeSpan.FromMinutes(10));
            _logger.LogInformation($"Stored OTP in memory cache for email: {customerEntity.Email}");

            // Send OTP via email
            string subject = "Email Verification OTP";
            string message = $"Your OTP for email verification is: {otp}";
            await _emailService.SendEmailAsync(customerEntity.Email, subject, message);
            _logger.LogInformation($"Sent OTP via email to: {customerEntity.Email}");

            // Save customer to repository
            await _customerRepository.CreateCustomerAsync(customerEntity);
            _logger.LogInformation("Saved customer to repository.");

            var customerToReturn = _mapper.Map<CustomerDTO>(customerEntity);
            _logger.LogInformation("Returning created customer DTO.");
            return CreatedAtRoute("GetCustomerById", new { customerId = customerToReturn.CustomerId }, customerToReturn);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailDTO verificationInfo)
        {
            _logger.LogInformation("Received request to verify email.");

            // Retrieve saved OTP based on email
            string savedOTP = RetrieveOTPFromCache(verificationInfo.Email);
            _logger.LogInformation($"Retrieved OTP from cache: {savedOTP}");

            if (savedOTP == null)
            {
                _logger.LogWarning("OTP expired or not found. Please request a new OTP.");
                return BadRequest("OTP expired or not found. Please request a new OTP.");
            }

            if (!string.Equals(savedOTP, verificationInfo.OTP, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning("Invalid OTP received. Verification failed.");
                return BadRequest("Invalid OTP. Please check your OTP and try again.");
            }

            // OTP verified successfully, update customer's email verification status
            var customer = await _customerRepository.GetCustomerByEmailAsync(verificationInfo.Email);

            if (customer == null)
            {
                _logger.LogWarning("Customer not found.");
                return NotFound("Customer not found."); // Handle if customer not found
            }

            customer.IsEmailVerified = true;
            await _customerRepository.UpdateCustomerAsync(customer);
            _logger.LogInformation("Customer email verified successfully.");

            // Remove OTP from storage after successful verification
            RemoveOTPFromStorage(verificationInfo.Email);
            _logger.LogInformation("Removed OTP from cache after verification.");

            return Ok("Email verified successfully");
        }

        private string RetrieveOTPFromCache(string email)
        {
            if (!_memoryCache.TryGetValue(email, out string otp))
            {
                _logger.LogWarning("OTP not found in cache.");
                return null; // OTP not found
            }
            return otp;
        }

        private void RemoveOTPFromStorage(string email)
        {
            _memoryCache.Remove(email);
            _logger.LogInformation("Removed OTP from cache.");
        }

        private string GenerateOTP()
        {
            // Generate and return a random OTP (e.g., using a random number generator)
            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString(); // 6-digit OTP example
            _logger.LogInformation($"Generated OTP: {otp}");
            return otp;
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, CustomerForUpdateDTO customer)
        {

            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);

            // Map properties from customerDTO to existingCustomer
            _mapper.Map(customer, existingCustomer);

            // Update the customer in the repository
            await _customerRepository.UpdateCustomerAsync(existingCustomer);

            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            await _customerRepository.DeleteCustomerAsync(customerId);
            return NoContent();
        }

        [HttpGet("{customerId}/hotels")]
        public async Task<IActionResult> GetCustomerWishlists(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            // Get hotels based on the list of hotel IDs in customer's wishlist
            var hotelsInWishlist = await _hotelRepository.GetHotelsByIdsAsync(customer.Wishlists);
            if (hotelsInWishlist == null || !hotelsInWishlist.Any())
            {
                return NotFound("No hotels found in customer's wishlist.");
            }

            var hotelDTOs = hotelsInWishlist.Select(hotel => _mapper.Map<HotelDto>(hotel)).ToList();
            return Ok(hotelDTOs);
        }

        [HttpPost("{customerId}/hotels/{hotelId}")]
        public async Task<IActionResult> AddHotelToCustomerWishlist(int customerId, int hotelId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            // Add hotel ID to customer's wishlist
            if (customer.Wishlists == null)
            {
                customer.Wishlists = new List<string>();
            }
            customer.Wishlists.Add(hotelId.ToString());

            // Update customer in the repository
            await _customerRepository.UpdateCustomerAsync(customer);

            return Ok($"Hotel with ID {hotelId} added to customer's wishlist.");
        }

        [HttpDelete("{customerId}/hotels/{hotelId}")]
        public async Task<IActionResult> RemoveHotelFromWishlist(int customerId, int hotelId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                _logger.LogWarning($"Customer with ID {customerId} not found.");
                return NotFound("Customer not found.");
            }

            // Check if the hotel is in the customer's wishlist
            if (customer.Wishlists == null || !customer.Wishlists.Contains(hotelId.ToString()))
            {
                _logger.LogWarning($"Hotel with ID {hotelId} not found in customer's wishlist.");
                return NotFound("Hotel not found in customer's wishlist.");
            }

            // Remove hotel ID from customer's wishlist
            customer.Wishlists.Remove(hotelId.ToString());

            // Update customer in the repository
            try
            {
                await _customerRepository.UpdateCustomerAsync(customer);
                _logger.LogInformation($"Hotel with ID {hotelId} removed from customer's wishlist successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error removing hotel with ID {hotelId} from customer's wishlist: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

    }
}
