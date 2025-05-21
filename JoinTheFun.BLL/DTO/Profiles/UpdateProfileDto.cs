using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.DTO.Profiles
{
    public class UpdateProfileDto
    {
        public string City { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
        public string Gender { get; set; }
        public List<int> InterestIds { get; set; }
    }

}
