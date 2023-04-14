using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Command.Update.UpdateForSameUser
{
    public class UpdateUserPostCommand : IRequest<string>
    {
        [Required]
        public int PostId { get; set; }
        //[Required]
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? Text { get; set; }
    }
}
