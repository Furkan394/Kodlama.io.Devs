using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Auths.Dtos;
using Kodlama.io.Devs.Application.Features.Auths.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.Register
{
    public class RegisterCommand : IRequest<TokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, TokenDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;

            public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<TokenDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                User user = _mapper.Map<User>(request);
                user.Status = true;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _authBusinessRules.UserCanNotBeDuplicatedWhenInserted(request.Email);

                User createdUser = await _userRepository.AddAsync(user);

                var roles = await _operationClaimRepository.GetListAsync(o => o.UserOperationClaims.Any(u => u.UserId == createdUser.Id));
                AccessToken accessToken = _tokenHelper.CreateToken(user, roles.Items);

                TokenDto tokenDto = _mapper.Map<TokenDto>(accessToken);
                return tokenDto;
            }
        }
    }
}
