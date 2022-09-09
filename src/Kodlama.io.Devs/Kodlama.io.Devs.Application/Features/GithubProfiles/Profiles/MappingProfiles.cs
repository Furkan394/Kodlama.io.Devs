using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.CreateGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.UpdateGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Models;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<GithubProfile, CreateGithubProfileCommand>().ReverseMap();
            CreateMap<GithubProfile, CreatedGithubProfileDto>().ReverseMap();
            CreateMap<GithubProfile, UpdateGithubProfileCommand>().ReverseMap();
            CreateMap<GithubProfile, UpdatedGithubProfileDto>().ReverseMap();
            CreateMap<IPaginate<GithubProfile>, GithubProfileListModel>().ReverseMap();
            CreateMap<GithubProfile, GithubProfileListDto>().ReverseMap();
            CreateMap<GithubProfile, GithubProfileGetByIdDto>().ReverseMap();
        }
    }
}
