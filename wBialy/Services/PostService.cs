using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Linq.Expressions;
using wBialy.Authorization;
using wBialy.Entities;
using wBialy.Exceptions;
using wBialy.Models;

namespace wBialy.Services
{
    public interface IPostService
    {
        int Create(CreatePostDto dto);
        void Delete(int id);
        PageResult<PostDto> GetAll(PostQuery query);
        PostDto GetById(int id);
        void Update(EditPostDto editPostDto, int id);
    }

    public class PostService : IPostService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PostService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public PostService(AppDbContext context, ILogger<PostService> logger, IAuthorizationService authorizationService, IUserContextService userContextService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        public PostDto GetById(int id)
        {
            var post = _context.Posts.FirstOrDefault(x => x.PostId == id);
            if (post is null)
                throw new NotFoundException("Post not found");
            var result = _mapper.Map<PostDto>(post);
            return result;
        }
        public PageResult<PostDto> GetAll(PostQuery query)
        {
            var baseQuery = _context
                .Posts
                .Where(x => query.SearchPhrase == null
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())
                ));
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Post, object>>>
                {
                    { nameof(Post.Title), x => x.Title},
                    { nameof(Post.Description), x => x.Description},
                    { nameof(Post.EventDate), x => x.EventDate},
                };
                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }
            var posts = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();
            var totalItemsCount = baseQuery.Count();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }
        public int Create(CreatePostDto dto)
        {
            var postDto = _mapper.Map<Post>(dto);
            var userId = _userContextService.GetUserId;
            postDto.UserId = userId;
            _context.Add(postDto);
            postDto.AddDate = DateTime.Now;
            _context.SaveChanges();
            return postDto.PostId;
        }
        public void Delete(int id)
        {
            _logger.LogWarning($"Post with id: {id} DELETE action invoked");
            var post = _context
                .Posts
                .FirstOrDefault(x => x.PostId == id);
            if (post is null)
            {
                throw new NotFoundException("Post not found");
            }
            var user = _userContextService.User;
            var authorizationResult = _authorizationService.AuthorizeAsync(user, post, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Forbidden");
            }
            _context.Remove(post);
            _context.SaveChanges();
        }
        public void Update(EditPostDto editPostDto, int id)
        {
            var postToUpdate = _context
                .Posts
                .FirstOrDefault(x => x.PostId == id);
            if (postToUpdate is null)
            {
                throw new NotFoundException("Post not found");
            }
            var user = _userContextService.User;
            var authorizationResult = _authorizationService.AuthorizeAsync(user, postToUpdate, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Forbidden");
            }
            postToUpdate.Title = editPostDto.Title;
            postToUpdate.Description = editPostDto.Description;
            postToUpdate.Image = editPostDto.Image;
            postToUpdate.Place = editPostDto.Place;
            postToUpdate.EventDate = editPostDto.EventDate;
            postToUpdate.Tags = editPostDto.Tags;
            postToUpdate.Link = editPostDto.Link;
            _context.SaveChanges();
        }
    }
}
