using LibraryManagementPortal.Common;
using LibraryManagementPortal.DB;
using LibraryManagementPortal.DTO;
using LibraryManagementPortal.Enums;
using LibraryManagementPortal.Interfaces;
using LibraryManagementPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementPortal.Services
{
    public class BorrowRequestService : IBorrowRequestService
    {
        LibraryDBContext _context;

        public BorrowRequestService(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddBorrowRequestAsync(BorrowRequestDTO request)
        {
            if (_context.BorrowRequests != null && _context.BorrowRequestDetails != null)
            {
                var associateBorrowRequests = (await GetBorrowRequestsOfUserAsync(request.RequestedByUserId)).Value;
                DateTime parsedDate;
                DateTime.TryParse(request.RequestDate, out parsedDate);
                var currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                TimeSpan timeDifference = parsedDate - currentMonth;

                if (associateBorrowRequests != null &&
                    associateBorrowRequests.Count <= 3 &&
                    timeDifference.TotalDays <= 30)
                {
                    await _context.BorrowRequests.AddAsync(request.BorrowRequestDTOToEntity());
                    await _context.SaveChangesAsync();
                    var lastId = (await _context.BorrowRequests.OrderBy(br => br.RequestId).LastAsync()).RequestId;
                    if (lastId != 0)
                    {
                        if (request.RequestDetails != null && request.RequestDetails.Count <= 5)
                        {
                            foreach (var book in request.RequestDetails)
                            {
                                var requestDetails = new BorrowRequestDetailsDTO
                                {
                                    BookId = book.BookId,
                                    BorrowRequestId = lastId,
                                };
                                await _context.BorrowRequestDetails.AddAsync(requestDetails.BorrowRequestDetailsDTOToEntity());
                            }
                            await _context.SaveChangesAsync();
                            return new OkResult();
                        }
                        else
                        {
                            return new BadRequestResult();
                        }
                    }
                    else
                    {
                        return new NotFoundResult();
                    }
                }
                else return new BadRequestResult();
            }
            else return new NoContentResult();
        }

        public async Task<IActionResult> ApproveBorrowRequestAsync(int requestId, int userId)
        {
            if (_context.BorrowRequests != null)
            {
                var foundRequest = await _context.BorrowRequests.FindAsync(requestId);
                if (foundRequest != null)
                {
                    foundRequest.RequestStatus = BorrowRequestStatus.Approved;
                    foundRequest.ProcessedByUserId = userId;
                    _context.BorrowRequests.Update(foundRequest);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else return new NotFoundResult();
            }
            else return new NoContentResult();
        }

        public async Task<IActionResult> RejectBorrowRequestAsync(int requestId, int userId)
        {
            if (_context.BorrowRequests != null)
            {
                var foundRequest = await _context.BorrowRequests.FindAsync(requestId);
                if (foundRequest != null)
                {
                    foundRequest.RequestStatus = BorrowRequestStatus.Rejected;
                    foundRequest.ProcessedByUserId = userId;
                    _context.BorrowRequests.Update(foundRequest);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else return new NotFoundResult();
            }
            else return new NoContentResult();
        }

        public async Task<ActionResult<List<BorrowRequestDTO>>> GetAllBorrowRequestsAsync()
        {
            if (_context.BorrowRequests != null)
            {
                return await _context.BorrowRequests
                    .Include(ru => ru.RequestedUser)
                    .Include(pu => pu.ProcessedUser)
                    .Include(rd => rd.RequestDetails)
                    .ThenInclude(b => b.Book)
                    .Select(br => br.BorrowRequestEntityToDTOComplete())
                    .ToListAsync();
            }
            else return new NoContentResult();
        }

        public async Task<ActionResult<List<BorrowRequestDTO>>> GetBorrowRequestsOfUserAsync(int id)
        {
            if (_context.BorrowRequests != null)
            {
                return await _context.BorrowRequests
                    .Include(ru => ru.RequestedUser)
                    .Include(pu => pu.ProcessedUser)
                    .Include(rd => rd.RequestDetails)
                    .ThenInclude(b => b.Book)
                    .Where(br => br.RequestedByUserId == id)
                    .Select(br => br.BorrowRequestEntityToDTOComplete())
                    .ToListAsync();
            }
            else return new NoContentResult();
        }
    }
}