using FluentValidation;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieTheater.Services.Validation
{
    public class ActorValidator : AbstractValidator<CreateActor>
    {
        [Obsolete]
        public ActorValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)// stop dieu kien thu >=2 neu dien kien 1 chua thoa man
                .NotEmpty().WithMessage("{Name} is required.")
                .NotNull()
                .Must(IsValidName).WithMessage("{Name} contains invalid characters")
                .Must(firstLetterUpercase).WithMessage("First letter should be uppercase")
                .MaximumLength(50).WithMessage("{Name} must not exceed 50 characters.")
                .MinimumLength(2).WithMessage("{Name} must at least 2 characters.");
            RuleFor(p => p.Dob)
                .NotEmpty().WithMessage("{Dob} is required.")
                .NotNull();
            RuleFor(p => p.Story)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{Story} is required.")
                .NotNull()
                .MaximumLength(1000).WithMessage("{Story} must not exceed 1000 characters.");
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
