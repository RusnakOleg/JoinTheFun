using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.DTO.Posts
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public byte[] ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorUsername { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
    }

}
