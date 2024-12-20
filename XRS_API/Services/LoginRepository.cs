
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XRS_API.Models;

namespace XRS_API.Services
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration configuration;

        public LoginRepository( IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<string?> LoginAsync(AuthorizationRequest request)
        {
            var user = ValidateUserInformation(request.UserName, request.Password);

            if (user == null)
                return Task.FromResult<string?>(null);

            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("given_name", user.FirstName),
                new Claim("family_name", user.LastName),
                new Claim("course", "Midad_11"),
                new Claim("sub", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var securityToken = new JwtSecurityToken(
                configuration["Authentication:Issuer"],
                configuration["Authentication:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(10),
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return Task.FromResult<string?>(token);
        }
        private XRS_User? ValidateUserInformation(string userName, string password)
        {
            if (userName == configuration["UserInformation:username"] && password == configuration["UserInformation:password"])
            {
                return new XRS_User
                {
                    FirstName = "Ahmed",
                    LastName = "Alkurdi",
                    UserName = "Ahmed163a",
                    UserId = 1,
                };
            }
            return null;
        }
    }
}
