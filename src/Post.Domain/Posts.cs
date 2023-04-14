using Post.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain;
public sealed class Posts
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public ICollection<PostComment>? PostComment { get; set; }
    public bool IsBlocked { get; set; }
}

