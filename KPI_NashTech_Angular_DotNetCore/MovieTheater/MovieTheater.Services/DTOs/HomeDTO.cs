using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.DTOs
{
    public class HomeDTO
    {
        public List<MovieDTO> InCinemas { get; set; }
        public List<MovieDTO> UpComingReleases { get; set; }
    }
}
