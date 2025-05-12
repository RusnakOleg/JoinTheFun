using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Entities
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<EventParticipant> Participants { get; set; }
    }

}
