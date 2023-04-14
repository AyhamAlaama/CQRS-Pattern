using MediatR;
using Post.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Command.Create
{
    public class CreatePostCommand :IRequest<string>
    {
       // [Required]
        [JsonIgnore]
        public string? UserId { get; set; }
        [Required]
        [StringLength(maximumLength:250,MinimumLength =5)]
        public string? Text { get; set; }

    }
}
