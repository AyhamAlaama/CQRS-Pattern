using Microsoft.AspNetCore.Identity;

namespace Post.Domain.IdentityModels.ExtendedUser
{
    public class ApplicationUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public ICollection<Posts>? Posts { get; set; }
        public ICollection<PostComment>? PostComments { get; set; }
        public ICollection<CommentsReplies>? CommentsReplies { get; set; }
    }
}
