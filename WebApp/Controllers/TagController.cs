using IntermediateLayer.KnowDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services.Implementations;

namespace WebApp.Controllers;

[ApiController]
[Authorize]
public class TagController : ControllerBase
{
    private readonly TagService _service;
    
    public TagController(TagService service)
    {
        _service = service;
    }
    
    [HttpPost("/api/posts/{postId}/tags")]
    public async Task<ActionResult<bool>> AddTagsToPost(long postId, TagsDto tags)
    {
        var result = await _service.AddToPostAsync(postId, tags);
        return result;
    }
    
    [HttpDelete("/api/posts/{postId}/tags")]
    public async Task<ActionResult<bool>> RemoveTagsFromPost(long postId, TagsDto tags)
    {
        var result = await _service.DeleteFromPostAsync(postId, tags);
        return result;
    }
}