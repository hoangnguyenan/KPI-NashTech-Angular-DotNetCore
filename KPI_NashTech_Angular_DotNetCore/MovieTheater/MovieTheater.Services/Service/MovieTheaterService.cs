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
    public class MovieTheaterService: IMovieTheaterService
    {
        private readonly MovieDbContext _context;
        private readonly IMapper mapper;

        public MovieTheaterService(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }


        public async Task<List<MovieTheaterDTO>> GetAllMovieTheater(PaginationDTO paginationDTO, HttpContext httpContext)
        {           
            var queryable = _context.Cinemas.AsQueryable();
            await httpContext.PaginationQueryable(queryable);
            var movies = await queryable.OrderByDescending(x => x.Id).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<MovieTheaterDTO>>(movies);
        }
        public async Task<MovieTheaterDTO> GetMovieTheaterById(int Id)
        {
            var movies = await _context.Cinemas.FirstOrDefaultAsync(x => x.Id == Id);
            return mapper.Map<MovieTheaterDTO>(movies);
        }
        public async Task<Cinema> CreateMovieTheater(CreateMovieTheater createMovieTheater)
        {
            var movies = mapper.Map<Cinema>(createMovieTheater);
            _context.Add(movies);
            await _context.SaveChangesAsync();
            return mapper.Map<Cinema>(movies);
        }
        public async Task<Cinema> UpdateMovieTheater(int Id, CreateMovieTheater updateMovieTheater)
        {
            var movies = await _context.Cinemas.FirstOrDefaultAsync(x => x.Id == Id);
            movies = mapper.Map(updateMovieTheater, movies);
            _context.Entry(movies).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return mapper.Map<Cinema>(movies);
        }
        public async Task<MovieTheaterDTO> DeleteMovieTheater(int Id)
        {
            var movies = await _context.Cinemas.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Cinemas.Remove(movies);
            await _context.SaveChangesAsync();
            return mapper.Map<MovieTheaterDTO>(movies);
        }
    }
}
