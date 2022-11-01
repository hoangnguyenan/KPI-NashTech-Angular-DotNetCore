using FluentValidation;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Validation
{
    public class AccountValidator: AbstractValidator<UserCredentials>
    {
        [Obsolete]
        public AccountValidator() {
            RuleFor(p => p.Username)
               .Cascade(CascadeMode.StopOnFirstFailure)// stop dieu kien thu >=2 neu dien kien 1 chua thoa man
               .NotEmpty().WithMessage("{User name} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{User name} must not exceed 50 characters.");
            RuleFor(p => p.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)// stop dieu kien thu >=2 neu dien kien 1 chua thoa man
                .NotEmpty().WithMessage("{Email} is required.")
                .NotNull()
                .EmailAddress()
                .MaximumLength(50).WithMessage("{Email} must not exceed 50 characters.");
            RuleFor(p => p.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{Password} is required.")
                .NotNull()
                .Must(firstLetterUpercase).WithMessage("Passwords must have at least one uppercase ('A'-'Z').")
                .Must(CheckNonAlphanumericCharacter).WithMessage("{Password} must have at least one non alphanumeric character.")
                .MaximumLength(50).WithMessage("{Password} must not exceed 50 characters.")
                .MinimumLength(8).WithMessage("{Password} must be less than 8 character.");
        }

        protected bool CheckNonAlphanumericCharacter(string password)
        {
            var check = password.ToString();
            if (check.All(char.IsLetterOrDigit))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected bool firstLetterUpercase(string password)
        {
            var firstLetter = password.ToString()[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                return false;
            }
            return true;
        }
    }


}
