using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class MedicalDataDTO
    {
        public int Id { get; set; }
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
        public bool Eligibility { get; set; }
        public string Features { get; set; }
        public string? Notes { get; set; }
        public int PersonId { get; set; }
    }
}
