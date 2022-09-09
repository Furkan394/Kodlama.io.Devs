using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Queries.GetListGithubProfile
{
    public class GetListGithubProfileQuery : IRequest<GithubProfileListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListGithubProfileQueryHandler : IRequestHandler<GetListGithubProfileQuery, GithubProfileListModel>
        {
            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly IMapper _mapper;

            public GetListGithubProfileQueryHandler(IGithubProfileRepository githubProfileRepository, IMapper mapper)
            {
                _githubProfileRepository = githubProfileRepository;
                _mapper = mapper;
            }

            public async Task<GithubProfileListModel> Handle(GetListGithubProfileQuery request, CancellationToken cancellationToken)
            {
                IPaginate<GithubProfile> githubProfiles = await _githubProfileRepository.GetListAsync(
                                                                                       index: request.PageRequest.Page,
                                                                                       size: request.PageRequest.PageSize);

                GithubProfileListModel githubProfileListModel = _mapper.Map<GithubProfileListModel>(githubProfiles);

                return githubProfileListModel;  

            }
        }
    }
}
