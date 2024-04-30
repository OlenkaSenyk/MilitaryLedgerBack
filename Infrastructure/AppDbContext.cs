using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Entities.File;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<CombatParticipation> CombatParticipations { get; set; }
        public DbSet<ServiceHistory> ServiceHistories { get; set; }
        public DbSet<Injurie> Injuries { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<MedicalData> MedicalDatas { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
