using Microsoft.AspNetCore.Http;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Interface
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> CreateAccount(UserCredentials userCredentials);
        Task<AuthenticationResponse> Login(UserLogin userLogin);
        Task<List<UserDTO>> GetAllUsers(PaginationDTO paginationDTO, HttpContext httpContext);
        Task<int> MakeAdmin(string userId, HttpContext httpContext);
        Task<bool> AbortAdmin(string userId);
    }
}
