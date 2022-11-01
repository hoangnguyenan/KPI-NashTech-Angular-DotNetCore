using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTheater.Services.DTOs;
using MovieTheater.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MovieTheater.WebApi.Controllers
{
    [Route("api/movietheaters")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]

    public class MovieTheaterController : ControllerBase
    {
        public readonly IMovieTheaterService movieTheaterService;
        private readonly ILogger<MovieTheaterController> logger;


        public MovieTheaterController(IMovieTheaterService movieTheaterService, ILogger<MovieTheaterController> logger)
        {
            this.movieTheaterService = movieTheaterService;
            this.logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<MovieTheaterDTO>>> GetMovieTheaters([FromQuery] PaginationDTO paginationDTO)
        {
            try
            {
                var result = await movieTheaterService.GetAllMovieTheater(paginationDTO, HttpContext);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    logger.LogInformation("Get all movie theater");
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
        public async Task<ActionResult<MovieTheaterDTO>> GetMovieTheater(int Id)
        {
            try
            {
                var result = await movieTheaterService.GetMovieTheaterById(Id);
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
        public async Task<ActionResult> PostMovieTheater(CreateMovieTheater createMovieTheater)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await movieTheaterService.CreateMovieTheater(createMovieTheater);
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
        public async Task<ActionResult> PutMovieTheater(int Id, CreateMovieTheater updateMovieTheater)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await movieTheaterService.UpdateMovieTheater(Id, updateMovieTheater);
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
        public async Task<ActionResult> DeleteMovieTheater(int Id)
        {
            try
            {
                var result = await movieTheaterService.DeleteMovieTheater(Id);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
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
