using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Services.DTOs;
using MovieTheater.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.WebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers([FromQuery] PaginationDTO paginationDTO)
        {
            try
            {
                var result = await accountService.GetAllUsers(paginationDTO, HttpContext);
                if (result.Count == 0)
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

        [HttpPost("makeAdmin")]
        public async Task<ActionResult> MakeAdmin([FromBody] string userId)
        {
            try
            {
                var result = await accountService.MakeAdmin(userId, HttpContext);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }

        [HttpPost("removeAdmin")]
        public async Task<ActionResult> RemoveAdmin([FromBody] string userId)
        {
            try
            {
                var result = await accountService.AbortAdmin(userId);
                if (result == false)
                {
                    return Ok(result);
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

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> Register([FromBody] UserCredentials userCredentials)
        {
            try
            {
                var result = await accountService.CreateAccount(userCredentials);
                if (result.Token == null)
                {
                    return BadRequest(result);
                }
                if(result.Token == string.Empty)
                {
                    return BadRequest("Username is aldready existed. Please try again.");
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var result = await accountService.Login(userLogin);
                if (string.IsNullOrEmpty(result.Token))
                {
                    return BadRequest("Password or Username are not correct");
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
