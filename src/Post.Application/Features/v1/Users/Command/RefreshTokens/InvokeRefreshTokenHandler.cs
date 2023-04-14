using MediatR;
using Post.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Users.Command.RefreshTokens
{
    public class InvokeRefreshTokenHandler : IRequestHandler<InvokeRefreshTokenCommand, bool>
    {
        private readonly IAuthService _authService;
        public InvokeRefreshTokenHandler(IAuthService authService)
        => _authService = authService;
        public async Task<bool> Handle(InvokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authService.InvokeRefreshToken(request);
        }
    }
}
