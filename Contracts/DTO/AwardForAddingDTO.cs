using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class AwardForAddingDTO
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public string Reason { get; set; }
    }
}
