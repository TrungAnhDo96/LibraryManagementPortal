using LibraryManagementPortal.Common;
using LibraryManagementPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementPortal.Interfaces
{
    public interface IUserService
    {
        public Task<ActionResult<List<UserDTO>>> GetAllUserAsync();
        public Task<ActionResult<UserDTO>> GetUserAsync(int id);
    }
}