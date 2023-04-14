using AutoMapper;
using MediatR;
using Post.Application.Interfaces;
using Post.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Query.FetchAllPosts
{
    internal class FetchAllHandler : IRequestHandler<FetchAllRequest, List<GetAllPostDto>>
    {
        private readonly IBaseRepository<Posts> _repo;
        private readonly IMapper _mapper;
        public FetchAllHandler(IBaseRepository<Posts> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<GetAllPostDto>> Handle(FetchAllRequest request, CancellationToken cancellationToken)
        {
            var query = await _repo.GetAllAsync(includes: new string[] {"User"});
            return _mapper.Map<List<GetAllPostDto>>(query);
        }
    }
}
