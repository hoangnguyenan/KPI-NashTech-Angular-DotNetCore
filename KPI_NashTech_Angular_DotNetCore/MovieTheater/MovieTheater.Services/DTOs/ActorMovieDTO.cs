using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Services.DTOs
{
    public class ActorMovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }
    }
}
