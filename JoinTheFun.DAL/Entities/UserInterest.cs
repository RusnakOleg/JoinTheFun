using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Entities
{
    public class UserInterest
    {
        public int InterestId { get; set; }
        public Interest Interest { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }

}
