using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using wBialy.Entities;
using wBialy.Models;
using wBialy.Services;

namespace wBialy.Controllers
{
    [Route("api/post")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Post>>> GetAll([FromQuery]PostQuery query)
        {
            var posts = await _postService.GetAll(query);
            return await Task.FromResult(Ok(posts));
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Post>> GetOne([FromRoute] int id)
        {
            var post = await _postService.GetById(id);
            return await Task.FromResult(Ok(post));
        }
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllToConfirm([FromQuery] PostQuery query)
        {
            var posts = await _postService.GetAllToConfirm(query);
            return await Task.FromResult(Ok(posts));
        }
        [HttpGet("admin/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Post>> GetOneToConfirm([FromRoute] int id)
        {
            var post = await _postService.GetByIdToConfirm(id);
            return await Task.FromResult(Ok(post));
        }
        [HttpPut("admin/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Confirm([FromRoute] int id)
        {
            await _postService.Confirm(id);
            return await Task.FromResult(Ok());
        }
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> CreatePost([FromBody] CreatePostDto dto)
        {
            var id = await _postService.Create(dto);
            return await Task.FromResult(Created($"/api/post/{id}", null));
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _postService.Delete(id);
            return await Task.FromResult(NoContent());
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> Update([FromBody] EditPostDto modifyPostDto, [FromRoute] int id)
        {
            await _postService.Update(modifyPostDto, id);
            return await Task.FromResult(Ok());
        }
    }
}
