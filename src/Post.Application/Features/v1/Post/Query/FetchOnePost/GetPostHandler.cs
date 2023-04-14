using AutoMapper;
using MediatR;
using Post.Application.Interfaces;
using Post.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Query.FetchOnePost
{
    public class GetPostHandler : IRequestHandler<GetPostRequest, GetPostDto>
    {
        private readonly IBaseRepository<Posts> _repo;
        private readonly IMapper _mapper;
        public GetPostHandler(IBaseRepository<Posts> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<GetPostDto> Handle(GetPostRequest request, CancellationToken cancellationToken)
        {
            var query = await _repo.GetByAsync(x => x.Id == request.Id);
            return  _mapper.Map<GetPostDto>(query);
        }
    }
}
