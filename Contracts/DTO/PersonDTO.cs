using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public string Education { get; set; }
        public string? PublicSpecialty { get; set; }
        public string? Workplace { get; set; }
        public string TRSSC { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? DischargeDate { get; set; }
        public string? DischargeReason { get; set; }
        public byte[] Signature { get; set; }
        public bool NeedMMC { get; set; }
        public DateOnly? LastMMC { get; set; }
        public double? Fine { get; set; }

        public ICollection<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();
    }
}
