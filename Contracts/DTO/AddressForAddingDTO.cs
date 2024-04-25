using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class AddressForAddingDTO
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Entrance { get; set; }
        public int? Apartment { get; set; }
        public string ResidenceOrRegistration { get; set; }
        public int PersonId { get; set; }
    }
}
