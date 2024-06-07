using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace HotelBookingSystem.Services
{
    public class TokenServices
    {
        private readonly IConfiguration _configuration;
        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateToken(dynamic user)
        {
            var securityKey = new SymmetricSecurityKey(
                Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            var audience = "";

            if(user is Admin)
            {
                claims.Add(new Claim("id", user.AdminId.ToString()));
                claims.Add(new Claim("role", "Admin"));
                audience = _configuration["Authentication:AdminAudience"];
            }
            else if(user is Customer)
            {
                claims.Add(new Claim("id", user.CustomerId.ToString()));
                claims.Add(new Claim("role", "Customer"));
                audience = _configuration["Authentication:CustomerAudience"];
            }
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("given_name", user.FirstName));
            claims.Add(new Claim("family_name", user.LastName));

            var token = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                audience,
                claims,
                DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}