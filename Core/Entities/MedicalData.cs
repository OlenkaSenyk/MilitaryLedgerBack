using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MedicalData : BaseEntity
    {
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
        public bool Eligibility { get; set; }
        public string Features { get; set; }
        public string? Notes { get; set; }

        public int PersonId { get; set; }
    }
}
