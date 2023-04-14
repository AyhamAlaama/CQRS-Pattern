using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Post.Application.Interfaces;
using Post.Domain;
using Post.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Command.Update.UpdateForSameUser
{
    internal class UpdateUserPostHandler : IRequestHandler<UpdateUserPostCommand,string>
    {
        private readonly IBaseRepository<Posts> _repo;
        private readonly IMapper _map;
        private readonly UserManager<ApplicationUser> _userManager;
        public UpdateUserPostHandler(IBaseRepository<Posts> repo, 
            IMapper map, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _map = map;
            _userManager = userManager;
        }
        public async Task<string> Handle(UpdateUserPostCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                return "Invalid User or Post Id";
            var checkPost = await _repo.GetByAsync(p => p.Id == request.PostId);
            if (checkPost is null)
                return "Invalid User or Post Id";
            var checkOwend = await _repo.GetByAsync(p => p.Id == request.PostId && p.UserId == request.UserId);
            if (checkOwend is null)
                return "you dont owen this post";
            var post = _map.Map<Posts>(request);
            post.Text = request.Text;
            post.LastModified = DateTime.Now;
            await _repo.UpdateAsync(post);
            return "Updated !";
        }
    }
}
