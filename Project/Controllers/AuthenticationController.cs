using System.IdentityModel.Tokens.Jwt;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using HotelBookingSystem.interfaces;

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
        public async Task<ActionResult<string>> Login(UserForLoginDTO userForLoginDTO)
        {
            var user = await _validationServices.ValidateUserCredentials(userForLoginDTO.Email, userForLoginDTO.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            switch (user.Role)
            {
                case "Admin":
                    var adminToken = _tokenServices.GenerateAdminToken(user);
                    return Ok(adminToken);

                case "Customer":
                    var CustomerToken = _tokenServices.GenerateCustomerToken(user);
                    return Ok(CustomerToken);

                case "SuperAdmin":
                    var SuperAdminToken = _tokenServices.Generate_SA_Token(user);
                    return Ok(SuperAdminToken);

                default:
                    return NotFound();
            }
        }

    }
}
