using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Injurie : BaseEntity
    {
        public DateOnly Date { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string MedicalAssistance { get; set; }
        public string? Notes { get; set; }

        public int PersonId { get; set; }
    }
}
