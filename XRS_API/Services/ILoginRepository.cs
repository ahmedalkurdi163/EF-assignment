using System.IdentityModel.Tokens.Jwt;
using XRS_API.Models;

namespace XRS_API.Services
{
    public interface ILoginRepository
    {
        public Task<string> LoginAsync(AuthorizationRequest request);
    }
}
