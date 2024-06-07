using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenServices _tokenServices;
        private readonly ValidationServices _validationServices;

        public AuthenticationController(IConfiguration configuration, TokenServices tokenServices, ValidationServices validationServices)
        {
            _tokenServices = tokenServices ?? throw new ArgumentNullException(nameof(tokenServices));
            _validationServices = validationServices ?? throw new ArgumentNullException(nameof(validationServices));
        }


        [HttpGet]
        [Route("ValidateUserCredentials")]
        public async Task<dynamic> ValidateUserCredentials([FromForm]string email, [FromForm]string password)
        {
            var user = await _validationServices.ValidateUserCredentials(email, password);
            if (user != null)
            {
                return user;
            }

           return null;   
        }

        [HttpPost]
        [Route("Login")]

        public async Task<ActionResult<string>> Login([FromBody] UserForLoginDTO userForLoginDTO)
        {
            var user = await ValidateUserCredentials(userForLoginDTO.Email, userForLoginDTO.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenToReturn = _tokenServices.GenerateToken(user);


            return Ok(tokenToReturn);
        }


        

    }
}