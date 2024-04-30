using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ServiceHistory : BaseEntity
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string MilitaryRank { get; set; }
        public string MilitaryCategory { get; set; }
        public string MilitaryBranch { get; set; }
        public string MilitaryUnit { get; set; }

        public int PersonId { get; set; }
    }
}
