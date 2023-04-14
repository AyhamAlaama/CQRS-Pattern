using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Post.Application.Interfaces;
using Post.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Users.Query.FetchAllUsers
{
    public class FetchAllHandler : IRequestHandler<FetchAllRequest, List<GetAllUserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public FetchAllHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<GetAllUserDto>> Handle(FetchAllRequest request, CancellationToken cancellationToken)
        {
               
            var query = await _userManager.Users.ToListAsync();
            return _mapper.Map<List<GetAllUserDto>>(query);
        }
    }
}
