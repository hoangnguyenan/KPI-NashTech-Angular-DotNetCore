using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Services.DTOs;
using MovieTheater.Services.Interface;

namespace MovieTheater.WebApi.Controllers
{
    [Route("api/actors")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]


    public class ActorController : ControllerBase
    {
        public readonly IActorService actorService;

        public ActorController(IActorService actorService)
        {
            this.actorService = actorService;
        }

        [HttpPost("searchByName")]
        public async Task<ActionResult<List<ActorMovieDTO>>> SearchByName([FromBody] string name)
        {
            try
            {
                var result = await actorService.SearchByName(name);
                if (result == null)
                {
                    return NoContent();
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

        // GET: api/Actor
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ActorDTO>>> GetActors([FromQuery] PaginationDTO paginationDTO)
        {
            try
            {
                var result = await actorService.GetAllActor(paginationDTO, HttpContext);
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

        // GET: api/Actor/5

        [HttpGet("{Id}")]
        public async Task<ActionResult<ActorDTO>> GetActor(int Id)
        {
            try
            {
                var result = await actorService.GetActorById(Id);
                if (Id != result.Id)
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

        // PUT: api/Actor/5
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutActor(int Id, [FromForm] CreateActor updateActor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await actorService.UpdateActor(Id, updateActor);
                if (result == null)
                {
                    return NotFound();
                }
                if (Id != result.Id)
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

        // POST: api/Actor
        [HttpPost]
        public async Task<ActionResult> PostActor([FromForm] CreateActor createActor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }   
                var result = await actorService.CreateActor(createActor);
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

        // DELETE: api/Actor/5
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteActor(int Id)
        {
            try
            {
                var result = await actorService.DeleteActor(Id);
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
