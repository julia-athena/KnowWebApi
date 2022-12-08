using DataAccess.Context;
using DataAccess.Model;
using IntermediateLayer.KnowDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Controllers;

namespace WebApp.Services.Implementations;

public class TagService
{
    private readonly KnowDbContext _context;
    
    public TagService(KnowDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddToPostAsync(long postId, TagsDto tags)
    {
        var post = await _context.Posts
            .Include(p => p.Tags)
            .SingleAsync(p => p.PostId == postId);
        
        foreach (var tag in tags.Tags)
        {
            var tagToAdd = await _context.Tags
                .SingleOrDefaultAsync(t => t.TagId == tag);

            if (tagToAdd is null)
                tagToAdd = new Tag() { TagId = tag };

            post.Tags.Add(tagToAdd);
        }
        
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteFromPostAsync(long postId, TagsDto tags)
    {
        var post = await _context.Posts
            .Include(p => p.Tags)
            .SingleAsync(p => p.PostId == postId);

        foreach (var tag in tags.Tags)
        {
            var currentTag = post.Tags
                .Single(t => t.TagId == tag);
            post.Tags.Remove(currentTag);
        }
        await _context.SaveChangesAsync();
 
        return true;
    }
}