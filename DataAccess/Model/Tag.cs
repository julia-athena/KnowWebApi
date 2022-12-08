using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model
{
    [Comment("Список тегов")]
    public class Tag
    {
        public string TagId { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}