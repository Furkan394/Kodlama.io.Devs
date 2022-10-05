using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task UserOperationClaimCanNotBeDuplicatedWhenInserted(int operationClaimId)
        {
            UserOperationClaim? userUperationClaim = await _userOperationClaimRepository.GetAsync(u => u.OperationClaimId == operationClaimId);
            if (userUperationClaim != null) throw new BusinessException("User operation claim exists.");
        }

        public void UserOperationClaimShouldExistWhenRequested(UserOperationClaim userOperationClaim)
        {
            if (userOperationClaim == null) throw new BusinessException("Requested user operation claim does not exist.");
        }
    }
}
