using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Infrastructure.Data.ApplicationDbContext;
using MovieTheater.Infrastructure.Data.Models;
using MovieTheater.Services.DTOs;
using MovieTheater.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Service
{
    public class RatingService: IRatingService
    {
        private readonly MovieDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public RatingService(MovieDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }


        public async Task<RatingDTO> PostRating(RatingDTO ratingDTO, HttpContext httpContext)
        {
            var username = httpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var user = await userManager.FindByNameAsync(username);
            var userId = user.Id;

            var currentRate = await _context.Ratings
                .FirstOrDefaultAsync(x => x.MovieId == ratingDTO.MovieId &&
                x.UserId == userId);

            if (currentRate == null)
            {
                var rating = new Rating();
                rating.MovieId = ratingDTO.MovieId;
                rating.Rate = ratingDTO.Rate;
                rating.UserId = userId;
                _context.Add(rating);
            }
            else
            {
                currentRate.Rate = ratingDTO.Rate;
            }

            await _context.SaveChangesAsync();

            return ratingDTO;
        }
    }
}
