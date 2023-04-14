using AutoMapper;
using MediatR;
using Post.Application.Interfaces;
using Post.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Query.FetchAllPosts.PostIsBlocked
{
    public class ListPostsIsBlocked
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsBlocked { get; set; }
    }
    public class PostIsBlockedRequest:IRequest<List<ListPostsIsBlocked>>
    {

    }
    public class PostIsBlockedHandler : IRequestHandler<PostIsBlockedRequest, List<ListPostsIsBlocked>>
    {

        private readonly IBaseRepository<Posts> _repo;
        private readonly IMapper _mapper;

        public PostIsBlockedHandler(IBaseRepository<Posts> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ListPostsIsBlocked>> Handle(PostIsBlockedRequest request, CancellationToken cancellationToken)
        {

            var query = await _repo.GetAllAsync
                (isblocked => isblocked.IsBlocked==true,
                includes: new string[] { "User" });
            return _mapper.Map<List<ListPostsIsBlocked>>(query);
        }
    }
}
