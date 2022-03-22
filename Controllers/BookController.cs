using LibraryManagementPortal.DTO;
using LibraryManagementPortal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementPortal.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> getAllBooks()
        {
            return await _service.GetAllBooksAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            return await _service.GetBookAsync(id);
        }

        [Authorize(Roles = "Super")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDTO book)
        {
            return await _service.AddBookAsync(book);
        }

        [Authorize(Roles = "Super")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO book)
        {
            return await _service.UpdateBookAsync(id, book);
        }

        [Authorize(Roles = "Super")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return await _service.DeleteBookAsync(id);
        }
    }
}