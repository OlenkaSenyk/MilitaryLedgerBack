﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CombatParticipation : BaseEntity
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Location { get; set; }
        public string OperationType { get; set; }

        public int PersonId { get; set; }
    }
}
