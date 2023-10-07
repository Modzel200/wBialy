using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
        Task Confirm(int id);
        Task<int> Create(CreatePostDto dto);
        Task Delete(int id);
        Task<PageResult<PostDto>> GetAll(PostQuery query);
        Task<PageResult<PostDto>> GetAllToConfirm(PostQuery query);
        Task<PostDto> GetById(int id);
        Task<PostDto> GetByIdToConfirm(int id);
        Task Update(EditPostDto editPostDto, int id);
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
        public async Task<PostDto> GetById(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id && x.Confirmed == true);
            if (post is null)
                throw new NotFoundException("Post not found");
            var result = _mapper.Map<PostDto>(post);
            return result;
        }
        public async Task<PageResult<PostDto>> GetAll(PostQuery query)
        {
            var baseQuery = _context
                .Posts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == true);
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
            var posts = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();
            var totalItemsCount = baseQuery.Count();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }
        public async Task<int> Create(CreatePostDto dto)
        {
            var postDto = _mapper.Map<Post>(dto);
            var userId = _userContextService.GetUserId;
            postDto.UserId = userId;
            await _context.AddAsync(postDto);
            postDto.AddDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return postDto.PostId;
        }
        public async Task Delete(int id)
        {
            _logger.LogWarning($"Post with id: {id} DELETE action invoked");
            var post = await _context
                .Posts
                .FirstOrDefaultAsync(x => x.PostId == id);
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
            await _context.SaveChangesAsync();
        }
        public async Task Update(EditPostDto editPostDto, int id)
        {
            var postToUpdate = await _context
                .Posts
                .FirstOrDefaultAsync(x => x.PostId == id);
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
            postToUpdate.Confirmed = false;
            await _context.SaveChangesAsync();
        }
        public async Task<PostDto> GetByIdToConfirm(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id && x.Confirmed == false);
            if (post is null)
                throw new NotFoundException("Post not found");
            var result = _mapper.Map<PostDto>(post);
            return result;
        }
        public async Task<PageResult<PostDto>> GetAllToConfirm(PostQuery query)
        {
            var baseQuery = _context
                .Posts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == false);
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
            var posts = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();
            var totalItemsCount = baseQuery.Count();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }
        public async Task Confirm(int id)
        {
            var postToConfirm = await _context
                .Posts
                .FirstOrDefaultAsync(x => x.PostId == id && x.Confirmed == false);
            if (postToConfirm is null)
            {
                throw new NotFoundException("Post not found");
            }
            postToConfirm.Confirmed = true;
            await _context.SaveChangesAsync();
        }
    }
}
