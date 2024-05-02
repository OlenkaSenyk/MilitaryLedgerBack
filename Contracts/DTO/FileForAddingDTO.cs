using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class FileForAddingDTO
    {
        public string Photo { get; set; }
        public string Passport { get; set; }
        public string IndividualTaxNumber { get; set; }
        public string MedicalCertificate { get; set; }
        public string ResidencePermit { get; set; }
    }
}
