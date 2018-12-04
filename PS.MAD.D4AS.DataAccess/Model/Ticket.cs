using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.DataAccess.Model
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public DateTime DateOfAccident { get; set; }
        public string Description { get; set; }
        public ICollection<Image> Images { get; set; }
        public double Priority { get; set; }
    }
}
