using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTheater.Services.DTOs;
using MovieTheater.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]

    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> logger;
        private readonly ICategoryService categoryService;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            this.logger = logger;
            this.categoryService = categoryService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories([FromQuery] PaginationDTO paginationDTO)
        {
            try
            {
                var result = await categoryService.GetAllCategory(paginationDTO, HttpContext);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    logger.LogInformation("Get all categories");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int Id)
        {
            try
            {
                var result = await categoryService.GetCategoryById(Id);
                if (Id != result.Id)
                {
                    return NotFound();
                }
                if (result == null)
                {
                    return BadRequest();
                }
                else
                {
                    logger.LogInformation($"Get ${result.Id} successfully");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostCategory([FromBody] CreateCategory createCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await categoryService.CreateCategory(createCategory);                
                if (result == null)
                {
                    return BadRequest();
                }
                else
                {
                    logger.LogInformation($"Create ${result.Id} successfully");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> PutCategory(int Id, [FromBody] CreateCategory updateCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await categoryService.UpdateCategory(Id, updateCategory);
                if (Id != result.Id)
                {
                    return NotFound();
                }
                if (result == null)
                {
                    return BadRequest();
                }
                else
                {
                    logger.LogInformation($"Update ${result.Id} successfully");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCategory(int Id)
        {
            try
            {
                var result = await categoryService.DeleteCategory(Id);                
                if (result != null)
                {
                    logger.LogInformation("Delete successfully");
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }
    }
}
