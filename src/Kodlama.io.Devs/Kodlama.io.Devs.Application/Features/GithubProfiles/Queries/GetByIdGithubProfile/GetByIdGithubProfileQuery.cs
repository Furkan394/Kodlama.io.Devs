using AutoMapper;
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

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Queries.GetByIdGithubProfile
{
    public class GetByIdGithubProfileQuery : IRequest<GithubProfileGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdGithubProfileQueryHandler : IRequestHandler<GetByIdGithubProfileQuery, GithubProfileGetByIdDto>
        {
            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly IMapper _mapper;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

            public GetByIdGithubProfileQueryHandler(IGithubProfileRepository githubProfileRepository, IMapper mapper, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _githubProfileRepository = githubProfileRepository;
                _mapper = mapper;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }

            public async Task<GithubProfileGetByIdDto> Handle(GetByIdGithubProfileQuery request, CancellationToken cancellationToken)
            {
                GithubProfile? githubProfile = await _githubProfileRepository.GetAsync(g => g.Id == request.Id);

                _githubProfileBusinessRules.GithubProfileShouldExistWhenRequested(githubProfile!);

                GithubProfileGetByIdDto githubProfileGetByIdDto = _mapper.Map<GithubProfileGetByIdDto>(githubProfile);

                return githubProfileGetByIdDto;
            }
        }
    }
}
