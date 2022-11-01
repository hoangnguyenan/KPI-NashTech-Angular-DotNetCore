using Microsoft.AspNetCore.Http;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Interface
{
    public interface IRatingService
    {
        Task<RatingDTO> PostRating(RatingDTO ratingDTO, HttpContext httpContext);
    }
}
