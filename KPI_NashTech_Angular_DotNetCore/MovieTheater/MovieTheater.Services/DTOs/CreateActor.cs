using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Services.DTOs
{
    public class CreateActor
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public IFormFile Picture { get; set; }
        public string Story { get; set; }
    }
}
