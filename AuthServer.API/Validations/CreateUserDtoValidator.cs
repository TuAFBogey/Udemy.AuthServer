using AuthServer.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API.Validations
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is Required").EmailAddress().WithMessage("Email is wrong");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is Required");
        }
    }
}
