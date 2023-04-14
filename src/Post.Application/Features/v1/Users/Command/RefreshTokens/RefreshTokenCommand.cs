using MediatR;
using Post.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Users.Command.RefreshTokens
{
    public class RefreshTokenCommand:IRequest<AuthModel>
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
