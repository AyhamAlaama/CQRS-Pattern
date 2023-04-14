using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Command.Update
{
    public class BlockPostCommand:IRequest
    {
        [Required]
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
    }
}
