using AutoMapper;
using MediatR;
using Post.Application.Interfaces;
using Post.Application.Interfaces.Const;
using Post.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Users.Command.RefreshTokens
{
    public class FetchAllRefreshTokenIsNotInvoked:IRequest<List<RefreshTokenDto>>
    {
    }
    public class FetchAllRefreshTokenIsNotInvokedHandler : IRequestHandler<FetchAllRefreshTokenIsNotInvoked, List<RefreshTokenDto>>
    {
        private readonly IBaseRepository<RefreshToken> _baseRepo;
        private readonly IMapper _mapper;

        public FetchAllRefreshTokenIsNotInvokedHandler(IBaseRepository<RefreshToken> baseRepo, IMapper mapper)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
        }

        public async Task<List<RefreshTokenDto>> Handle(FetchAllRefreshTokenIsNotInvoked request, CancellationToken cancellationToken)
        {
            var list = await _baseRepo.GetAllAsync(
                        includes: new string[] { "User" },
                                orderby: x=> x.IsRevoked,
                                orederByDirection: OrderBy.Ascending);

            return _mapper.Map<List<RefreshTokenDto>>(list);
        }
    }
    public class RefreshTokenDto
    {
       
        public string? Token { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }

      
    }
}
