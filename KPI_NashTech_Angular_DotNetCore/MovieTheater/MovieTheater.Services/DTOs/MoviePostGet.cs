using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Services.DTOs
{
    public class MoviePostGet
    {
        public List<CategoryDTO> Categories { get; set; }
        public List<MovieTheaterDTO> MovieTheaters { get; set; }
    }
}
