using LibraryManagementPortal.DB;
using LibraryManagementPortal.DTO;
using LibraryManagementPortal.Interfaces;
using LibraryManagementPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementPortal.Services
{
    public class CategoryService : ICategoryService
    {
        LibraryDBContext _context;

        public CategoryService(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddCategoryAsync(CategoryDTO dto)
        {
            if (_context.Categories != null)
            {
                try
                {
                    await _context.Categories.AddAsync(dto.CategoryDTOToEntity());
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

        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            if (_context.Categories != null)
            {
                try
                {
                    var foundCategory = await _context.Categories.FindAsync(id);
                    if (foundCategory != null)
                    {
                        _context.Categories.Remove(foundCategory);
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

        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategoriesAsync()
        {
            if (_context.Categories != null)
            {
                try
                {
                    var categories = await _context.Categories.Select(category => category.CategoryEntityToDTO()).ToListAsync();
                    return new OkObjectResult(categories);
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(e);
                }
            }
            return new NoContentResult();
        }

        public async Task<ActionResult<CategoryDTO>> GetCategoryAsync(int id)
        {
            if (_context.Categories != null)
            {
                try
                {
                    var foundCategory = await _context.Categories.FindAsync(id);
                    if (foundCategory != null)
                        return new OkObjectResult(foundCategory.CategoryEntityToDTO());
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

        public async Task<IActionResult> UpdateCategoryAsync(int id, CategoryDTO dto)
        {
            if (_context.Categories != null)
            {
                try
                {
                    var foundCategory = await _context.Categories.FindAsync(id);
                    if (foundCategory != null)
                    {
                        foundCategory = dto.CategoryDTOToEntity();
                        _context.Categories.Update(foundCategory);
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