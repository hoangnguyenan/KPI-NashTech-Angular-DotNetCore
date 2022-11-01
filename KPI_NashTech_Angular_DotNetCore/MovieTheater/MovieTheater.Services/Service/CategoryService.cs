using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Infrastructure.Data.ApplicationDbContext;
using MovieTheater.Infrastructure.Data.Models;
using MovieTheater.Services.DTOs;
using MovieTheater.Services.Interface;
using MovieTheater.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Service
{
    public class CategoryService: ICategoryService
    {
        private readonly MovieDbContext _context;
        private readonly IMapper mapper;

        public CategoryService(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAllCategory(PaginationDTO paginationDTO, HttpContext httpContext)
        {
            var queryable = _context.Categories.AsQueryable();
            await httpContext.PaginationQueryable(queryable);
            var categories = await queryable.OrderByDescending(x => x.Id).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryById(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            return mapper.Map<CategoryDTO>(category);
        }

        public async Task<Category> CreateCategory(CreateCategory createCategory)
        {
            var category = mapper.Map<Category>(createCategory);
            _context.Add(category);
            await _context.SaveChangesAsync();
            return mapper.Map<Category>(category);
        }
        public async Task<Category> UpdateCategory(int Id, CreateCategory updateCategory)
        {
            var category = mapper.Map<Category>(updateCategory);
            category.Id = Id;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return mapper.Map<Category>(category);
        }

        public async Task<CategoryDTO> DeleteCategory(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return mapper.Map<CategoryDTO>(category);
        }
    }
}
