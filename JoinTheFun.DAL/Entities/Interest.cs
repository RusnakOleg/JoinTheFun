using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Entities
{
    public class Interest
    {
        [Key]
        public int InterestId { get; set; }
        public string Name { get; set; }

        public ICollection<UserInterest> UserInterests { get; set; }
    }

}
