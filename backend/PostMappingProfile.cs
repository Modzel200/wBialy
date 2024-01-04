using AutoMapper;
using Microsoft.EntityFrameworkCore;
using wBialy.Entities;
using wBialy.Exceptions;
using wBialy.Models;
using wBialy.Services;

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
                //.ForMember(x => x.LikedBy, y => y.MapFrom(z => new List<User>()));
            CreateMap<EventTag, EventTagDto>();
            CreateMap<LFTag, LFTagDto>();
            CreateMap<GastroTag, GastroTagDto>();
        }
    }
}
