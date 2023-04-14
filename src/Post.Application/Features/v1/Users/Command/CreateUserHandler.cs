using MediatR;
using Post.Application.Interfaces;
using Post.Domain.IdentityModels.ExtendedUser;

namespace Post.Application.Features.v1.Users.Command;

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, AuthModel>
    {
        private readonly IAuthService _authService;
        public CreateUserHandler(IAuthService authService)
        => _authService = authService;
        
        public async Task<AuthModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var authModel = await _authService.RegisterAsync(request);

            return authModel;
        }

 
    }
