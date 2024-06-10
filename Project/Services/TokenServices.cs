using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using HotelBookingSystem.interfaces;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services
{
    public class TokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public LoginResponseDTO GenerateToken(UserValidationResult userValidationResult)
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            if (userValidationResult.Role == "SuperAdmin")
            {
                claims.Add(new Claim("sub", userValidationResult.User.SuperAdminId.ToString()));
                claims.Add(new Claim("given_name", "System"));
                claims.Add(new Claim("family_name", "Admin"));
            }
            else if (userValidationResult.Role == "Admin")
            {
                claims.Add(new Claim("sub", userValidationResult.User.AdminId.ToString()));
                claims.Add(new Claim("given_name", userValidationResult.User.FirstName));
                claims.Add(new Claim("family_name", userValidationResult.User.LastName));
            }
            else if (userValidationResult.Role == "Customer")
            {
                claims.Add(new Claim("sub", userValidationResult.User.CustomerId.ToString()));
                claims.Add(new Claim("given_name", userValidationResult.User.FirstName));
                claims.Add(new Claim("family_name", userValidationResult.User.LastName));
            }
            claims.Add(new Claim("Role", userValidationResult.Role));

            var token = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:audience"],
                claims,
                DateTime.UtcNow,
                DateTime.Now.AddHours(1),
                credentials
            );

            return new LoginResponseDTO { Token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

    }
}
