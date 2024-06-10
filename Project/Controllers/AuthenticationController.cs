using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Authentication")]
    [Authorize]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenServices _tokenServices;
        private readonly ValidationServices _validationServices;

        public AuthenticationController(TokenServices tokenServices, ValidationServices validationServices)
        {
            _tokenServices = tokenServices ?? throw new ArgumentNullException(nameof(tokenServices));
            _validationServices = validationServices ?? throw new ArgumentNullException(nameof(validationServices));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(UserForLoginDTO userForLoginDTO)
        {
            var user = await _validationServices.ValidateUserCredentials(userForLoginDTO.Email, userForLoginDTO.Password);

            if (user.User == null)
            {
                return Unauthorized();
            }

            return _tokenServices.GenerateToken(user);
        }


    }
}
