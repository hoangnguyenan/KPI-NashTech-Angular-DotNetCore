using FluentValidation;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Validation
{
    public class RatingValidator : AbstractValidator<RatingDTO>
    {
        [Obsolete]
        public RatingValidator()
        {
            RuleFor(p => p.Rate)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);
        }
    }
}
