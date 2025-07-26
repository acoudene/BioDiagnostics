using BioDiagnostics.Data.EFCore.SqlServer.Entities;
using BioDiagnostics.Data.EFCore.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BioDiagnostics.Data.EFCore.SqlServer.DbContexts;

public class BioDiagnosticsDbContext : DbContext
{
  public DbSet<RequestToBeReviewedSql> RequestToBeRevieweds { get; init; }

  public BioDiagnosticsDbContext(DbContextOptions options)
      : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder
      .Entity<RequestToBeReviewedSql>(e =>
      {
        //e.ToCollection(RequestToBeReviewedRepository.CollectionName);

        e.HasDiscriminator()
        .HasValue<RequestToBeReviewedSql>("requestToBeReviewed");

        var requestedNavigation = e.OwnsMany(p => p.Requesteds);
        requestedNavigation
        .OwnsMany(p => p.Identifiers);
        requestedNavigation
        .OwnsMany(p => p.Codes)
        .OwnsMany(p => p.Codings);
        requestedNavigation
        .OwnsMany(p => p.Observations)        
        .OwnsOne(p => p.Specimen)
        .OwnsMany(p => p.Identifiers);
        
        var patientNavigation = requestedNavigation.OwnsOne(p => p.Patient);
        patientNavigation
        .OwnsMany(p => p.Identifiers);
        patientNavigation
        .OwnsMany(p => p.Names);
      });
  }
}