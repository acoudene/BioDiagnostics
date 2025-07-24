using Microsoft.EntityFrameworkCore;

namespace BioDiagnostics.Data.EFCore.MongoDb.DbContexts;

public class BioDiagnosticsDbContextBuilder
{
  private readonly string _connectionString;
  private string? _databaseName;

  public BioDiagnosticsDbContextBuilder(string connectionString)
  {
    if (string.IsNullOrWhiteSpace(connectionString))
      throw new ArgumentNullException(nameof(connectionString));

    _connectionString = connectionString;
  }

  public BioDiagnosticsDbContextBuilder WithDatabaseName(string databaseName)
  {
    if (string.IsNullOrWhiteSpace(databaseName))
      throw new ArgumentException(nameof(databaseName));

    _databaseName = databaseName;
    return this;
  }

  public BioDiagnosticsDbContext Build()
  {
    if (string.IsNullOrWhiteSpace(_databaseName))
      throw new ArgumentException(nameof(_databaseName));

    var options = new DbContextOptionsBuilder<BioDiagnosticsDbContext>()
      .UseMongoDB(
      _connectionString,
      _databaseName)
      .Options;
    return new BioDiagnosticsDbContext(options);
  }
}
