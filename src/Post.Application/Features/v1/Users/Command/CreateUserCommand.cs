using MediatR;
using Post.Domain.IdentityModels.ExtendedUser;
using System.ComponentModel.DataAnnotations;

namespace Post.Application.Features.v1.Users.Command;

    public class CreateUserCommand :IRequest<AuthModel>
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

    }