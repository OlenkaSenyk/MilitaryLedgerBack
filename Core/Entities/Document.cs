using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Document : BaseEntity
    {
        public string Type { get; set; }
        public string Number { get; set; }
        public DateOnly IssueDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public byte[] ScanCopy { get; set; }
        public int UserId { get; set; }
        public int? PersonId { get; set; }
    }
}
