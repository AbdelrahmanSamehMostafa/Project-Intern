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

        public LoginResponseDTO GenerateAdminToken(dynamic admin) //admin token
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("sub", admin.User.AdminId.ToString(), SecurityAlgorithms.HmacSha256));
            claims.Add(new Claim("given_name", admin.User.FirstName));
            claims.Add(new Claim("family_name", admin.User.LastName));
            claims.Add(new Claim("Role", "Admin"));

            var token = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:audience"],
                claims,
                DateTime.UtcNow,
                DateTime.Now.AddHours(1),
                credentials
            );
            var response = new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return response;
        }


        public LoginResponseDTO GenerateCustomerToken(dynamic customer) //customer token
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("sub", customer.User.CustomerId.ToString(), SecurityAlgorithms.HmacSha256));
            claims.Add(new Claim("given_name", customer.User.FirstName));
            claims.Add(new Claim("family_name", customer.User.LastName));
            claims.Add(new Claim("Role", "Customer"));

            var token = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:audience"],
                claims,
                DateTime.UtcNow,
                DateTime.Now.AddHours(1),
                credentials
            );

            var response = new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return response;
        }
        public LoginResponseDTO Generate_SA_Token(dynamic customer)// Super Admin Token 
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("sub", "1"));
            claims.Add(new Claim("given_name", "System"));
            claims.Add(new Claim("family_name", "Admin"));
            claims.Add(new Claim("Role", "SuperAdmin"));

            var token = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:audience"],
                claims,
                DateTime.UtcNow,
                DateTime.Now.AddHours(1),
                credentials
            );

            var response = new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return response;
        }

    }
}
