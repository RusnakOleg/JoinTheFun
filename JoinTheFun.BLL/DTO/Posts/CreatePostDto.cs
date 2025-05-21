using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.DTO.Posts
{
    public class CreatePostDto
    {
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        public string UserId { get; set; } // Додано
    }

}
