using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Features.v1.Post.Query.FetchAllPosts
{
    public class FetchAllRequest : IRequest<List<GetAllPostDto>>
    {
    }
}
