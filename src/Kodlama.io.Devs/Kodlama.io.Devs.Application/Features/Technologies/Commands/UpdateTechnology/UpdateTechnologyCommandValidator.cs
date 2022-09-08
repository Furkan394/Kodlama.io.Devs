using FluentValidation;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommandValidator : AbstractValidator<Technology>
    {
        public UpdateTechnologyCommandValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
        }
    }
}
