using MediatR;
using Post.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Roles.Query
{
    public class FetchUserRoles:IRequest<List<UserRolesDto>>
    {
        public string? UserId { get; set; }
    }
    public class FetchUserRolesHandler : IRequestHandler<FetchUserRoles, List<UserRolesDto>>
    {
        private readonly IAuthService _authService;
        public FetchUserRolesHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<List<UserRolesDto>> Handle(FetchUserRoles request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetUserRoles(request.UserId);
            return result;
        }
    }
    public class UserRolesDto
    {
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public List<string>? Roles { get; set; }

    }

}
