using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Rules
{
    public class GithubProfileBusinessRules
    {
        private readonly IGithubProfileRepository _githubProfileRepository;

        public GithubProfileBusinessRules(IGithubProfileRepository githubProfileRepository)
        {
            _githubProfileRepository = githubProfileRepository;
        }

        public async Task GithubProfileCanNotBeDuplicatedWhenInserted(string githubUrl)
        {
            IPaginate<GithubProfile> result = await _githubProfileRepository.GetListAsync(g => g.GithubUrl == githubUrl);
            if (result.Items.Any()) throw new BusinessException("Github profile exists.");
        }

        public void GithubProfileShouldExistWhenRequested(GithubProfile githubProfile)
        {
            if (githubProfile == null) throw new BusinessException("Requested github profile does not exist.");
        }
    }
}
