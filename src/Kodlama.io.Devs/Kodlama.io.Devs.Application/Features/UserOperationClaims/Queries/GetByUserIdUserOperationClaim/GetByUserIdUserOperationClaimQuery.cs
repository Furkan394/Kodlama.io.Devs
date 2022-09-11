using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Dtos;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Queries.GetByUserIdUserOperationClaim
{
    public class GetByUserIdUserOperationClaimQuery : IRequest<UserOperationClaimGetByUserIdDto>
    {
        public int UserId { get; set; }

        public class GetByUserIdUserOperationClaimQueryHandler : IRequestHandler<GetByUserIdUserOperationClaimQuery, UserOperationClaimGetByUserIdDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public GetByUserIdUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<UserOperationClaimGetByUserIdDto> Handle(GetByUserIdUserOperationClaimQuery request, CancellationToken cancellationToken)
            { 
                UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(o => o.UserId == request.UserId);

                UserOperationClaimGetByUserIdDto userOperationClaimGetByUserIdDto = _mapper.Map<UserOperationClaimGetByUserIdDto>(userOperationClaim);

                return userOperationClaimGetByUserIdDto;
            }
        }
    }
}
