using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string DriversLicenseNumber { get; set; }
    }
}
