﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TopCourseWorkBl.AuthenticationLayer.Exceptions;
using TopCourseWorkBl.DataLayer;
using TopCourseWorkBl.DataLayer.Cmds.Auth;

namespace TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.RevokeAuthentication
{
    public class RevokeAuthenticationHandler : IRequestHandler<RevokeAuthenticationCommand, EmptyResult>
    {
        private readonly AuthRepository _repository;
        private readonly IMapper _mapper;

        public RevokeAuthenticationHandler(AuthRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<EmptyResult> Handle(RevokeAuthenticationCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByRefreshTokenAsync(new GetUserByRefreshTokenCmd(request.Token), cancellationToken);

            if (user == null)
                throw new NoUserException("No user was found with this token");

            var refreshToken =
                await _repository.GetRefreshToken(new GetRefreshTokenCmd(request.Token), cancellationToken);

            if (!refreshToken.IsActive)
                throw new TokenExpiredException("Token already inactive");
            
            var updateCmd = _mapper.Map<UpdateRefreshTokenCmd>(request, opt
                => opt.AfterMap((_, dest) =>
                {
                    dest.RevokedAt = DateTime.UtcNow;
                }));
            await _repository.UpdateRefreshTokenAsync(updateCmd, cancellationToken);
            
            return new EmptyResult();
        }
    }
}