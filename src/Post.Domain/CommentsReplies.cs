using Post.Domain.IdentityModels.ExtendedUser;

namespace Post.Domain;

public class CommentsReplies
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int PostCommentId { get; set; }
        public PostComment? PostComment { get; set; }
        public bool IsBlocked { get; set; }

}

