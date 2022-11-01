using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Interface
{
    public interface IMovieService
    {
        Task<List<MovieDTO>> FilterMovies(FilterMoviesDTO filterMoviesDTO, HttpContext httpContext);
        Task<HomeDTO> GetAllMovies(PaginationDTO paginationDTO, HttpContext httpContext);       
        Task<MovieDTO> GetMovieById(int id, HttpContext httpContext);
        Task<MoviePostGet> GetCinemaAndCategoryPost();
        Task<int> CreateMovie(CreateMovie createMovie);
        Task<MoviePutGet> GetCinemaAndCategoryPut(MovieDTO movie);
        Task<int> UpdateMovie(int id, CreateMovie createMovie);
        Task<MovieDTO> DeleteMovie(int id);
    }
}
