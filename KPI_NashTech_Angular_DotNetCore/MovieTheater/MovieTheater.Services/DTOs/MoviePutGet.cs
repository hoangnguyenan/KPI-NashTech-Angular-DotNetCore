using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.DTOs
{
    public class MoviePutGet
    {
        public MovieDTO Movie { get; set; }
        public List<CategoryDTO> SelectedCategories { get; set; }
        public List<CategoryDTO> NonSelectedCategories { get; set; }
        public List<MovieTheaterDTO> SelectedMovieTheaters { get; set; }
        public List<MovieTheaterDTO> NonSelectedMovieTheaters { get; set; }
        public List<ActorMovieDTO> Actors { get; set; }
    }
}
