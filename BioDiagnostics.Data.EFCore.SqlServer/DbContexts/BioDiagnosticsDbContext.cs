using BioDiagnostics.Data.EFCore.SqlServer.Entities;
using BioDiagnostics.Data.EFCore.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BioDiagnostics.Data.EFCore.SqlServer.DbContexts;

public class BioDiagnosticsDbContext : DbContext
{
  public DbSet<RequestToBeReviewedMsSql> RequestToBeRevieweds { get; init; }

  public BioDiagnosticsDbContext(DbContextOptions options)
      : base(options)
  {
  }  

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder
      .Entity<RequestToBeReviewedMsSql>(e =>
      {       
        e.HasMany(p => p.Requesteds);     
      });

    modelBuilder
      .Entity<ServiceRequestMsSql>(e =>
      {
        e.HasMany(p => p.Identifiers);
        e.HasMany(p => p.Codes);
        e.HasMany(p => p.Observations);
        e.HasOne(p => p.Patient);
      });

    modelBuilder
      .Entity<CodeableConceptMsSql>(e =>
      {
        e.HasMany(p => p.Codings);
      });

    modelBuilder
      .Entity<ObservationMsSql>(e =>
      {         
        e.HasMany(p => p.Codes);
        e.HasOne(p => p.Specimen);
      });

    modelBuilder
      .Entity<SpecimenMsSql>(e =>
      {
        e.HasMany(p => p.Identifiers);
      });

    modelBuilder
      .Entity<PatientMsSql>(e =>
      {  
        e.HasMany(p => p.Identifiers);
        e.HasMany(p => p.Names);
      });
  }
}