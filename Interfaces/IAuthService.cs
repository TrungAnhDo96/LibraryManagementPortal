using LibraryManagementPortal.Common;
using LibraryManagementPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementPortal.Interfaces
{
    public interface IAuthService
    {
        public Task<ActionResult<string>> AuthenticateAsync(LoginDetails details);
    }
}