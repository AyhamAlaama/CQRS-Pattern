using AutoMapper;
using MediatR;
using Post.Application.Interfaces;
using Post.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Command.Update
{
    internal class BlockPostHandler : IRequestHandler<BlockPostCommand>
    {
        private readonly IBaseRepository<Posts> _repo;
        private readonly IMapper _map;
        public BlockPostHandler(IBaseRepository<Posts> repo, IMapper map)
        {
            _repo = repo;
            _map = map;
        }
        public async Task<Unit> Handle(BlockPostCommand request, CancellationToken cancellationToken)
        {
            var post = _map.Map<Posts>(request);
            post.LastModified = DateTime.Now;
            await _repo.UpdateAsync(post);
            return Unit.Value;
        }
    }
}
