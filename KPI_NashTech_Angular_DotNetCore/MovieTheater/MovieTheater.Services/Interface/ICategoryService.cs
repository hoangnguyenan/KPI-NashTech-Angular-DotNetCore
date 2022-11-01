using Microsoft.AspNetCore.Http;
using MovieTheater.Infrastructure.Data.Models;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategory(PaginationDTO paginationDTO, HttpContext httpContext);
        Task<CategoryDTO> GetCategoryById(int Id);
        Task<Category> CreateCategory(CreateCategory createCategory);
        Task<Category> UpdateCategory(int Id, CreateCategory updateCategory);
        Task<CategoryDTO> DeleteCategory(int Id);
    }
}
