using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public string Education { get; set; }
        public string EducationLevel { get; set; }
        public string? PublicSpecialty { get; set; }
        public string? Workplace { get; set; }
        public string TRSSC { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? DischargeDate { get; set; }
        public string? DischargeReason { get; set; }
        public string Signature { get; set; }
        public bool NeedMMC { get; set; }
        public DateOnly? LastMMC { get; set; }
        public double? Fine { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Award> Awards { get; set; }
        public ICollection<CombatParticipation> CombatParticipations { get; set; }
        public ICollection<Document> Documents { get; set; }
        public File Files { get; set; }
        public ICollection<Injurie> Injuries { get; set; }
        public MedicalData MedicalData { get; set; }
        public Parameter Parameter { get; set; }
        public ICollection<ServiceHistory> ServiceHistories { get; set; }
    }
}
