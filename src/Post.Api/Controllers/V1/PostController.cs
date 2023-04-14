using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Post.Api.Extensions;
using Post.Application.Features.v1.Post;
using Post.Application.Features.v1.Post.Command.Create;
using Post.Application.Features.v1.Post.Command.Update;
using Post.Application.Features.v1.Post.Command.Update.UpdateForSameUser;
using Post.Application.Features.v1.Post.Query.FetchAllPosts;
using Post.Application.Features.v1.Post.Query.FetchAllPosts.PostIsBlocked;

namespace Post.Api.Controllers.V1  
{

    [Route("api/V1/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Create New Post (Need User Authorize)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Posts/Create")]
        public async Task<ActionResult<int>> CreatePost([FromBody] CreatePostCommand command)
         => Ok(await _mediator.Send(
             new CreatePostCommand()
             { 
                 Text = command.Text ,
                 UserId = HttpContext.GetUserId()


             })
             );


        /// <summary>
        /// List of Post (Need Admin Authorize)
        /// </summary>
        /// <returns></returns>
       /// [Authorize(Roles = "Admin")]
        [HttpGet("Posts/All")]
        public async Task<ActionResult<List<GetAllPostDto>>> GetAllPosts()
         => Ok(await _mediator.Send(new FetchAllRequest()));
        /// <summary>
        /// List of blocked Post (Need Admin Authorize)
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        [HttpGet("Posts/AllIsBlocked")]
        public async Task<ActionResult<List<ListPostsIsBlocked>>> GetAllPostsIsBlocked()
         => Ok(await _mediator.Send(new PostIsBlockedRequest()));
        /// <summary>
        /// Block Post (Need Admin Authorize)
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>No Content</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("Posts/Block")]
        public async Task<ActionResult> BlockPost([FromBody] BlockPostCommand cmd)
        {
            await _mediator.Send(
                new BlockPostCommand 
                                { 
                                    Id=cmd.Id,
                                    IsBlocked=cmd.IsBlocked
                                });
            return NoContent();
        }
        /// <summary>
        /// Update : user can update his post text
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Posts/Update")]
        public async Task<ActionResult<string>> Update([FromBody] UpdateUserPostCommand cmd)
        {
           var reult = await _mediator.Send(
               new UpdateUserPostCommand 
               { PostId=cmd.PostId,UserId=HttpContext.GetUserId(),Text=cmd.Text});
            return reult;
        }

    }
}
