using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Content { get; set; }
        public byte[] ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PostLike> Likes { get; set; }
        public ICollection<PostComment> Comments { get; set; }
    }

}
