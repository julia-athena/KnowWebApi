using System;
using System.Collections.Generic;
using DataAccess.Model;
using IntermediateLayer.KnowDto;

namespace IntermediateLayer.KnowDto
{
    public class PostDto
    {
        public long PostId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public CreatorDto CreatedBy { get; set; }
        public string State { get; set; }
        public string Content { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}