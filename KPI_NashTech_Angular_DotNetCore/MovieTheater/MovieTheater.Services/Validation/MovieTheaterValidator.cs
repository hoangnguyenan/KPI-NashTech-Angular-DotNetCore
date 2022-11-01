using FluentValidation;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Validation
{
    public class MovieTheaterValidator: AbstractValidator<CreateMovieTheater>
    {
        [Obsolete]
        public MovieTheaterValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)// stop dieu kien thu >=2 neu dien kien 1 chua thoa man
                .NotEmpty().WithMessage("{Name} is required.")
                .NotNull()
                .Must(IsValidName).WithMessage("{Name} contains invalid characters")
                .Must(firstLetterUpercase).WithMessage("First letter should be uppercase")
                .MaximumLength(50).WithMessage("{Name} must not exceed 50 characters.")
                .MinimumLength(2).WithMessage("{Name} must at least 2 characters.");

            RuleFor(p => p.Address)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotEmpty().WithMessage("{Address} is required.")
               .Must(firstLetterUpercase).WithMessage("First letter should be uppercase")
               .MaximumLength(100).WithMessage("{Address} must not exceed 50 characters.")
               .MinimumLength(2).WithMessage("{Address} must at least 2 characters.");

            RuleFor(p => p.Latitude)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(-90).WithMessage("Latitude can be greater than -90")
                .LessThanOrEqualTo(90).WithMessage("Latitude can be less than 90");            

            RuleFor(p => p.Longitude)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(-180).WithMessage("Longitude can be greater than -180")
                .LessThanOrEqualTo(180).WithMessage("Longitude can be less than 180");
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
        protected bool IsValidName(string name)
        {
            name = name.Replace(" ", "");
            return name.All(char.IsLetter);
        }
        
    }
}
