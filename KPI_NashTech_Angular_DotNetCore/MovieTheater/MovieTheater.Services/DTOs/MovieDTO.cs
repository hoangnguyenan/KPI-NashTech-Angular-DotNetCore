using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public double AverageVote { get; set; }
        public int UserVote { get; set; }
        
        public List<CategoryDTO> Categories { get; set; }
        public List<MovieTheaterDTO> MovieTheaters { get; set; }
        public List<ActorMovieDTO> Actors { get; set; }
    }
}
