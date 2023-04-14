using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Roles.Command.AddUserToRole
{
    public class AddUserToRoleCommand:IRequest<string>
    {
        public string? UserId { get; set; }
        public string? RoleName { get; set; }
    }
}
