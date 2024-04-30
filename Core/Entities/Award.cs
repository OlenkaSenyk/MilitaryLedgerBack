using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Award : BaseEntity
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public string Reason { get; set; }

        public int PersonId { get; set; }
    }
}
