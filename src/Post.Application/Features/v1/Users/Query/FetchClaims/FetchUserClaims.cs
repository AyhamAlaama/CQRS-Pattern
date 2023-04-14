using MediatR;
using Microsoft.AspNetCore.Identity;
using Post.Application.Interfaces;
using Post.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Users.Query.FetchClaims
{
    public class FetchUserClaims:IRequest<List<Claim>>
    {
        public string? UserId { get; set; }
    }
    public class FetchUserClaimsHandler : IRequestHandler<FetchUserClaims,
        List<Claim>>
    {
        private readonly IAuthService _authService;

        public FetchUserClaimsHandler(IAuthService authService)=>
            _authService = authService;

        public async Task<List<Claim>> Handle(FetchUserClaims request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetUserClaims(request.UserId);
            return result;
        }
    }
}
