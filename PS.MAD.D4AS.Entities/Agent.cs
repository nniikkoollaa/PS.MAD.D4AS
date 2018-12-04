using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.Entities
{
    public class Agent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
