using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFrontend.DTO.Events
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public string ImageUrl { get; set; }
        public string CreatorUsername { get; set; }
        public int ParticipantCount { get; set; }
        public string CreatorId { get; set; }
    }

}
