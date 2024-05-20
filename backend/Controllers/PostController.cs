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
            return Ok(posts);
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
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllToConfirm()
        {
            var posts = await _postService.GetAllToConfirm();
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
        [HttpDelete("admin/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsAdmin([FromRoute] int id)
        {
            await _postService.DeleteAsAdmin(id);
            return await Task.FromResult(NoContent());
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
        public async Task<ActionResult> UpdateGastroPost([FromBody] EditGastroPostDto modifyPostDto, [FromRoute] int id)
        {
            await _postService.UpdateGastroPost(modifyPostDto, id);
            return await Task.FromResult(Ok());
        }
        [HttpGet("gastrotags")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GastroTagDto>>> GetAllGastroTags()
        {
            var tags = await _postService.GetAllGastroTags();
            return await Task.FromResult(Ok(tags));
        }
        [HttpGet("eventtags")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EventTagDto>>> GetAllEventTags()
        {
            var tags = await _postService.GetAllEventTags();
            return await Task.FromResult(Ok(tags));
        }
        [HttpGet("lftags")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LFTagDto>>> GetAllLFTags()
        {
            var tags = await _postService.GetAllLFTags();
            return await Task.FromResult(Ok(tags));
        }
        [HttpGet("userlfposts")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllUserLFPosts()
        {
            var posts = await _postService.GetAllUserLFPosts();
            return await Task.FromResult(Ok(posts));
        }
        [HttpGet("usereventposts")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllUserEventPosts()
        {
            var posts = await _postService.GetAllUserEventPosts();
            return await Task.FromResult(Ok(posts));
        }
        [HttpGet("usergastroposts")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllUserGastroPosts()
        {
            var posts = await _postService.GetAllUserGastroPosts();
            return await Task.FromResult(Ok(posts));
        }
        [HttpGet("userlikedposts")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllUserLikedPosts()
        {
            var posts = await _postService.GetAllUserLikedPosts();
            return await Task.FromResult(Ok(posts));
        }
        [HttpPost("like/{id}")]
        [Authorize(Roles ="User,Admin")]
        public async Task<ActionResult> LikePost([FromRoute] int id)
        {
            await _postService.LikePost(id);
            return await Task.FromResult(Ok());
        }
    }
}
