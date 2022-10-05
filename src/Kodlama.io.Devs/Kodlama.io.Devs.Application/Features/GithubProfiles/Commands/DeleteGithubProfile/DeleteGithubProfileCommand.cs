using Core.Application.Pipelines.Authorization;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.DeleteGithubProfile
{
    public class DeleteGithubProfileCommand : IRequest, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles { get; } = new string[1] { "user" };

        public class DeleteGithubProfileCommandHandler : IRequestHandler<DeleteGithubProfileCommand>
        {
            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

            public DeleteGithubProfileCommandHandler(IGithubProfileRepository githubProfileRepository, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _githubProfileRepository = githubProfileRepository;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }

            public async Task<Unit> Handle(DeleteGithubProfileCommand request, CancellationToken cancellationToken)
            {
                GithubProfile? githubProfile = await _githubProfileRepository.GetAsync(g => g.Id == request.Id);

                _githubProfileBusinessRules.GithubProfileShouldExistWhenRequested(githubProfile!);

                await _githubProfileRepository.DeleteAsync(githubProfile!);

                return Unit.Value;
            }
        }
    }
}
