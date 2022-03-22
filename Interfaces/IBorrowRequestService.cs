using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementPortal.Common;
using LibraryManagementPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementPortal.Interfaces
{
    public interface IBorrowRequestService
    {
        public Task<ActionResult<List<BorrowRequestDTO>>> GetAllBorrowRequestsAsync();
        public Task<ActionResult<List<BorrowRequestDTO>>> GetBorrowRequestsOfUserAsync(int id);
        public Task<IActionResult> ApproveBorrowRequestAsync(int requestId, int userId);
        public Task<IActionResult> RejectBorrowRequestAsync(int requestId, int userId);
        public Task<IActionResult> AddBorrowRequestAsync(BorrowRequestDTO dto);
    }
}