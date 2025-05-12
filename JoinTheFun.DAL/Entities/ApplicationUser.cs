using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JoinTheFun.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Profile Profile { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Event> CreatedEvents { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Following { get; set; }
        public ICollection<EventParticipant> ParticipatingEvents { get; set; }
        public ICollection<PostComment> Comments { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
    }

}
