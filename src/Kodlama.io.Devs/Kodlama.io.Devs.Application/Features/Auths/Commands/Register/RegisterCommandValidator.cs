using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(u => u.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.FirstName).NotNull();
            RuleFor(u => u.UserForRegisterDto.FirstName).MinimumLength(3);

            RuleFor(u => u.UserForRegisterDto.LastName).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.LastName).NotNull();
            RuleFor(u => u.UserForRegisterDto.LastName).MinimumLength(3);

            RuleFor(u => u.UserForRegisterDto.Email).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.Email).NotNull();
            RuleFor(u => u.UserForRegisterDto.Email).EmailAddress();

            RuleFor(u => u.UserForRegisterDto.Password).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.Password).NotNull();
        }
    }
}
