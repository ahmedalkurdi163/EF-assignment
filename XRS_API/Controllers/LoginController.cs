using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XRS_API.Models;
using XRS_API.Services;

namespace XRS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILoginRepository loginRepository;

        public LoginController(IConfiguration configuration,
            ILoginRepository loginRepository)
        {
            this.configuration = configuration;
            this.loginRepository = loginRepository;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthorizationRequest request)
        {
            var token = loginRepository.LoginAsync(request);
            if(token == null) return Unauthorized();
            return Ok(token.Result);
        }
    }
}
