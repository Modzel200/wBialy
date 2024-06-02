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
using System.Globalization;
using Microsoft.Extensions.Caching.Memory;

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
        Task<IEnumerable<PostDto>> GetAllToConfirm();
        Task<PostDto> GetById(int id);
        Task<PostDto> GetByIdToConfirm(int id);
        Task UpdateEventPost(EditEventPostDto editPostDto, int id);
        Task UpdateGastroPost(EditGastroPostDto editPostDto, int id);
        Task UpdateLFPost(EditLFPostDto editPostDto, int id);
        Task<IEnumerable<GastroTagDto>> GetAllGastroTags();
        Task<IEnumerable<EventTagDto>> GetAllEventTags();
        Task<IEnumerable<LFTagDto>> GetAllLFTags();
        Task<IEnumerable<PostDto>> GetAllUserLFPosts();
        Task<IEnumerable<PostDto>> GetAllUserEventPosts();
        Task<IEnumerable<PostDto>> GetAllUserGastroPosts();
        Task DeleteAsAdmin(int id);
        Task LikePost(int id);
        Task<IEnumerable<PostDto>> GetAllUserLikedPosts();
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
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == id && x.Confirmed == true);
            if (post is null)
                throw new NotFoundException("Post not found");
            var result = _mapper.Map<PostDto>(post);
            var user = _userContextService.User;
            if(user.Claims.Count() > 0)
            {
                result.isLiked = await DidUserLikedPost(id);
            } else
            {
                result.isLiked = false;
            }
            
            return result;
        }
        public async Task<PageResult<PostDto>> GetAllLFPosts(PostQuery query)
        {
            List<LFPost> baseQuery;
            List<LFTag> tags = new List<LFTag>();
            bool found = false;
            if (string.Compare(query.LfFlag.ToLower(), "found") == 0)
            {
                found = true;
            }
            if (string.IsNullOrEmpty(query.SortBy))
            {
                query.SortBy = "AddDate"; //tutaj sie usunie i doda od razu sortowanie w frontend po dacie
            }
            if (!query.TagFilter.IsNullOrEmpty())
            {
                tags = await _context.LFTags.Where(x => query.TagFilter.Contains(x.Name)).ToListAsync();
            }
            var columnsSelectors = new Dictionary<string, Expression<Func<LFPost, object>>>
            {
                //{ nameof(LFPost.Title), x => x.Title},
                //{ nameof(LFPost.Description), x => x.Description},
                { nameof(LFPost.AddDate), x => x.AddDate},
            };
            var selectedColumn = columnsSelectors[query.SortBy];
            if (query.SortDirection == SortDirection.ASC)
            {
                baseQuery = await _context
                .LFPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())))
                && (query.TagFilter.IsNullOrEmpty()
                || x.Tags.Any(y => tags.Contains(y)))
                && x.Confirmed == true && x.Found == found)
                .OrderBy(selectedColumn)
                .ToListAsync();
            }
            else
            {
                baseQuery = await _context
                .LFPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())))
                && (query.TagFilter.IsNullOrEmpty()
                || x.Tags.Any(y => tags.Contains(y)))
                && x.Confirmed == true && x.Found == found)
                .OrderByDescending(selectedColumn)
                .ToListAsync();
            }
            var posts = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize);
            var totalItemsCount = baseQuery.Count;
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }

        //private IQueryable<LFPost> GetAllLFPostsQueryable()
        //{
        //    return _context.LFPosts.Include(x => x.Tags).AsQueryable();
        //}
        //public async Task<PageResult<PostDto>> GetAllLFPosts(PostQuery query)
        //{
        //    List<LFTag> tags = new List<LFTag>();
        //    if (string.IsNullOrEmpty(query.SortBy))
        //    {
        //        query.SortBy = "AddDate"; //tutaj sie usunie i doda od razu sortowanie w frontend po dacie
        //    }
        //    if (!query.TagFilter.IsNullOrEmpty())
        //    {
        //        tags = await _context.LFTags.Where(x => query.TagFilter.Contains(x.Name)).ToListAsync();
        //    }
        //    bool found = string.Compare(query.LfFlag.ToLower(), "found") == 0 ?
        //        true : false;
        //    var columnsSelectors = new Dictionary<string, Expression<Func<LFPost, object>>>
        //    {
        //        //{ nameof(LFPost.Title), x => x.Title},
        //        //{ nameof(LFPost.Description), x => x.Description},
        //        { nameof(LFPost.AddDate), x => x.AddDate},
        //    };
        //    var selectedColumn = columnsSelectors[query.SortBy];
        //    var baseQuery = query.SortDirection == SortDirection.ASC ?
        //        await GetAllLFPostsQueryable().Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
        //        || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
        //        || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())))
        //        && (query.TagFilter.IsNullOrEmpty()
        //        || x.Tags.Any(y => tags.Contains(y))) && x.Confirmed == true && x.Found == found).OrderBy(selectedColumn).ToListAsync()
        //        : await GetAllLFPostsQueryable().Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
        //        || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
        //        || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())))
        //        && (query.TagFilter.IsNullOrEmpty()
        //        || x.Tags.Any(y => tags.Contains(y))) && x.Confirmed == true && x.Found == found).OrderByDescending(selectedColumn).ToListAsync();

        //    var posts = baseQuery
        //                .Skip(query.PageSize * (query.PageNumber - 1))
        //                .Take(query.PageSize);
        //    var totalItemsCount = baseQuery.Count;
        //    var postDtos = _mapper.Map<List<PostDto>>(posts);
        //    var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
        //    return await Task.FromResult(result);
        //}
        public async Task<PageResult<PostDto>> GetAllEventPosts(PostQuery query)
        {
            List<EventPost> baseQuery;
            List<EventTag> tags = new List<EventTag>();
            DateTime date;
            bool isDateSpecified = DateTime.TryParse(query.DateFilter, out date) && !string.IsNullOrEmpty(query.DateFilter);
            if (string.IsNullOrEmpty(query.SortBy))
            {
                query.SortBy = "EventDate"; //tutaj sie usunie i doda od razu sortowanie w frontend po dacie
            }
            if (!query.TagFilter.IsNullOrEmpty())
            {
                tags = await _context.EventTags.Where(x => query.TagFilter.Contains(x.Name)).ToListAsync();
            }
            var columnsSelectors = new Dictionary<string, Expression<Func<EventPost, object>>>
            {
                //{ nameof(EventPost.Title), x => x.Title},
                //{ nameof(EventPost.Description), x => x.Description},
                //{ nameof(EventPost.AddDate), x => x.AddDate},
                { nameof(EventPost.EventDate), x => x.EventDate},
            };
            var selectedColumn = columnsSelectors[query.SortBy];
            if (query.SortDirection == SortDirection.ASC)
            {
                baseQuery = await _context
                .EventPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())))
                && (query.TagFilter.IsNullOrEmpty()
                || x.Tags.Any(y => tags.Contains(y)))
                && (!isDateSpecified
                || x.EventDate.Date == date.Date)
                && x.Confirmed == true)
                .OrderBy(selectedColumn)
                .ToListAsync();
            }
            else
            {
                baseQuery = await _context
                .EventPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())))
                && (query.TagFilter.IsNullOrEmpty()
                || x.Tags.Any(y => tags.Contains(y)))
                && (!isDateSpecified
                || x.EventDate.Date == date.Date)
                && x.Confirmed == true)
                .OrderByDescending(selectedColumn)
                .ToListAsync();
            }
            var posts = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize);
            var totalItemsCount = baseQuery.Count;
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }
        public async Task<PageResult<PostDto>> GetAllGastroPosts(PostQuery query)
        {
            List<GastroPost> baseQuery;
            List<GastroTag> tags = new List<GastroTag>();
            if (string.IsNullOrEmpty(query.SortBy))
            {
                query.SortBy = "AddDate"; //tutaj sie usunie i doda od razu sortowanie w frontend po dacie
            }
            if (!query.TagFilter.IsNullOrEmpty())
            {
                tags = await _context.GastroTags.Where(x => query.TagFilter.Contains(x.Name)).ToListAsync();
            }
            var columnsSelectors = new Dictionary<string, Expression<Func<GastroPost, object>>>
            {
                //{ nameof(GastroPost.Title), x => x.Title},
                //{ nameof(GastroPost.Description), x => x.Description},
                { nameof(GastroPost.AddDate), x => x.AddDate},
                //{ nameof(GastroPost.Day), x => x.Day},
            };
            var selectedColumn = columnsSelectors[query.SortBy];
            if (query.SortDirection == SortDirection.ASC)
            {
                baseQuery = await _context
                .GastroPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())))
                && (query.TagFilter.IsNullOrEmpty()
                || x.Tags.Any(y => tags.Contains(y)))
                && (string.IsNullOrEmpty(query.DateFilter)
                || string.Compare(query.DateFilter , x.Day) == 0)
                && x.Confirmed == true)
                .OrderBy(selectedColumn)
                .ToListAsync();
            }
            else
            {
                baseQuery = await _context
                .GastroPosts
                .Where(x => (string.IsNullOrEmpty(query.SearchPhrase)
                || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())))
                && (query.TagFilter.IsNullOrEmpty()
                || x.Tags.Any(y => tags.Contains(y)))
                && (string.IsNullOrEmpty(query.DateFilter)
                || string.Compare(query.DateFilter, x.Day) == 0)
                && x.Confirmed == true)
                .OrderByDescending(selectedColumn)
                .ToListAsync();
            }
            var posts = baseQuery
                        .Skip(query.PageSize * (query.PageNumber - 1))
                        .Take(query.PageSize);
            var totalItemsCount = baseQuery.Count;
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var result = new PageResult<PostDto>(postDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }

        public async Task<int> CreateLFPost(CreateLFPostDto dto)
        {
            var tagList = new List<LFTag>();
            var likedBy = new List<User>();
            foreach (var e in dto.Tags)
            {
                tagList.Add(await _context.LFTags.SingleOrDefaultAsync(x => x.Name == e.Name));
            }
            var postDto = new LFPost()
            {
                Title = dto.Title,
                Description = dto.Description,
                Image = dto.Image,
                Place = dto.Place,
                Found = dto.Found,
                Location = dto.Location,
                Tags = tagList,
                LikedBy = likedBy
            };
            var userId = _userContextService.GetUserId;
            postDto.UserId = userId;
            await _context.AddAsync(postDto);
            postDto.AddDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return postDto.PostId;
        }
        public async Task<int> CreateGastroPost(CreateGastroPostDto dto)
        {
            var likedBy = new List<User>();
            var tagList = new List<GastroTag>();
            foreach (var e in dto.Tags)
            {
                tagList.Add(await _context.GastroTags.SingleOrDefaultAsync(x => x.Name == e.Name));
            }
            var postDto = new GastroPost()
            {
                Title = dto.Title,
                Description = dto.Description,
                Image = dto.Image,
                Place = dto.Place,
                Location = dto.Location,
                Day = dto.Day,
                Link = dto.Link,
                Tags = tagList,
                LikedBy = likedBy
            };
            var userId = _userContextService.GetUserId;
            postDto.UserId = userId;
            await _context.AddAsync(postDto);
            postDto.AddDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return postDto.PostId;
        }
        public async Task<int> CreateEventPost(CreateEventPostDto dto)
        {
            var likedBy = new List<User>();
            var tagList = new List<EventTag>();
            foreach (var e in dto.Tags)
            {
                tagList.Add(await _context.EventTags.SingleOrDefaultAsync(x => x.Name == e.Name));
            }
            var postDto = new EventPost()
            {
                Title = dto.Title,
                Description = dto.Description,
                Image = dto.Image,
                Place = dto.Place,
                Location = dto.Location,
                EventDate = DateTime.Parse(dto.EventDate),
                Link = dto.Link,
                Tags = tagList,
                LikedBy = likedBy
            };
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
                .SingleOrDefaultAsync(x => x.PostId == id);
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
        public async Task DeleteAsAdmin(int id)
        {
            _logger.LogWarning($"Post with id: {id} DELETE action invoked");
            var post = await _context
                .Posts
                .SingleOrDefaultAsync(x => x.PostId == id);
            if (post is null)
            {
                throw new NotFoundException("Post not found");
            }
            _context.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLFPost(EditLFPostDto editPostDto, int id)
        {
            var postToUpdate = await _context
                .LFPosts
                .Include(x => x.Tags)
                .SingleOrDefaultAsync(x => x.PostId == id);
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
                postToUpdate.Tags.Add(await _context.LFTags.SingleOrDefaultAsync(x => x.Name == e.Name));
            }
            postToUpdate.Title = editPostDto.Title;
            postToUpdate.Description = editPostDto.Description;
            postToUpdate.Image = editPostDto.Image;
            postToUpdate.Place = editPostDto.Place;
            postToUpdate.Found = editPostDto.Found;
            postToUpdate.Location = editPostDto.Location;
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
                .SingleOrDefaultAsync(x => x.PostId == id);
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
                postToUpdate.Tags.Add(await _context.EventTags.SingleOrDefaultAsync(x => x.Name == e.Name));
            }
            postToUpdate.Title = editPostDto.Title;
            postToUpdate.Description = editPostDto.Description;
            postToUpdate.Image = editPostDto.Image;
            postToUpdate.Place = editPostDto.Place;
            postToUpdate.Location = editPostDto.Location;
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
                .SingleOrDefaultAsync(x => x.PostId == id);
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
                postToUpdate.Tags.Add(await _context.GastroTags.SingleOrDefaultAsync(x => x.Name == e.Name));
            }
            postToUpdate.Title = editPostDto.Title;
            postToUpdate.Description = editPostDto.Description;
            postToUpdate.Image = editPostDto.Image;
            postToUpdate.Place = editPostDto.Place;
            postToUpdate.Location = editPostDto.Location;
            postToUpdate.Day = editPostDto.Day;
            postToUpdate.Link = editPostDto.Link;
            postToUpdate.Confirmed = false;
            _context.Update(postToUpdate);
            await _context.SaveChangesAsync();
        }
        public async Task<PostDto> GetByIdToConfirm(int id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == id && x.Confirmed == false);
            if (post is null)
                throw new NotFoundException("Post not found");
            var result = _mapper.Map<PostDto>(post);
            return result;
        }
        public async Task<IEnumerable<PostDto>> GetAllToConfirm()
        {
            List<Post> baseQuery = await _context
                    .Posts
                    .Where(x => x.Confirmed == false)
                    .ToListAsync();
            var postDtos = _mapper.Map<List<PostDto>>(baseQuery);
            return postDtos;
        }
        public async Task Confirm(int id)
        {
            var postToConfirm = await _context
                .Posts
                .SingleOrDefaultAsync(x => x.PostId == id && x.Confirmed == false);
            if (postToConfirm is null)
            {
                throw new NotFoundException("Post not found");
            }
            postToConfirm.Confirmed = true;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<GastroTagDto>> GetAllGastroTags()
        {
            var tags = await _context
                .GastroTags
                .ToListAsync();
            var tagsDto = _mapper.Map<List<GastroTagDto>>(tags);
            return tagsDto;
        }
        public async Task<IEnumerable<EventTagDto>> GetAllEventTags()
        {
            var tags = await _context
                .EventTags
                .ToListAsync();
            var tagsDto = _mapper.Map<List<EventTagDto>>(tags);
            return tagsDto;
        }
        public async Task<IEnumerable<LFTagDto>> GetAllLFTags()
        {
            var tags = await _context
                .LFTags
                .ToListAsync();
            var tagsDto = _mapper.Map<List<LFTagDto>>(tags);
            return tagsDto;
        }
        public async Task<IEnumerable<PostDto>> GetAllUserLFPosts()
        {
            var userId = (int)_userContextService.GetUserId;
            var posts = await _context
                .LFPosts.Where(x => x.UserId == userId)
                .ToListAsync();
            if (posts is null)
            {
                throw new NotFoundException("Not found");
            }
            var postsDto = _mapper.Map<List<PostDto>>(posts);
            return postsDto;
        }
        public async Task<IEnumerable<PostDto>> GetAllUserEventPosts()
        {
            var userId = (int)_userContextService.GetUserId;
            var posts = await _context
                .EventPosts.Where(x => x.UserId == userId)
                .ToListAsync();
            if (posts is null)
            {
                throw new NotFoundException("Not found");
            }
            var postsDto = _mapper.Map<List<PostDto>>(posts);
            return postsDto;
        }
        public async Task<IEnumerable<PostDto>> GetAllUserGastroPosts()
        {
            var userId = (int)_userContextService.GetUserId;
            var posts = await _context
                .GastroPosts.Where(x => x.UserId == userId)
                .ToListAsync();
            if (posts is null)
            {
                throw new NotFoundException("Not found");
            }
            var postsDto = _mapper.Map<List<PostDto>>(posts);
            return postsDto;
        }
        public async Task LikePost(int id)
        {
            var userId = _userContextService.GetUserId;
            var user = await _context.Users.Include(x => x.LikedPosts).SingleOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var postTolike = await _context.Posts.Include(x => x.LikedBy).SingleOrDefaultAsync(x => x.PostId == id && x.Confirmed == true);
            if (postTolike is null)
            {
                throw new NotFoundException("Post not found");
            }
            if (postTolike.LikedBy.Contains(user))
            {
                postTolike.LikedBy.Remove(user);
                postTolike.LikeCount--;
            } else
            {
                postTolike.LikedBy.Add(user);
                postTolike.LikeCount++;
            }
            _context.Update(postTolike);
            await _context.SaveChangesAsync();
        }
        private async Task<bool> DidUserLikedPost(int postId)
        {
            var userId = _userContextService.GetUserId;
            if(userId is null)
            {
                throw new NotFoundException("User not found");
            }
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);
            if(user == null)
            {
                throw new NotFoundException("User not found");
            }
            var post = await _context.Posts.Include(x => x.LikedBy).SingleOrDefaultAsync(x => x.PostId == postId);
            if(post == null)
            {
                throw new NotFoundException("Post not found");
            }
            return post.LikedBy.Contains(user);
        }
        public async Task<IEnumerable<PostDto>> GetAllUserLikedPosts()
        {
            var userId = _userContextService.GetUserId;
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);
            if(user == null)
            {
                throw new NotFoundException("User not found");
            }

            List<EventPost> baseQuery = await _context
                    .EventPosts
                    .Include(x => x.LikedBy)
                    .Where(x => x.LikedBy.Contains(user))
                    .ToListAsync();
            var postDtos = _mapper.Map<List<PostDto>>(baseQuery);
            foreach(var post in postDtos)
            {
                post.isLiked = true;
            }
            return postDtos;
        }
    }
}
