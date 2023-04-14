using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Post.Application.Features.v1.Post.Command.Create;
using Post.Application.Features.v1.Post.Command.Update;
using Post.Application.Features.v1.Post.Command.Update.UpdateForSameUser;
using Post.Application.Features.v1.Post.Query.FetchAllPosts;
using Post.Application.Features.v1.Post.Query.FetchAllPosts.PostIsBlocked;
using Post.Application.Features.v1.Post.Query.FetchOnePost;
using Post.Application.Features.v1.Roles.Query;
using Post.Application.Features.v1.Users.Command.RefreshTokens;
using Post.Application.Features.v1.Users.Query.FetchAllUsers;
using Post.Domain;
using Post.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Profiles
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<Posts, GetAllPostDto>()
                .ForMember
                (
                dest => dest.UserName,
                options => options.MapFrom(src => src.User.Email)
                )
                .ReverseMap();
            CreateMap<RefreshToken, RefreshTokenDto>()
                .ForMember
                (
                dest => dest.UserName,
                options => options.MapFrom(src => src.User.Email)
                )
                .ReverseMap();

            CreateMap<Posts, ListPostsIsBlocked>()
                .ForMember
                (
                dest => dest.UserName,
                options => options.MapFrom(src => src.User.Email)
                )
                .ReverseMap();
            CreateMap<Posts, GetPostDto>().ReverseMap();
            CreateMap<Posts, BlockPostCommand>().ReverseMap();
            CreateMap<Posts, UpdateUserPostCommand>().ReverseMap();
            CreateMap<Posts, CreatePostCommand>().ReverseMap();
            CreateMap<ApplicationUser, GetAllUserDto>().ReverseMap();
            CreateMap<IdentityRole, RolesDto>().ReverseMap();
        }
    }
}
