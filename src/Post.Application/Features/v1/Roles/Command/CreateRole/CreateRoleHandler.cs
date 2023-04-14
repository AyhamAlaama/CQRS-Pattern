using MediatR;
using Post.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Roles.Command.CreateRole
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, string>
    {
        private readonly IAuthService _authService;

        public CreateRoleHandler(IAuthService authService)
        =>_authService = authService;
        

        public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.CreateRole(request.Name);
            return result;
        }
    }
}
