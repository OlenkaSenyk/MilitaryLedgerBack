using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public string Role { get; set; }

        public User CreatedByUser { get; set; }
        public User LastUpdatedByUser { get; set; }
        public ICollection<CalendarEvent> CalendarEvents { get; set; }
        public ICollection<Document> Documents { get; set; }

        public ICollection<Address> CreatedAddresses { get; set; }
        public ICollection<Address> LastUpdatedAddresses { get; set; }
        public ICollection<Award> CreatedAwards { get; set; }
        public ICollection<Award> LastUpdatedAwards { get; set; }
        public ICollection<CalendarEvent> CreatedCalendarEvents { get; set; }
        public ICollection<CalendarEvent> LastUpdatedCalendarEvents { get; set; }
        public ICollection<CombatParticipation> CreatedCombatParticipations { get; set; }
        public ICollection<CombatParticipation> LastUpdatedCombatParticipations { get; set; }
        public ICollection <Document> CreatedDocuments { get; set; }
        public ICollection<Document> LastUpdatedDocuments { get; set; }
        public ICollection<DocumentTemplate> CreatedDocumentTemplates { get; set; }
        public ICollection<DocumentTemplate> LastUpdatedDocumentTemplates { get; set; }
        public ICollection<File> CreatedFiles { get; set; }
        public ICollection<File> LastUpdatedFiles { get; set; }
        public ICollection<Injurie> CreatedInjuries { get; set; }
        public ICollection<Injurie> LastUpdatedInjuries { get; set; }
        public ICollection<MedicalData> CreatedMedicalDatas { get; set; }
        public ICollection<MedicalData> LastUpdatedMedicalDatas { get; set; }
        public ICollection<News> CreatedNews { get; set; }
        public ICollection<News> LastUpdatedNews { get; set; }
        public ICollection<Parameter> CreatedParameters { get; set; }
        public ICollection<Parameter> LastUpdatedParameters { get; set; }
        public ICollection<Person> CreatedPeople { get; set; }
        public ICollection<Person> LastUpdatedPeople { get; set; }
        public ICollection<ServiceHistory> CreatedServiceHistories { get; set; }
        public ICollection<ServiceHistory> LastUpdatedServiceHistories { get; set; }
    }
}
