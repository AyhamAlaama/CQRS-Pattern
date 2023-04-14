using Post.Domain.IdentityModels.ExtendedUser;

namespace Post.Domain;

public sealed class PostComment
{ 
    public int Id { get; set; }
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }
    public int PostId { get; set; }
    public Posts? Post { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public ICollection<CommentsReplies>? CommentsReplies { get; set; }
    public bool IsBlocked { get; set; }
}
    

