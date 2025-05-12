using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Entities
{
    public class PostLike
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

}
