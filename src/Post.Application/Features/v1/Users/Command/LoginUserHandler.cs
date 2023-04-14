using MediatR;
using Post.Application.Interfaces;
using Post.Domain.IdentityModels.ExtendedUser;

namespace Post.Application.Features.v1.Users.Command;
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthModel>
    {
        private readonly IAuthService _authService;
        public LoginUserHandler(IAuthService authService)
        => _authService = authService;
        
        public async Task<AuthModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var authModel = await _authService.LoginAsync(request);
            return authModel;
        }
    }

