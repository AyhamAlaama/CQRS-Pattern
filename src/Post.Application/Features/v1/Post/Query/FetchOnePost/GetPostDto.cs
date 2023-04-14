using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Query.FetchOnePost
{
    public class GetPostDto
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsBlocked { get; set; }
    }
}
