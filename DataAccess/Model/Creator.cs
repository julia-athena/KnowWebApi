using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model
{
    [Comment("Пользователи базы знаний")]
    public class Creator
    {
        public long CreatorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Post> CreatedPosts { get; set; }
        public bool SoftDeleted { get; set; }
    }
}