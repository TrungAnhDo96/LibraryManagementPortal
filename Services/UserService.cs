using LibraryManagementPortal.Common;
using LibraryManagementPortal.DB;
using LibraryManagementPortal.DTO;
using LibraryManagementPortal.Entities;
using LibraryManagementPortal.Interfaces;
using LibraryManagementPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementPortal.Services
{
    public class UserService : IUserService
    {
        LibraryDBContext _context;
        public UserService(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<UserDTO>>> GetAllUserAsync()
        {
            if (_context.Users != null)
            {
                return await _context.Users.Include(br => br.BorrowRequests).Select(user => user.UserEntityToDTOComplete()).ToListAsync();
            }
            return new NoContentResult();
        }

        public async Task<ActionResult<UserDTO>> GetUserAsync(int id)
        {
            if (_context.Users != null)
            {
                var foundUser = await _context.Users.Include(br => br.BorrowRequests).FirstOrDefaultAsync(u => u.UserId == id);
                if (foundUser != null)
                    return foundUser.UserEntityToDTOComplete();
                else
                    return new NotFoundResult();
            }
            return new NoContentResult();
        }
    }
}