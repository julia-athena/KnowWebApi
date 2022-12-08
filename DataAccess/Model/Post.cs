using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model
{
    [Comment("Статьи в базе знаний")]
    public class Post
    {
        public long PostId { get; set; }
        public string Title { get; set; }
        public PostState State { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public Creator FirstCreator { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public bool SoftDeleted { get; set; }
    }
}