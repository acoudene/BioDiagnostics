using BioDiagnostics.Data.EFCore.MongoDb.DbContexts;
using BioDiagnostics.Data.EFCore.MongoDb.Repositories;
using BioDiagnostics.Data.MongoDb.Tests;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Testcontainers.MongoDb;
using Xunit.Abstractions;

namespace BioDiagnostics.Data.EFCore.MongoDb.Tests;

public class RequestToBeReviewedRepositoryTest : IAsyncLifetime
{
  private readonly MongoDbContainer _mongoDbContainer;
  private const string BaseDataPath = "Data";
  private readonly ITestOutputHelper _output;

  public RequestToBeReviewedRepositoryTest(ITestOutputHelper output)
  {
    _output = output;
    _mongoDbContainer = new MongoDbBuilder()
     .WithImage("mongo:latest")
     .WithPortBinding(28917, 27017)
     .WithCleanUp(true)
     .Build();
  }

  public async Task InitializeAsync()
  {
    await _mongoDbContainer.StartAsync();
  }

  public async Task DisposeAsync()
  {
    await _mongoDbContainer.StopAsync();
  }

  protected SeedData DoSeedData(SeedDataBuilder seedDataBuilder)
  {
    string connectionString = _mongoDbContainer.GetConnectionString();
    seedDataBuilder.WithConnectionString(connectionString);
    var seedData = seedDataBuilder.Build();
    string databaseName = seedData.DatabaseName;
    SeedDataHelper.ImportDataByCollection<BsonDocument>(
      connectionString,
      databaseName,
      seedData.CollectionName,
      Path.Combine(BaseDataPath, seedData.FileName));

    return seedData;
  }

  private BioDiagnosticsDbContextBuilder CreateDbContextBuilder()
    => new BioDiagnosticsDbContextBuilder(_mongoDbContainer.GetConnectionString())
    .UseLoggerFactory(new LoggerFactory(new[] { new TestOutputLoggerProvider(_output) }));

  [Theory]
  [ClassData(typeof(SeedDataTheoryData))]
  public async Task GetAllAsync_get_all_entities_from_database(SeedDataBuilder seedDataBuilder)
  {
    // Arrange
    var seedData = DoSeedData(seedDataBuilder);
    using var dbContext = CreateDbContextBuilder()
      .WithDatabaseName(seedData.DatabaseName)
      .Build();
    var repository = new RequestToBeReviewedRepository(dbContext);

    // Act
    var entities = await repository.GetAllAsync();

    // Assert
    Assert.NotNull(entities);
    Assert.NotEmpty(entities);
  }
}