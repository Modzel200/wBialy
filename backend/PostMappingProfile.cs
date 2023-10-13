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
            CreateMap<LFPost, PostDto>();
            CreateMap<GastroPost, PostDto>();
            CreateMap<EventPost, PostDto>();
            //CreateMap<CreateEventPostDto, Post>();
            //CreateMap<CreateGastroPostDto, Post>();
            //CreateMap<CreateLFPostDto, Post>();
            CreateMap<CreateLFPostDto, LFPost>();
            CreateMap<CreateGastroPostDto, GastroPost>();
            CreateMap<CreateEventPostDto, EventPost>();
        }
    }
}
