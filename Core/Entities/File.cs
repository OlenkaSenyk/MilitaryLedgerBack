using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class File : BaseEntity
    {
        public byte[] Photo { get; set; }
        public byte[] Passport { get; set; }
        public byte[] IndividualTaxNumber { get; set; }
        public byte[] MedicalCertificate { get; set; }
        public byte[] ResidencePermit { get; set; }

        public int PersonId { get; set; }
    }
}
