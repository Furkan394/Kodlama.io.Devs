using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Auths.Commands.Register;
using Kodlama.io.Devs.Application.Features.Auths.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<AccessToken, TokenDto>().ReverseMap();
        }
    }
}
