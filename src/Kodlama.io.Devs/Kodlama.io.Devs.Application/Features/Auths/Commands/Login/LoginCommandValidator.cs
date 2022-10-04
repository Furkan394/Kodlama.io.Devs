using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(u => u.UserForLoginDto.Email).NotEmpty();
            RuleFor(u => u.UserForLoginDto.Email).NotNull();
            RuleFor(u => u.UserForLoginDto.Email).EmailAddress();

            RuleFor(u => u.UserForLoginDto.Password).NotEmpty();
            RuleFor(u => u.UserForLoginDto.Password).NotNull();
        }
    }
}
