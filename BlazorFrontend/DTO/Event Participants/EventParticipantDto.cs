using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFrontend.DTO.Event_Participants
{
    public class EventParticipantDto
    {
        public int EventId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; } // going / interested
    }

}
