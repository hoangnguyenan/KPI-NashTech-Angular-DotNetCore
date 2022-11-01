using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _context;
        private readonly IMapper mapper;
        private readonly IStorageService storageService;
        private readonly string containerName = "movies";
        private readonly UserManager<IdentityUser> userManager;
        public MovieService(MovieDbContext context, IMapper mapper,
            IStorageService storageService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.mapper = mapper;
            this.storageService = storageService;
            this.userManager = userManager;
        }

        public async Task<List<MovieDTO>> FilterMovies(FilterMoviesDTO filterMoviesDTO, HttpContext httpContext)
        {
            var moviesQueryable = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(filterMoviesDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(x => x.Title.Contains(filterMoviesDTO.Title));
            }

            if (filterMoviesDTO.InCinemas)
            {
                moviesQueryable = moviesQueryable.Where(x => x.InCinemas);
            }

            if (filterMoviesDTO.UpComingReleases)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(x => x.ReleaseDate > today);
            }

            if (filterMoviesDTO.CategoryId != 0)
            {
                moviesQueryable = moviesQueryable
                    .Where(x => x.MovieCategories.Select(y => y.CategoryId)
                    .Contains(filterMoviesDTO.CategoryId));
            }
            await httpContext.PaginationQueryable(moviesQueryable);

            var movies = await moviesQueryable.OrderBy(x => x.Title).Paginate(filterMoviesDTO.PaginationDTO)
                .ToListAsync();
            return mapper.Map<List<MovieDTO>>(movies);
        }

        public async Task<HomeDTO> GetAllMovies(PaginationDTO paginationDTO, HttpContext httpContext)
        {
            var today = DateTime.Today;
            var queryableUpComing = _context.Movies.AsQueryable().Where(x => (x.ReleaseDate > today & x.InCinemas == false));
            var queryableInCinema = _context.Movies.AsQueryable().Where(x => (x.InCinemas || x.ReleaseDate <= today));
            await httpContext.PaginationQueryableForMovie(queryableUpComing, queryableInCinema);

            var upComingReleases = await _context.Movies
                .Where(x => (x.ReleaseDate > today & x.InCinemas == false))
                .OrderBy(x => x.ReleaseDate).Paginate(paginationDTO)
                .ToListAsync();

            var inCinemas = await _context.Movies
                .Where(x =>(x.InCinemas || x.ReleaseDate <= today))
                .OrderByDescending(x => x.ReleaseDate).Paginate(paginationDTO)
                .ToListAsync();

            var homeDTO = new HomeDTO();           
            homeDTO.UpComingReleases = mapper.Map<List<MovieDTO>>(upComingReleases);
            homeDTO.InCinemas = mapper.Map<List<MovieDTO>>(inCinemas);
            return homeDTO;
        }

        public async Task<MovieDTO> GetMovieById(int id, HttpContext httpContext)
        {
            var movie = await _context.Movies
                .Include(x => x.MovieCategories).ThenInclude(x => x.Category)
                .Include(x => x.MovieCinemas).ThenInclude(x => x.Cinema)
                .Include(x => x.MovieActors).ThenInclude(x => x.Actor)
                .FirstOrDefaultAsync(x => x.Id == id);          

            var movieMapper = mapper.Map<MovieDTO>(movie);
            if (movie != null)
            {
                var averageVote = 0.0;
                var userVote = 0;

                if (await _context.Ratings.AnyAsync(x => x.MovieId == id))
                {
                    averageVote = await _context.Ratings.Where(x => x.MovieId == id)
                        .AverageAsync(x => x.Rate);

                    averageVote = Math.Round(averageVote, 2);

                    if (httpContext.User.Identity.IsAuthenticated)
                    {
                        var email = httpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
                        var user = await userManager.FindByEmailAsync(email);
                        var userId = user.Id;

                        var ratingDb = await _context.Ratings.FirstOrDefaultAsync(x => x.MovieId == id
                        && x.UserId == userId);

                        if (ratingDb != null)
                        {
                            userVote = ratingDb.Rate;
                        }
                    }
                }

                movieMapper.AverageVote = averageVote;
                movieMapper.UserVote = userVote;
                movieMapper.Actors = movieMapper.Actors.OrderBy(x => x.Order).ToList();
            }
            return movieMapper;
        }
        public async Task<MoviePostGet> GetCinemaAndCategoryPost()
        {
            var cinemas = await _context.Cinemas.OrderBy(x => x.Name).ToListAsync();
            var categories = await _context.Categories.OrderBy(x => x.Name).ToListAsync();

            var movieTheaterDTO = mapper.Map<List<MovieTheaterDTO>>(cinemas);
            var categoriesDTO = mapper.Map<List<CategoryDTO>>(categories);

            return new MoviePostGet() { Categories = categoriesDTO, MovieTheaters = movieTheaterDTO };
        }
        public async Task<int> CreateMovie(CreateMovie createMovie)
        {
            var movie = mapper.Map<Movie>(createMovie);

            if (createMovie.Poster != null)
            {
                movie.Poster = await storageService.SaveFile(containerName, createMovie.Poster);
            }

            ActorsOrder(movie);
            _context.Add(movie);
            await _context.SaveChangesAsync();

            return movie.Id;
        }
        public async Task<MoviePutGet> GetCinemaAndCategoryPut(MovieDTO movie)
        {
            var categorySelectedIds = movie.Categories.Select(x => x.Id).ToList();
            var nonSelectedCategories = await _context.Categories.Where(x => !categorySelectedIds.Contains(x.Id))
                .ToListAsync();

            var movieTheatersIds = movie.MovieTheaters.Select(x => x.Id).ToList();
            var nonSelectedMovieTheaters = await _context.Cinemas.Where(x =>
            !movieTheatersIds.Contains(x.Id)).ToListAsync();

            var nonSelectedCategoryDTO = mapper.Map<List<CategoryDTO>>(nonSelectedCategories);
            var nonSelectedMovieTheaterDTO = mapper.Map<List<MovieTheaterDTO>>(nonSelectedMovieTheaters);

            var response = new MoviePutGet();
            response.Movie = movie;
            response.SelectedCategories = movie.Categories;
            response.NonSelectedCategories = nonSelectedCategoryDTO;
            response.SelectedMovieTheaters = movie.MovieTheaters;
            response.NonSelectedMovieTheaters = nonSelectedMovieTheaterDTO;
            response.Actors = movie.Actors;

            return response;
        }
        public async Task<int> UpdateMovie(int id, CreateMovie createMovie)
        {
            var movie = await _context.Movies.Include(x => x.MovieActors)
                .Include(x => x.MovieCategories)
                .Include(x => x.MovieCinemas)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null)
            {
                return 0;
            }
            else { 
                movie = mapper.Map(createMovie, movie);

                if (createMovie.Poster != null)
                {
                    movie.Poster = await storageService.EditFile(containerName, movie.Poster, createMovie.Poster);
                }

                ActorsOrder(movie);
                await _context.SaveChangesAsync();            
                return movie.Id; 
            }
        }

        public async Task<MovieDTO> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
            {
                return mapper.Map<MovieDTO>(movie);
            }
            _context.Remove(movie);
            await _context.SaveChangesAsync();
            await storageService.DeleteFile(containerName, movie.Poster);

            return mapper.Map<MovieDTO>(movie);
        }

        private void ActorsOrder(Movie movie)
        {
            if (movie.MovieActors != null)
            {
                for (int i = 0; i < movie.MovieActors.Count; i++)
                {
                    movie.MovieActors[i].Order = i;
                }
            }
        }
    }
}
