using MediatR;
using Post.Application.Interfaces;
using Post.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Users.Command.RefreshTokens
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthModel>
    {
        private readonly IAuthService _authService;
        public RefreshTokenHandler(IAuthService authService)
        => _authService = authService;
        public async Task<AuthModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var authmodel = await _authService.RefreshTokenAsync(request);
            return authmodel;
        }
    }
}
