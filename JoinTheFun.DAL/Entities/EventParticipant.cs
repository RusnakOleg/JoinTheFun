using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Entities
{
    public class EventParticipant
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Status { get; set; } // going / interested
    }

}
