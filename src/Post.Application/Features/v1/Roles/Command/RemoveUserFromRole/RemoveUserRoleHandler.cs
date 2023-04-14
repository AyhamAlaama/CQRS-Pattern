using MediatR;
using Post.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Roles.Command.RemoveUserFromRole
{
    public class RemoveUserRoleHandler : IRequestHandler<RemoveUserRoleCommand, string>
    {
        private readonly IAuthService _authService;

        public RemoveUserRoleHandler(IAuthService authService)
        => _authService = authService;
        

        public async Task<string> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.RemoveUserFromRoleAsync(request);
            return result;
        }
    }
}
