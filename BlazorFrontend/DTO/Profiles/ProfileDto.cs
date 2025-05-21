using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFrontend.DTO.Profiles
{
    public class ProfileDto
    {
        public string UserId { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
        public string Gender { get; set; }
        public List<string> Interests { get; set; }
        public string Username { get; set; }
    }

}
