using DataAccess.Context;
using IntermediateLayer.KnowDto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.Services.Implementations;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly PostService _service;

        public PostController(PostService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetOneAsync(long id)
        {
            var result = await _service.GetByIdAsync(id);

            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            
            if (!result.Any())
                return null;
            
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<PostDto>> CreateOneAsync(PostDtoToCreate post)
        {
            var createdPost = await _service.CreateAsync(post);
            return createdPost;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PostDto>> UpdateOneAsync(long id, PostDtoToUpdate post)
        {
            var updatedPost = await _service.Update(id, post);
            return updatedPost;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteOneAsync(long id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);;
        }
    }
}