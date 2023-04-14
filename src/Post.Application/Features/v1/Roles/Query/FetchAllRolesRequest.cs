using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Roles.Query
{
    public class FetchAllRolesRequest:IRequest<List<RolesDto>>
    {
    }
    public class FetchAllRolesHandler : IRequestHandler<FetchAllRolesRequest, List<RolesDto>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public FetchAllRolesHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<List<RolesDto>> Handle(FetchAllRolesRequest request, CancellationToken cancellationToken)
        {

            var query = await _roleManager.Roles.ToListAsync();

            return _mapper.Map<List<RolesDto>>(query);
        }
    }

    public class RolesDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
