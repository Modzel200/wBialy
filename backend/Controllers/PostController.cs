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
        [HttpGet("lfposts")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllLF([FromQuery]PostQuery query)
        {
            var posts = await _postService.GetAllLFPosts(query);
            return await Task.FromResult(Ok(posts));
        }
        [HttpGet("gastroposts")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllGastro([FromQuery] PostQuery query)
        {
            var posts = await _postService.GetAllGastroPosts(query);
            return await Task.FromResult(Ok(posts));
        }
        [HttpGet("eventposts")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllEvent([FromQuery] PostQuery query)
        {
            var posts = await _postService.GetAllEventPosts(query);
            return await Task.FromResult(Ok(posts));
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PostDto>> GetOne([FromRoute] int id)
        {
            var post = await _postService.GetById(id);
            return await Task.FromResult(Ok(post));
        }
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllToConfirm([FromQuery] PostQuery query)
        {
            var posts = await _postService.GetAllToConfirm(query);
            return await Task.FromResult(Ok(posts));
        }
        [HttpGet("admin/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PostDto>> GetOneToConfirm([FromRoute] int id)
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
        [HttpPost("lfposts")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> CreateLFPost([FromBody] CreateLFPostDto dto)
        {
            var id = await _postService.CreateLFPost(dto);
            return await Task.FromResult(Created($"/api/post/{id}", null));
        }
        [HttpPost("eventposts")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> CreateEventPost([FromBody] CreateEventPostDto dto)
        {
            var id = await _postService.CreateEventPost(dto);
            return await Task.FromResult(Created($"/api/post/{id}", null));
        }
        [HttpPost("gastroposts")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> CreateGastroPost([FromBody] CreateGastroPostDto dto)
        {
            var id = await _postService.CreateGastroPost(dto);
            return await Task.FromResult(Created($"/api/post/{id}", null));
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _postService.Delete(id);
            return await Task.FromResult(NoContent());
        }
        [HttpPut("lfposts/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> UpdateLFPost([FromBody] EditLFPostDto modifyPostDto, [FromRoute] int id)
        {
            await _postService.UpdateLFPost(modifyPostDto, id);
            return await Task.FromResult(Ok());
        }
        [HttpPut("eventposts/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> UpdateEventPost([FromBody] EditEventPostDto modifyPostDto, [FromRoute] int id)
        {
            await _postService.UpdateEventPost(modifyPostDto, id);
            return await Task.FromResult(Ok());
        }
        [HttpPut("gastroposts/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> UpdateGstroPost([FromBody] EditGastroPostDto modifyPostDto, [FromRoute] int id)
        {
            await _postService.UpdateGastroPost(modifyPostDto, id);
            return await Task.FromResult(Ok());
        }
        [HttpGet("gastrotags")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<GastroTagDto>>> GetAllGastroTags()
        {
            var tags = await _postService.GetAllGastroTags();
            return await Task.FromResult(Ok(tags));
        }
        [HttpGet("eventtags")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<EventTagDto>>> GetAllEventTags()
        {
            var tags = await _postService.GetAllEventTags();
            return await Task.FromResult(Ok(tags));
        }
        [HttpGet("lftags")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<LFTagDto>>> GetAllLFTags()
        {
            var tags = await _postService.GetAllLFTags();
            return await Task.FromResult(Ok(tags));
        }
        [HttpGet("userposts")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllUserPosts()
        {
            var posts = await _postService.GetAllUserPosts();
            return await Task.FromResult(Ok(posts));
        }
    }
}
