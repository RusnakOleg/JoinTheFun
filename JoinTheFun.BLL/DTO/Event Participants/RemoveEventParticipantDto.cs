using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.DTO.Event_Participants
{
    public class RemoveEventParticipantDto
    {
        public int EventId { get; set; }
        public string UserId { get; set; }
    }
}
