using MovieTheater.Services.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Services.DTOs
{
    public class CreateCategory
    {        
        public string Name { get; set; }
    }
}
