using AutoMapper;
using MediatR;

using Microsoft.AspNetCore.Identity;
using Post.Application.Interfaces;
using Post.Domain;
using Post.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Command.Create
{
    internal class CreatPostHandler : IRequestHandler<CreatePostCommand, string>
    {
        private readonly IBaseRepository<Posts> _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> userManager;
        public CreatPostHandler(IBaseRepository<Posts> repo, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<string> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {

            var post = _mapper.Map<Posts>(request);

            if (await userManager.FindByIdAsync(request.UserId) is null)
                return "There is no user in The same id";
            post.UserId = request.UserId;
            post.CreatedAt = DateTime.Now;
            post = await _repo.AddAsync(post);
            return new { PostId =  post.Id }.ToString() ;
        }
    }
}
