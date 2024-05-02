using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string? Region { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string? Entrance { get; set; }
        public string? Apartment { get; set; }
        public string ResidenceOrRegistration { get; set; }

        public int PersonId { get; set; }
    }
}
