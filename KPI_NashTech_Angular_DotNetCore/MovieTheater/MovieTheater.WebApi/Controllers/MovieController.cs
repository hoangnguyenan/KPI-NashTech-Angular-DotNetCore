using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Services.DTOs;
using MovieTheater.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.WebApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy ="Admin")]

    public class MovieController: ControllerBase
    {
        public readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<MovieDTO>>> FilterMovies([FromQuery] FilterMoviesDTO filterMoviesDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await movieService.FilterMovies(filterMoviesDTO, HttpContext);
                if (result.Count == 0)
                {
                    return result;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<HomeDTO>> GetHome([FromQuery] PaginationDTO paginationDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await movieService.GetAllMovies(paginationDTO, HttpContext);
                if (result == null)
                {
                    return BadRequest(result);
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

        [HttpGet("GetPost")]
        public async Task<ActionResult<MoviePostGet>> GetCinemaAndCategoryPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await movieService.GetCinemaAndCategoryPost();
                if (result == null)
                {
                    return BadRequest(result);
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

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await movieService.GetMovieById(id, HttpContext);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return result;
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostMovie([FromForm] CreateMovie createMovie)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await movieService.CreateMovie(createMovie);
                if (result == 0)
                {
                    return BadRequest(result);
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

        [HttpGet("getPut/{id:int}")]
        public async Task<ActionResult<MoviePutGet>> GetCinemaAndCategoryPut(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var getMovie = await GetMovie(id);
                if (getMovie.Result is NotFoundResult)
                {
                    return NotFound();
                }
                else
                {
                    var movie = getMovie.Value;
                    var result = await movieService.GetCinemaAndCategoryPut(movie);
                    if (result == null)
                    {
                        return BadRequest(result);
                    }
                    else
                    {
                        return Ok(result);
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutMovie(int id, [FromForm] CreateMovie createMovie)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await movieService.UpdateMovie(id, createMovie);
                if (result == 0)
                {
                    return NotFound(result);
                }
                if (id != result)
                {
                    return BadRequest();
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await movieService.DeleteMovie(id);
                if (result == null)
                {
                    return NotFound(result);
                }
                if (id != result.Id)
                {
                    return BadRequest();
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
