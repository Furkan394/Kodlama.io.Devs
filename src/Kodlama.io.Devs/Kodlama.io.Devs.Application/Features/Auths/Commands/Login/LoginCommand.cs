using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Auths.Dtos;
using Kodlama.io.Devs.Application.Features.Auths.Rules;
using Kodlama.io.Devs.Application.Services.AuthService;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.Login
{
    public class LoginCommand : IRequest<LoggedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IAuthService _authService;

            public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, AuthBusinessRules authBusinessRules, IAuthService authService)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _authBusinessRules = authBusinessRules;
                _authService = authService;
            }
            public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);

                _authBusinessRules.MailMustExistWhenLoggingIn(request.UserForLoginDto.Email);
                _authBusinessRules.VerifyUserPasswordHash(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                LoggedDto loggedDto = new()
                {
                    AccessToken = createdAccessToken,
                    RefreshToken = addedRefreshToken,
                };

                return loggedDto;
            }
        }
    }
}
