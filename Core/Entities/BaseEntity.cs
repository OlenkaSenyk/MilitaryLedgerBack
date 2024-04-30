﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedById { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public int? LastUpdatedById { get; set; }
    }
}
