using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post.Api.Extensions;
using Post.Application.Features.v1.Roles.Command.AddUserToRole;
using Post.Application.Features.v1.Roles.Command.CreateRole;
using Post.Application.Features.v1.Roles.Command.RemoveUserFromRole;
using Post.Application.Features.v1.Roles.Query;
using Post.Application.Features.v1.Users.Command;
using Post.Application.Features.v1.Users.Command.RefreshTokens;
using Post.Application.Features.v1.Users.Query.FetchAllUsers;
using Post.Application.Features.v1.Users.Query.FetchClaims;
using Post.Domain.IdentityModels.ExtendedUser;
using System.Security.Claims;

namespace Post.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Manage Users And Roles
        /// </summary>
        /// <param name="mediator"></param>
        public ManageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// register new user
        /// </summary>
        /// <param name="command">CreateUserCommand model</param>
        /// <returns> UserName , UserId</returns>
        [HttpPost("Users/SignUp")]
        public async Task<ActionResult<AuthModel>> CreateUser([FromBody]CreateUserCommand command)
        {
            var cmd = await _mediator.Send(command);
            //check if no error
            if (cmd.Message is null)
            {
                
                return Ok(cmd);
            }
            return Ok(cmd.Message);
        }
        /// <summary>
        /// Get RefreshToken For 7 month
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
       // [Authorize]
        [HttpPost("Users/RefreshToken")]
        public async Task<ActionResult<AuthModel>> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var cmd = await _mediator.Send(command);
            if (cmd.Message is null)
            {
                
                return Ok(new {token = cmd.Token, refreshtoken = cmd.RefreshToken});
            }

            return Ok(cmd);
        }

        [HttpGet("Users/AllRefreshTokenNotInvoked")]
        public async Task<ActionResult<List<RefreshTokenDto>>> GetAllRefreshToken()
        => Ok(await _mediator.Send(new FetchAllRefreshTokenIsNotInvoked()));
     
        [HttpPost("Users/InvokeRefreshToken")]
        public async Task<ActionResult<bool>> InvokeRefreshToken([FromBody] InvokeRefreshTokenCommand command)
        => Ok(await _mediator.Send(command));
        
        /// <summary>
        /// Login user to system
        /// </summary>
        /// <param name="command"> UserName or Email , UserId</param>
        /// <returns>UserName , UserId</returns>
        [HttpPost("Users/Signin")]
        public async Task<ActionResult<AuthModel>> LoginUser([FromBody] LoginUserCommand command)
        {
            var cmd = await _mediator.Send(command);
            if (cmd.Message is null)
            {
                return Ok(new { token = cmd.Token, refreshtoken = cmd.RefreshToken });
            }
            return Ok(cmd.Message);
        }
        /// <summary>
        /// Get List of Users
        /// </summary>
        /// <returns>List of Users</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("Users/All")]
        public async Task<ActionResult<List<GetAllUserDto>>> GetAllUsers()
        {
            var list = await _mediator.Send(new FetchAllRequest());
            return Ok(list);
        }
        /// <summary>
        /// Get Logged UserInfo
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Users/UserId")]
        public async Task<ActionResult<string>> UserInfo()
         => Ok(HttpContext.GetUserId());

        /// <summary>
        /// Get List of Roles
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("Roles/All")]
        public async Task<ActionResult<List<RolesDto>>> GetAllRoles()
        {
            var list = await _mediator.Send(new FetchAllRolesRequest());
            return Ok(list);
        }
        /// <summary>
        /// Get User Roles List
        /// </summary>
        /// <param name="id">UserId as string</param>
        /// <returns>User Roles List</returns>
       [HttpGet("Roles/UserRoles/{id}")]
        public async Task<ActionResult<List<UserRolesDto>>> AllUserRoles(string id)
        {
            var list = await _mediator.Send(new FetchUserRoles() { UserId=id});
            return Ok(list);
        }
        //[HttpGet("GetUserClaims")]
        //public async Task<ActionResult<List<Claim>>> GetUserClaims()
        //{
        //    var list = await _mediator.Send(new FetchUserClaims());
        //    return Ok(list);
        //}
       // [Authorize(Roles="Admin")]
       /// <summary>
       /// Add User To Role
       /// </summary>
       /// <param name="command"></param>
       /// <returns>message sccuss</returns>
        [HttpPost("Roles/AddUser")]
       public async Task<ActionResult<string>> AddUserToRole([FromBody] AddUserToRoleCommand command)
        {
            var cmd = await _mediator.Send(command);
            return cmd;
        }
        /// <summary>
        /// Create new role
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("Roles/Create")]
        public async Task<ActionResult<string>> CreateRole([FromBody]CreateRoleCommand  command)
        {
            var cmd = await _mediator.Send(command);
            return cmd;
        }
        /// <summary>
        /// Remove User from Role
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("Roles/RemoveUser")]
        public async Task<ActionResult<string>> RemoveUserFromRole(
            [FromBody] RemoveUserRoleCommand command)
        {
            var cmd = await _mediator.Send(command);
            return cmd;
        }



        private void SetTokenInCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddMinutes(2),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };
            HttpContext.Response.Cookies.Append("token", token, cookieOptions);
        }
        private void SetRefreshTokenInCookie(string refreshtoken)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMonths(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };
            HttpContext.Response.Cookies.Append("refreshtoken", refreshtoken, cookieOptions);
        }
    }
}