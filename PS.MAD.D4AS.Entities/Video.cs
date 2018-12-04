using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.Entities
{
    public class Video:BaseEntity
    {
        public Guid Id { get; set; }
        public Ticket Ticket { get; set; }
        public byte[] Body { get; set; }
        public PriorityEnum Priority { get; set; }
        public string UserDescription { get; set; }
        public string AnalyserDescription { get; set; }
        public string Name { get; set; }
    }
}
