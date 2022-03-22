using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementPortal.Interfaces
{
    public interface ICategoryService
    {
        public Task<ActionResult<List<CategoryDTO>>> GetAllCategoriesAsync();
        public Task<ActionResult<CategoryDTO>> GetCategoryAsync(int id);

        public Task<IActionResult> AddCategoryAsync(CategoryDTO dto);

        public Task<IActionResult> UpdateCategoryAsync(int id, CategoryDTO dto);

        public Task<IActionResult> DeleteCategoryAsync(int id);
    }
}