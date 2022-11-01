using System;

namespace MovieTheater.Services.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Picture { get; set; }
        public string Story { get; set; }
    }
}
