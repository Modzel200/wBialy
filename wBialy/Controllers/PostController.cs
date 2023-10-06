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
        public ActionResult<IEnumerable<Post>> GetAll([FromQuery]PostQuery query)
        {
            var posts = _postService.GetAll(query);
            return Ok(posts);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Post> GetOne([FromRoute] int id)
        {
            var post = _postService.GetById(id);
            return Ok(post);
        }
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult CreatePost([FromBody] CreatePostDto dto)
        {
            var id = _postService.Create(dto);
            return Created($"/api/post/{id}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _postService.Delete(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] EditPostDto modifyPostDto, [FromRoute] int id)
        {
            _postService.Update(modifyPostDto, id);
            return Ok();
        }
    }
}
