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

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.Login
{
    public class LoginCommand : IRequest<TokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;

            public LoginCommandHandler(IUserRepository userRepository, IOperationClaimRepository operationClaimRepository, IMapper mapper, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _authBusinessRules = authBusinessRules;
            }
            public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(u => u.Email == request.Email);

                _authBusinessRules.UserShouldExistWhenRequested(user!);

                var result = HashingHelper.VerifyPasswordHash(request.Password, user!.PasswordHash, user.PasswordSalt);
                var roles = await _operationClaimRepository.GetListAsync(o => o.UserOperationClaims.Any(u => u.UserId == user.Id));

                AccessToken accessToken = _tokenHelper.CreateToken(user, roles.Items);

                TokenDto tokenDto = _mapper.Map<TokenDto>(accessToken);

                return tokenDto;
            }
        }
    }
}
