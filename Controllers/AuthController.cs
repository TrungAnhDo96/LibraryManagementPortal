using LibraryManagementPortal.Common;
using LibraryManagementPortal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementPortal.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Authenticate([FromBody] LoginDetails details)
        {
            return await _service.AuthenticateAsync(details);
        }
    }
}