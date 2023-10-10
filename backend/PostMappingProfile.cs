using AutoMapper;
using wBialy.Entities;
using wBialy.Models;

namespace wBialy
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<CreatePostDto, Post>();
        }
    }
}
