using LibraryManagementPortal.Common;
using LibraryManagementPortal.DTO;
using LibraryManagementPortal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementPortal.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/requests")]
    public class BorrowRequestController : ControllerBase
    {
        IBorrowRequestService _service;

        public BorrowRequestController(IBorrowRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<BorrowRequestDTO>>> GetAllRequests()
        {
            return await _service.GetAllBorrowRequestsAsync();
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<List<BorrowRequestDTO>>> GetRequestsOfUser(int id)
        {
            return await _service.GetBorrowRequestsOfUserAsync(id);
        }

        [Authorize(Roles = "Normal")]
        [HttpPost]
        public async Task<IActionResult> AddRequest([FromBody] BorrowRequestDTO request)
        {
            return await _service.AddBorrowRequestAsync(request);
        }

        [Authorize(Roles = "Super")]
        [HttpPut("approve/{requestId}")]
        public async Task<IActionResult> ApproveRequest(int requestId, [FromBody] int userId)
        {
            return await _service.ApproveBorrowRequestAsync(requestId, userId);
        }

        [Authorize(Roles = "Super")]
        [HttpPut("reject/{requestId}")]
        public async Task<IActionResult> RejectRequest(int requestId, [FromBody] int userId)
        {
            return await _service.RejectBorrowRequestAsync(requestId, userId);
        }
    }
}