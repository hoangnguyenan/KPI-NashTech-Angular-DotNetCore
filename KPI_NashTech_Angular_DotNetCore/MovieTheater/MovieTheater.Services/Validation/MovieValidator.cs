using FluentValidation;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Validation
{
    public class MovieValidator: AbstractValidator<CreateMovie>
    {
        [Obsolete]
        public MovieValidator()
        {
            RuleFor(p => p.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)// stop dieu kien thu >=2 neu dien kien 1 chua thoa man
                .NotEmpty().WithMessage("{Title} is required.")
                .NotNull()
                .Must(firstLetterUpercase).WithMessage("First letter should be uppercase")
                .MaximumLength(100).WithMessage("{Title} must not exceed 100 characters.")
                .MinimumLength(2).WithMessage("{Title} must at least 2 characters.");

            RuleFor(p => p.Summary)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotEmpty().WithMessage("{Summary} is required.")
               .Must(firstLetterUpercase).WithMessage("First letter should be uppercase")
               .MaximumLength(1000).WithMessage("{Summary} must not exceed 1000 characters.")
               .MinimumLength(2).WithMessage("{Summary} must at least 2 characters.");

            RuleFor(p => p.Trailer)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .MaximumLength(1000).WithMessage("{Trailer} must not exceed 1000 characters.")
               .MinimumLength(2).WithMessage("{Trailer} must at least 2 characters.");
                
        }
        protected bool firstLetterUpercase(string name)
        {
            var firstLetter = name.ToString()[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                return false;
            }
            return true;
        }
    }
}
