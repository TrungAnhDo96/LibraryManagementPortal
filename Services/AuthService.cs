using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryManagementPortal.Common;
using LibraryManagementPortal.DB;
using LibraryManagementPortal.DTO;
using LibraryManagementPortal.Entities;
using LibraryManagementPortal.Interfaces;
using LibraryManagementPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementPortal.Services
{
    public class AuthService : IAuthService
    {
        private IConfiguration _configuration;
        private LibraryDBContext _context;

        public AuthService(IConfiguration config, LibraryDBContext context)
        {
            _configuration = config;
            _context = context;
        }

        public async Task<ActionResult<string>> AuthenticateAsync(LoginDetails details)
        {
            if (details != null && details.UserName != null && details.Password != null)
            {
                var userQuery = await GetLoginUserAsync(details);
                var user = userQuery.Value;

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Role, user.Role != null ? user.Role : "Normal"),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user.FirstName + " " + user.LastName),
                        new Claim("UserName", user.UserName != null ? user.UserName : ""),
                        new Claim("AccessLevel", user.Role != null ? user.Role : ""),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var jwtToken = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: details.isRememerMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddMinutes(15),
                        signingCredentials: signIn);

                    return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(jwtToken));
                }
                else
                {
                    return new UnauthorizedResult();
                }
            }
            else
            {
                return new BadRequestResult();
            }
        }

        private async Task<ActionResult<UserDTO>> GetLoginUserAsync(LoginDetails details)
        {
            if (_context.Users != null)
            {
                List<User> users = await _context.Users.ToListAsync();
                var foundUser = users.FirstOrDefault(user =>
                    user != null &&
                    user.UserName == details.UserName &&
                    user.Password != null &&
                    EncryptionUtils.Base64Decode(user.Password) == details.Password
                );
                if (foundUser != null)
                    return foundUser.UserEntityToDTOSimple();
                else
                    return new NotFoundResult();
            }
            return new NoContentResult();
        }
    }
}