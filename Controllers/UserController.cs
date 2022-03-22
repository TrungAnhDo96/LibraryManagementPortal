using LibraryManagementPortal.DTO;
using LibraryManagementPortal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementPortal.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Super")]
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUser()
        {
            return await _service.GetAllUserAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            return await _service.GetUserAsync(id);
        }

    }
}