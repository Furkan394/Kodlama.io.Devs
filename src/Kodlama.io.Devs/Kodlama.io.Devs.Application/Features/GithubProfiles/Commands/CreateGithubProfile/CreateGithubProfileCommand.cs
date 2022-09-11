using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.CreateGithubProfile
{
    public class CreateGithubProfileCommand : IRequest<CreatedGithubProfileDto>, ISecuredRequest
    {
        public int UserId { get; set; }
        public string? GithubUrl { get; set; }

        public string[] Roles { get; } = { "user" };

        public class CreateGithubProfileCommandHandler : IRequestHandler<CreateGithubProfileCommand, CreatedGithubProfileDto>
        {
            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly IMapper _mapper;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

            public CreateGithubProfileCommandHandler(IGithubProfileRepository githubProfileRepository, IMapper mapper, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _githubProfileRepository = githubProfileRepository;
                _mapper = mapper;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }

            public async Task<CreatedGithubProfileDto> Handle(CreateGithubProfileCommand request, CancellationToken cancellationToken)
            {
                await _githubProfileBusinessRules.GithubProfileCanNotBeDuplicatedWhenInserted(request.GithubUrl!);

                GithubProfile githubProfile = _mapper.Map<GithubProfile>(request);

                GithubProfile createdGithubProfile = await _githubProfileRepository.AddAsync(githubProfile);

                CreatedGithubProfileDto createdGithubProfileDto = _mapper.Map<CreatedGithubProfileDto>(createdGithubProfile);

                return createdGithubProfileDto;
            }
        }
    }
}
