using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Roles.Command.RemoveUserFromRole
{
    public class RemoveUserRoleCommand:IRequest<string>
    {
        public string? UserId { get; set; }
        public string? RoleName { get; set; }
    }
}
