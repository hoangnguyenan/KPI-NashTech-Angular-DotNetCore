using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieTheater.Infrastructure.Data.ApplicationDbContext;
using MovieTheater.Services.DTOs;
using MovieTheater.Services.Interface;
using MovieTheater.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Service
{
    public class AccountService : IAccountService
    {
        private readonly MovieDbContext _context;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountService(MovieDbContext context,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }        

        public async Task<AuthenticationResponse> CreateAccount(UserCredentials userCredentials)
        {
            var user = new IdentityUser { Email = userCredentials.Email, UserName = userCredentials.Username  };
            await userManager.SetPhoneNumberAsync(user, "User");
            var result = await userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                var getError = result.Errors.ToList()[0].Code;
                if (getError == "DuplicateUserName")
                {
                    return new AuthenticationResponse()
                    {
                        Token = ""
                    };
                }
                else {                 
                    return new AuthenticationResponse()
                    {
                        Token = null
                    };
                }
            }
        }

        public async Task<AuthenticationResponse> Login(UserLogin userLogin)
        {
            var user = await userManager.FindByNameAsync(userLogin.Username);
            if (user != null)
            {
                var userCredential = new UserCredentials();
                userCredential.Email = user.Email;
                userCredential.Username = user.UserName;
                userCredential.Password = user.PasswordHash;

                var result = await signInManager.PasswordSignInAsync(userLogin.Username,
                    userLogin.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return await BuildToken(userCredential);
                }
                else
                {
                    return new AuthenticationResponse()
                    {
                        Token = null
                    };
                };
            }
            else
            {
                return new AuthenticationResponse()
                {
                    Token = null
                };
            }

        }

        public async Task<List<UserDTO>> GetAllUsers(PaginationDTO paginationDTO, HttpContext httpContext)
        {
            var queryable = _context.Users.AsQueryable();
            await httpContext.PaginationQueryable(queryable);          

            var username = httpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var user = await userManager.FindByNameAsync(username);
            var userId = user.Id;

            var users = await queryable.OrderBy(x => x.Email).Where(x => x.Id != userId).Paginate(paginationDTO).ToListAsync();

            return mapper.Map<List<UserDTO>>(users);
        }

        public async Task<int> MakeAdmin(string userId, HttpContext httpContext)
        {
            var user = await userManager.FindByIdAsync(userId);
            var claimsDB = await userManager.GetClaimsAsync(user);
            if (claimsDB.Count > 0)
            {
                var role = claimsDB.ToList()[0].Value;
                if (role == "admin")
                {
                    var username = httpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
                    var currentUser = await userManager.FindByNameAsync(username);
                    if (user != currentUser) {
                        return 3;
                    }

                    return 0;
                }
                else
                {
                    if (role == "manager")
                    {
                        return 2;
                    }
                    else { 
                        await userManager.AddClaimAsync(user, new Claim("role", "manager"));
                        await userManager.SetPhoneNumberAsync(user, "Manager");
                        return 1;                    
                    }

                }
            }
            else {
                await userManager.AddClaimAsync(user, new Claim("role", "manager"));
                await userManager.SetPhoneNumberAsync(user, "Manager");

                return 1;
            }
        }
        public async Task<bool> AbortAdmin(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var claimsDB = await userManager.GetClaimsAsync(user);
            if (claimsDB.Count > 0)
            {
                var role = claimsDB.ToList()[0].Value;
                if (role == "admin")
                {
                    return false;
                }
                else
                {
                    await userManager.RemoveClaimAsync(user, new Claim("role", "manager"));
                    await userManager.SetPhoneNumberAsync(user, "User");

                    return true;
                }
            }
            else
            {
                await userManager.RemoveClaimAsync(user, new Claim("role", "manager"));
                await userManager.SetPhoneNumberAsync(user, "User");

                return true;
            }
        }

        private async Task<AuthenticationResponse> BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email),
                new Claim("username", userCredentials.Username)
            };

            var user = await userManager.FindByNameAsync(userCredentials.Username);
            var claimsDB = await userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("keyjwt")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
