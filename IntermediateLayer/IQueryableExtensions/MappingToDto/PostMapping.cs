using System.Linq;
using DataAccess.Extensions;
using IntermediateLayer.KnowDto;
using DataAccess.Model;

namespace IntermediateLayer.IQueryableExtensions.MappingToDto
{
    public static class PostMapping
    {
        public static PostDto? MapToDtoOrDefault(this Post? post)
        {
           if (post is null)
                return null;
           
           var result =  new PostDto()
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                CreatedOn = post.Created,
                CreatedBy = new CreatorDto()
                {
                    CreatorId = post.FirstCreator.CreatorId,
                    Email = post.FirstCreator.Email,
                    FirstName = post.FirstCreator.FirstName,
                    LastName = post.FirstCreator.LastName
                },
                State = post.State.GetDescription(),
                Tags = post.Tags.Select(t => t.TagId).ToList()
            };
            return result;
        }
    }
}