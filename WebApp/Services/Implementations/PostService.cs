using System.Linq;
using System.Transactions;
using DataAccess.Context;
using DataAccess.Model;
using IntermediateLayer.IQueryableExtensions;
using IntermediateLayer.IQueryableExtensions.MappingToDto;
using IntermediateLayer.KnowDto;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Services.Implementations
{
    public class PostService
    {
        private readonly KnowDbContext _context;

        public PostService(KnowDbContext context)
        {
            _context = context;
        }

        public async Task<PostDto> GetByIdAsync(long id)
        {
            var result = await _context.Posts
                .Include(p=> p.FirstCreator)
                .Include(p=>p.Tags)
                .SingleAsync(p => p.PostId == id);

            return result.MapToDtoOrDefault();
        }

        public async Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var entities = await _context.Posts
                .Include(p=> p.FirstCreator)
                .Include(p=>p.Tags)
                .ToListAsync();

            var result = entities.Select(p => p.MapToDtoOrDefault());
                
            return result;
        }

        public IEnumerable<PostDto> GetByPage(PageOptions pageOptions)
        {
            throw new NotImplementedException();
        }

        public async Task<PostDto> CreateAsync(PostDtoToCreate newPost)
        {
            var creator = await _context.Creators
                .SingleAsync(c => c.CreatorId == newPost.CreatorId);

            var entityPost = new Post()
            {
                Title = newPost.Title,
                FirstCreator = creator,
                Content = newPost.Content,
                Tags = new List<Tag>()
            };

            foreach (var tag in newPost.Tags)
            {
                var existingTag = await _context.Tags
                    .SingleOrDefaultAsync(t => t.TagId == tag);
                
                if (existingTag is null)
                    entityPost.Tags.Add(new Tag(){TagId = tag});
                else 
                    entityPost.Tags.Add(existingTag);
            }

            _context.Posts.Add(entityPost);
            await _context.SaveChangesAsync();

            return entityPost.MapToDtoOrDefault();
        }

        public async Task<PostDto> Update(long postId, PostDtoToUpdate postToUpdate)
        {
            var entityPost = await _context.Posts
                .Include(p=>p.FirstCreator)
                .Include(p=>p.Tags)
                .SingleAsync(p => p.PostId == postId);

            entityPost.Title = postToUpdate.Title == "" || postToUpdate.Title is null 
                ? entityPost.Title 
                : postToUpdate.Title;
            
            entityPost.Content = postToUpdate.Content == "" || postToUpdate.Content is null 
                ? entityPost.Content 
                : postToUpdate.Content;

            await _context.SaveChangesAsync();

            return entityPost.MapToDtoOrDefault();
        }

        public async Task<bool> DeleteAsync(long postId)
        {
            var entityPost = await _context.Posts
                .SingleAsync(p => p.PostId == postId);

            entityPost.SoftDeleted = true;
            
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}