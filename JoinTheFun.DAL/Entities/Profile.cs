using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Entities
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int Age { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }

        public Gender Gender { get; set; }

        public ICollection<UserInterest> Interests { get; set; }
    }

}
