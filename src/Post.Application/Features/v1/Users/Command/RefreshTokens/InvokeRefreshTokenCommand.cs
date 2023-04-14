using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Users.Command.RefreshTokens
{
    public class InvokeRefreshTokenCommand:IRequest<bool>
    {
        public string? RefreshTokenId { get; set; }
    }
}
