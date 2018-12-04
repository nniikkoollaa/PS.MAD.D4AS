using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.DataAccess.Model
{
    public class Video
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public double Priority { get; set; }
        public string Name { get; set; }
        public string UserDescription { get; set; }
        public string AnalyserDescription { get; set; }
    }
}
