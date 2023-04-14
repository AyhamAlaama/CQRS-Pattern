using Post.Application.Features.v1.Roles.Command.AddUserToRole;
using Post.Application.Features.v1.Roles.Command.RemoveUserFromRole;
using Post.Application.Features.v1.Roles.Query;
using Post.Application.Features.v1.Users.Command.RefreshTokens;
using Post.Application.Features.v1.Users.Command;
using Post.Application.Features.v1.Users.Query.FetchClaims;
using Post.Domain.IdentityModels.ExtendedUser;
using System.Security.Claims;

namespace Post.Application.Interfaces;



public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(CreateUserCommand model);
        Task<AuthModel> LoginAsync(LoginUserCommand model);
        Task<AuthModel> RefreshTokenAsync(RefreshTokenCommand model);
        Task<string> AddUserToRoleAsync(AddUserToRoleCommand model);
        Task<List<UserRolesDto>> GetUserRoles(string id);
        Task<string> CreateRole(string name);
        Task<bool> InvokeRefreshToken(InvokeRefreshTokenCommand model);
        Task<string> RemoveUserFromRoleAsync( RemoveUserRoleCommand model);
        Task<List<Claim>> GetUserClaims(string UserId);

    }

