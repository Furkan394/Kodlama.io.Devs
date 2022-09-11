using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserCanNotBeDuplicatedWhenInserted(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(u => u.Email == email);
            if (result.Items.Any()) throw new BusinessException("User email exists.");
        }

        public void UserShouldExistWhenRequested(User user)
        {
            if (user == null) throw new BusinessException("Requested user does not exist.");
        }

        public void VerifyUserPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            if (!result) throw new BusinessException("Password is not correct.");
        }
    }
}
