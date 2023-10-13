using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using wBialy.Authorization;
using wBialy.Entities;
using wBialy.Exceptions;
using wBialy.Models;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace wBialy.Services
{
    public interface IPostService
    {
        Task Confirm(int id);
        Task<int> CreateEventPost(CreateEventPostDto dto);
        Task<int> CreateGastroPost(CreateGastroPostDto dto);
        Task<int> CreateLFPost(CreateLFPostDto dto);
        Task Delete(int id);
        Task<PageResult<PostDto>> GetAllEventPosts(PostQuery query);
        Task<PageResult<PostDto>> GetAllGastroPosts(PostQuery query);
        Task<PageResult<PostDto>> GetAllLFPosts(PostQuery query);
        Task<PageResult<PostDto>> GetAllToConfirm(PostQuery query);
        Task<PostDto> GetById(int id);
        Task<PostDto> GetByIdToConfirm(int id);
        Task UpdateEventPost(EditEventPostDto editPostDto, int id);
        Task UpdateGastroPost(EditGastroPostDto editPostDto, int id);
        Task UpdateLFPost(EditLFPostDto editPostDto, int id);
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
            return await Task.FromResult(result);
        }
        //public async Task<PageResult<PostDto>> GetAll(PostQuery query)
        //{
        //    if(String.Equals(query.PostType, "LFPost"))
        //    {
        //        return await GetAllLFPosts(query);
        //    } else
        //    {
        //        if(String.Equals(query.PostType, "EventPost"))
        //        {
        //            return await GetAllEventPosts(query);
        //        } else
        //        {
        //            return await GetAllGastroPosts(query);
        //        }
        //    }

        //    //var baseQuery = await _context
        //    //    .Posts
        //    //    .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
        //    //    || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
        //    //    || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == true).ToListAsync();
        //    //if (!string.IsNullOrEmpty(query.SortBy))
        //    //{
        //    //    var columnsSelectors = new Dictionary<string, Expression<Func<Post, object>>>
        //    //    {
        //    //        { nameof(Post.Title), x => x.Title},
        //    //        { nameof(Post.Description), x => x.Description},
        //    //        { nameof(Post.EventDate), x => x.EventDate},
        //    //    };
        //    //    var selectedColumn = columnsSelectors[query.SortBy];
        //    //    baseQuery = query.SortDirection == SortDirection.ASC ?
        //    //        baseQuery.OrderBy(selectedColumn)
        //    //        : baseQuery.OrderByDescending(selectedColumn);
        //    //}
        //    //var posts = await baseQuery
        //    //    .Skip(query.PageSize * (query.PageNumber - 1))
        //    //    .Take(query.PageSize)
        //    //    .ToListAsync();
        //    //var totalItemsCount = baseQuery.Count();
        //    //var postDtos = _mapper.Map<List<PostDto>>(posts);
        //    //var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
        //    //return await Task.FromResult(result);
        //}

        public async Task<PageResult<PostDto>> GetAllLFPosts(PostQuery query)
        {
            List<LFPost> baseQuery;
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                query.SortBy = "AddDate"; //tutaj sie usunie i doda od razu sortowanie w frontend po dacie
            }
            var columnsSelectors = new Dictionary<string, Expression<Func<LFPost, object>>>
            {
                { nameof(LFPost.Title), x => x.Title},
                { nameof(LFPost.Description), x => x.Description},
                { nameof(LFPost.AddDate), x => x.AddDate},
            };
            var selectedColumn = columnsSelectors[query.SortBy];
            if (query.SortDirection == SortDirection.ASC)
            {
                baseQuery = await _context
                .LFPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == true)
                .OrderBy(selectedColumn)
                .ToListAsync();
            }
            else
            {
                baseQuery = await _context
                .LFPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == true)
                .OrderByDescending(selectedColumn)
                .ToListAsync();
            }
            var posts = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize);
            var totalItemsCount = baseQuery.Count;
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return await Task.FromResult(result);
        }
        public async Task<PageResult<PostDto>> GetAllEventPosts(PostQuery query)
        {
            List<EventPost> baseQuery;
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                query.SortBy = "AddDate"; //tutaj sie usunie i doda od razu sortowanie w frontend po dacie
            }
            var columnsSelectors = new Dictionary<string, Expression<Func<EventPost, object>>>
            {
                { nameof(EventPost.Title), x => x.Title},
                { nameof(EventPost.Description), x => x.Description},
                { nameof(EventPost.AddDate), x => x.AddDate},
                { nameof(EventPost.EventDate), x => x.EventDate},
            };
            var selectedColumn = columnsSelectors[query.SortBy];
            if (query.SortDirection == SortDirection.ASC)
            {
                baseQuery = await _context
                .EventPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == true)
                .OrderBy(selectedColumn)
                .ToListAsync();
            }
            else
            {
                baseQuery = await _context
                .EventPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == true)
                .OrderByDescending(selectedColumn)
                .ToListAsync();
            }
            var posts = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize);
            var totalItemsCount = baseQuery.Count;
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return await Task.FromResult(result);
        }
        public async Task<PageResult<PostDto>> GetAllGastroPosts(PostQuery query)
        {
            List<GastroPost> baseQuery;
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                query.SortBy = "AddDate"; //tutaj sie usunie i doda od razu sortowanie w frontend po dacie
            }
            var columnsSelectors = new Dictionary<string, Expression<Func<GastroPost, object>>>
            {
                { nameof(GastroPost.Title), x => x.Title},
                { nameof(GastroPost.Description), x => x.Description},
                { nameof(GastroPost.AddDate), x => x.AddDate},
                { nameof(GastroPost.Day), x => x.Day},
            };
            var selectedColumn = columnsSelectors[query.SortBy];
            if (query.SortDirection == SortDirection.ASC)
            {
                baseQuery = await _context
                .GastroPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == true)
                .OrderBy(selectedColumn)
                .ToListAsync();
            }
            else
            {
                baseQuery = await _context
                .GastroPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == true)
                .OrderByDescending(selectedColumn)
                .ToListAsync();
            }
            var posts = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize);
            var totalItemsCount = baseQuery.Count;
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return await Task.FromResult(result);
        }

        //public async Task<int> Create(CreatePostDto dto)
        //{
        //    var postDto = _mapper.Map<Post>(dto);
        //    var userId = _userContextService.GetUserId;
        //    postDto.UserId = userId;
        //    await _context.AddAsync(postDto);
        //    postDto.AddDate = DateTime.Now;
        //    await _context.SaveChangesAsync();
        //    return await Task.FromResult(postDto.PostId);
        //}
        public async Task<int> CreateLFPost(CreateLFPostDto dto)
        {
            var tagList = new List<LFTag>();
            foreach (var e in dto.Tags)
            {
                tagList.Add(await _context.LFTags.FirstOrDefaultAsync(x => x.Name == e.Name));
            }
            var postDto = new LFPost()
            {
                Title = dto.Title,
                Description = dto.Description,
                Image = dto.Image,
                Place = dto.Place,
                Tags = tagList,
            };
            var userId = _userContextService.GetUserId;
            postDto.UserId = userId;
            await _context.AddAsync(postDto);
            postDto.AddDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return await Task.FromResult(postDto.PostId);
        }
        public async Task<int> CreateGastroPost(CreateGastroPostDto dto)
        {
            var tagList = new List<GastroTag>();
            foreach (var e in dto.Tags)
            {
                tagList.Add(await _context.GastroTags.FirstOrDefaultAsync(x => x.Name == e.Name));
            }
            var postDto = new GastroPost()
            {
                Title = dto.Title,
                Description = dto.Description,
                Image = dto.Image,
                Place = dto.Place,
                Day = dto.Day,
                Link = dto.Link,
                Tags = tagList,
            };
            var userId = _userContextService.GetUserId;
            postDto.UserId = userId;
            await _context.AddAsync(postDto);
            postDto.AddDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return await Task.FromResult(postDto.PostId);
        }
        public async Task<int> CreateEventPost(CreateEventPostDto dto)
        {
            var tagList = new List<EventTag>();
            foreach (var e in dto.Tags)
            {
                tagList.Add(await _context.EventTags.FirstOrDefaultAsync(x => x.Name == e.Name));
            }
            var postDto = new EventPost()
            {
                Title = dto.Title,
                Description = dto.Description,
                Image = dto.Image,
                Place = dto.Place,
                EventDate = dto.EventDate,
                Link = dto.Link,
                Tags = tagList,
            };
            var userId = _userContextService.GetUserId;
            postDto.UserId = userId;
            await _context.AddAsync(postDto);
            postDto.AddDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return await Task.FromResult(postDto.PostId);
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
            //_context.Remove(await _context.Posts.SingleOrDefaultAsync(x => x == post));
            await _context.SaveChangesAsync();
        }
        //public async Task Update(EditPostDto editPostDto, int id)
        //{
        //    var postToUpdate = await _context
        //        .Posts
        //        .FirstOrDefaultAsync(x => x.PostId == id);
        //    if (postToUpdate is null)
        //    {
        //        throw new NotFoundException("Post not found");
        //    }
        //    var user = _userContextService.User;
        //    var authorizationResult = _authorizationService.AuthorizeAsync(user, postToUpdate, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
        //    if (!authorizationResult.Succeeded)
        //    {
        //        throw new ForbidException("Forbidden");
        //    }
        //    postToUpdate.Title = editPostDto.Title;
        //    postToUpdate.Description = editPostDto.Description;
        //    postToUpdate.Image = editPostDto.Image;
        //    postToUpdate.Place = editPostDto.Place;
        //    //postToUpdate.EventDate = editPostDto.EventDate;
        //    //postToUpdate.Tags = editPostDto.Tags;
        //    //postToUpdate.Link = editPostDto.Link;
        //    postToUpdate.Confirmed = false;
        //    await _context.SaveChangesAsync();
        //}
        public async Task UpdateLFPost(EditLFPostDto editPostDto, int id)
        {
            var postToUpdate = await _context
                .LFPosts
                .Include(x => x.Tags)
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
            postToUpdate.Tags.Clear();
            foreach (var e in editPostDto.Tags)
            {
                postToUpdate.Tags.Add(await _context.LFTags.FirstOrDefaultAsync(x => x.Name == e.Name));
            }
            postToUpdate.Title = editPostDto.Title;
            postToUpdate.Description = editPostDto.Description;
            postToUpdate.Image = editPostDto.Image;
            postToUpdate.Place = editPostDto.Place;
            postToUpdate.Confirmed = false;
            //postToUpdate.EventDate = editPostDto.EventDate;
            _context.Update(postToUpdate);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEventPost(EditEventPostDto editPostDto, int id)
        {
            var postToUpdate = await _context
                .EventPosts
                .Include(x => x.Tags)
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
            postToUpdate.Tags.Clear();
            foreach (var e in editPostDto.Tags)
            {
                postToUpdate.Tags.Add(await _context.EventTags.FirstOrDefaultAsync(x => x.Name == e.Name));
            }
            postToUpdate.Title = editPostDto.Title;
            postToUpdate.Description = editPostDto.Description;
            postToUpdate.Image = editPostDto.Image;
            postToUpdate.Place = editPostDto.Place;
            postToUpdate.EventDate = editPostDto.EventDate;
            postToUpdate.Link = editPostDto.Link;
            postToUpdate.Confirmed = false;
            _context.Update(postToUpdate);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateGastroPost(EditGastroPostDto editPostDto, int id)
        {
            var postToUpdate = await _context
                .GastroPosts
                .Include(x => x.Tags)
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
            postToUpdate.Tags.Clear();
            foreach (var e in editPostDto.Tags)
            {
                postToUpdate.Tags.Add(await _context.GastroTags.FirstOrDefaultAsync(x => x.Name == e.Name));
            }
            postToUpdate.Title = editPostDto.Title;
            postToUpdate.Description = editPostDto.Description;
            postToUpdate.Image = editPostDto.Image;
            postToUpdate.Place = editPostDto.Place;
            postToUpdate.Day = editPostDto.Day;
            postToUpdate.Link = editPostDto.Link;
            postToUpdate.Confirmed = false;
            _context.Update(postToUpdate);
            await _context.SaveChangesAsync();
        }
        public async Task<PostDto> GetByIdToConfirm(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id && x.Confirmed == false);
            if (post is null)
                throw new NotFoundException("Post not found");
            var result = _mapper.Map<PostDto>(post);
            return await Task.FromResult(result);
        }
        public async Task<PageResult<PostDto>> GetAllToConfirm(PostQuery query)
        {
            List<Post> baseQuery;
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Post, object>>>
                {
                    { nameof(Post.Title), x => x.Title},
                    { nameof(Post.Description), x => x.Description},
                    //{ nameof(Post.EventDate), x => x.EventDate},
                };
                var selectedColumn = columnsSelectors[query.SortBy];
                if (query.SortDirection == SortDirection.ASC)
                {
                    baseQuery = await _context
                    .Posts
                    .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                    || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                    || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == false)
                    .OrderBy(selectedColumn)
                    .ToListAsync();
                }
                else
                {
                    baseQuery = await _context
                    .Posts
                    .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                    || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                    || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == false)
                    .OrderByDescending(selectedColumn)
                    .ToListAsync();
                }
            }
            else
            {
                baseQuery = await _context
                    .Posts
                    .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                    || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                    || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == false)
                    .ToListAsync();
            }
            var posts = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize);
            var totalItemsCount = baseQuery.Count;
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return await Task.FromResult(result);
            //var baseQuery = /*await*/ _context
            //    .Posts
            //    .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
            //    || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
            //    || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()))) && x.Confirmed == false)/*.ToListAsync()*/;
            //if (!string.IsNullOrEmpty(query.SortBy))
            //{
            //    var columnsSelectors = new Dictionary<string, Expression<Func<Post, object>>>
            //    {
            //        { nameof(Post.Title), x => x.Title},
            //        { nameof(Post.Description), x => x.Description},
            //        { nameof(Post.EventDate), x => x.EventDate},
            //    };
            //    var selectedColumn = columnsSelectors[query.SortBy];

            //    baseQuery = query.SortDirection == SortDirection.ASC ?
            //        baseQuery.OrderBy(selectedColumn)
            //        : baseQuery.OrderByDescending(selectedColumn);
            //}
            //var posts = await baseQuery
            //    .Skip(query.PageSize * (query.PageNumber - 1))
            //    .Take(query.PageSize)
            //    .ToListAsync();
            //var totalItemsCount = baseQuery.Count();
            //var postDtos = _mapper.Map<List<PostDto>>(posts);
            //var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            //return await Task.FromResult(result);
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
