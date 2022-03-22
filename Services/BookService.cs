using LibraryManagementPortal.DB;
using LibraryManagementPortal.DTO;
using LibraryManagementPortal.Interfaces;
using LibraryManagementPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementPortal.Services
{
    public class BookService : IBookService
    {
        LibraryDBContext _context;

        public BookService(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddBookAsync(BookDTO dto)
        {
            if (_context.Books != null)
            {
                try
                {
                    await _context.Books.AddAsync(dto.BookDTOToEntity());
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(e);
                }
            }
            else
                return new NoContentResult();
        }

        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            if (_context.Books != null)
            {
                try
                {
                    var foundBook = await _context.Books.FindAsync(id);
                    if (foundBook != null)
                    {
                        _context.Books.Remove(foundBook);
                        await _context.SaveChangesAsync();
                        return new OkResult();
                    }
                    else
                    {
                        return new NotFoundResult();
                    }
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(e);
                }
            }
            else
                return new NoContentResult();
        }

        public async Task<ActionResult<List<BookDTO>>> GetAllBooksAsync()
        {
            if (_context.Books != null)
            {
                try
                {
                    var books = await _context.Books
                        .Include(c => c.Category)
                        .Select(book => book.BookEntityToDTOComplete())
                        .ToListAsync();
                    return new OkObjectResult(books);
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(e);
                }
            }
            return new NoContentResult();
        }

        public async Task<ActionResult<BookDTO>> GetBookAsync(int id)
        {
            if (_context.Books != null)
            {
                try
                {
                    var foundBook = await _context.Books.FindAsync(id);
                    if (foundBook != null)
                        return new OkObjectResult(foundBook.BookEntityToDTOComplete());
                    else
                        return new NotFoundResult();
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(e);
                }
            }
            return new NoContentResult();
        }

        public async Task<IActionResult> UpdateBookAsync(int id, BookDTO dto)
        {
            if (_context.Books != null)
            {
                try
                {
                    var foundBook = await _context.Books.FindAsync(id);
                    if (foundBook != null)
                    {
                        foundBook = dto.BookDTOToEntity();
                        _context.Books.Update(foundBook);
                        await _context.SaveChangesAsync();
                        return new OkResult();
                    }
                    else
                        return new NotFoundResult();
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(e);
                }
            }
            else
                return new NoContentResult();
        }
    }
}