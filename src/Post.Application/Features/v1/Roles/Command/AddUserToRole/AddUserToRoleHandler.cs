using MediatR;
using Post.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Roles.Command.AddUserToRole
{

    public class AddUserToRoleHandler : IRequestHandler<AddUserToRoleCommand, string>
    {
        private readonly IAuthService _authService;
        public AddUserToRoleHandler(IAuthService authService)
        => _authService = authService;

        public async Task<string> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _authService.AddUserToRoleAsync(request);
            return role;
        }
    }
}
