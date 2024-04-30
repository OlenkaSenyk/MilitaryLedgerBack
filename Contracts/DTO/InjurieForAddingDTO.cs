using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class InjurieForAddingDTO
    {
        public DateOnly Date { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string MedicalAssistance { get; set; }
        public string? Notes { get; set; }
    }
}
