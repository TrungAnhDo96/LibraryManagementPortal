using LibraryManagementPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementPortal.Interfaces
{
    public interface IBookService
    {
        public Task<ActionResult<List<BookDTO>>> GetAllBooksAsync();

        public Task<ActionResult<BookDTO>> GetBookAsync(int id);

        public Task<IActionResult> AddBookAsync(BookDTO dto);

        public Task<IActionResult> UpdateBookAsync(int id, BookDTO dto);

        public Task<IActionResult> DeleteBookAsync(int id);

    }
}