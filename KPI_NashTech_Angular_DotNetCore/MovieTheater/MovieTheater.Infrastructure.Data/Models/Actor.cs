﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Infrastructure.Data.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Picture { get; set; }
        public string Story { get; set; }

    }
}
