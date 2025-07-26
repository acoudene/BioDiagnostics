using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BioDiagnostics.Data.EFCore.SqlServer.DbContexts;

public class BioDiagnosticsDbContextBuilder
{
  private readonly string _connectionString;
  private string? _databaseName;
  private LoggerFactory? _loggerFactory;

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

  public BioDiagnosticsDbContextBuilder UseLoggerFactory(LoggerFactory loggerFactory)
  {
    if (loggerFactory is null)
      throw new ArgumentException(nameof(loggerFactory));

    _loggerFactory = loggerFactory;
    return this;
  }

  public DbContextOptions BuildOptions()
  {
    if (string.IsNullOrWhiteSpace(_databaseName))
      throw new ArgumentException(nameof(_databaseName));

    if (_loggerFactory is null)
    {
      return new DbContextOptionsBuilder<BioDiagnosticsDbContext>()
      .UseSqlServer(_connectionString)
      .Options;
    }

    return new DbContextOptionsBuilder<BioDiagnosticsDbContext>()
      .UseSqlServer(_connectionString)
      .UseLoggerFactory(_loggerFactory)
      .EnableSensitiveDataLogging()
      .Options;
  }

  public BioDiagnosticsDbContext Build()
  {
    var options = BuildOptions();
    return new BioDiagnosticsDbContext(options);
  }
}
