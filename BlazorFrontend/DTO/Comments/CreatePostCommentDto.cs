using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFrontend.DTO.Comments
{
    public class CreatePostCommentDto
    {
        public int PostId { get; set; }
        public string Content { get; set; }

        public string UserId { get; set; } // Додано
    }

}
