using FluentValidation;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Validation
{
    public class CategoryValidator : AbstractValidator<CreateCategory>
    {
        [Obsolete]
        public CategoryValidator()
        {
           RuleFor(p => p.Name)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty().WithMessage("{Name} is required.")
          .NotNull()
          .Must(IsValidName).WithMessage("{Name} contains invalid characters")
          .Must(firstLetterUpercase).WithMessage("First letter should be uppercase")
          .MaximumLength(256).WithMessage("{Name} must not exceed 256 characters.")
          .MinimumLength(2).WithMessage("{Name} must at least 2 characters.");
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
