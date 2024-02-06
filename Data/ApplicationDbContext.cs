using AgendaExamHAB.Helpers;
using AgendaExamHAB.Models;
using AgendaExamHAB.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgendaExamHAB.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly ICurrentUserServices currentUserServices;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserServices currentUserServices)
            : base(options)
        {
            this.currentUserServices = currentUserServices ?? throw new ArgumentNullException(nameof(currentUserServices));
        }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonContact> PersonContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Dim_Person");
            modelBuilder.Entity<PersonContact>().ToTable("Udt_Person_Contact");
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditProcess();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void AuditProcess()
        {
            var CurrentTime = DateTimeOffset.UtcNow;
            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is Audit))
            {
                var audit = item.Entity as Audit;
                audit.Created = CurrentTime;
                audit.CreatedBy = currentUserServices.GetCurrentUserName();
                audit.Modified = CurrentTime;
                audit.ModifiedBy = currentUserServices.GetCurrentUserName();
            }

            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is Audit))
            {
                var audit = item.Entity as Audit;
                audit.Modified = CurrentTime;
                audit.ModifiedBy = currentUserServices.GetCurrentUserName();
                item.Property(nameof(audit.Created)).IsModified = false;
                item.Property(nameof(audit.CreatedBy)).IsModified = false;
            }
        }

    }

}
