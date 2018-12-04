using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.API.Model
{
    public class NewTicketRequest
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
    }
}
