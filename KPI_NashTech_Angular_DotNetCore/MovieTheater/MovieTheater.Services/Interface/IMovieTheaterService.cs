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
    public interface IMovieTheaterService
    {
        Task<List<MovieTheaterDTO>> GetAllMovieTheater(PaginationDTO paginationDTO, HttpContext httpContext);
        Task<MovieTheaterDTO> GetMovieTheaterById(int Id);
        Task<Cinema> CreateMovieTheater(CreateMovieTheater createMovieTheater);
        Task<Cinema> UpdateMovieTheater(int Id, CreateMovieTheater updateMovieTheater);
        Task<MovieTheaterDTO> DeleteMovieTheater(int Id);
    }
}
