using BioDiagnostics.Data.EFCore.SqlServer.DbContexts;
using BioDiagnostics.Data.EFCore.SqlServer.Entities;
using BioDiagnostics.Data.EFCore.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Testcontainers.MsSql;
using Xunit.Abstractions;

namespace BioDiagnostics.Data.EFCore.SqlServer.Tests;

public class RequestToBeReviewedRepositoryTest : IAsyncLifetime
{
  private readonly MsSqlContainer _dbContainer;
  private const string BaseDataPath = "Data";
  private readonly ITestOutputHelper _output;

  public RequestToBeReviewedRepositoryTest(ITestOutputHelper output)
  {
    _output = output;
    _dbContainer = new MsSqlBuilder()     
     .WithCleanUp(true)
     .Build();
  }

  public async Task InitializeAsync()
  {
    await _dbContainer.StartAsync();
  }

  public async Task DisposeAsync()
  {
    await _dbContainer.StopAsync();
  }

  //protected SeedData DoSeedData(SeedDataBuilder seedDataBuilder)
  //{
  //  string connectionString = _dbContainer.GetConnectionString();
  //  seedDataBuilder.WithConnectionString(connectionString);
  //  var seedData = seedDataBuilder.Build();
  //  string databaseName = seedData.DatabaseName;
  //  SeedDataHelper.ImportDataByCollection<BsonDocument>(
  //    connectionString,
  //    databaseName,
  //    seedData.CollectionName,
  //    Path.Combine(BaseDataPath, seedData.FileName));

  //  return seedData;
  //}

  private BioDiagnosticsDbContextBuilder CreateDbContextBuilder()
    => new BioDiagnosticsDbContextBuilder(_dbContainer.GetConnectionString())
    .UseLoggerFactory(new LoggerFactory(new[] { new TestOutputLoggerProvider(_output) }));

  //[Theory]
  //[ClassData(typeof(SeedDataTheoryData))]
  [Fact]
  public async Task GetAllAsync_get_all_entities_from_database(/*SeedDataBuilder seedDataBuilder*/)
  {
    // Arrange
    //var seedData = DoSeedData(seedDataBuilder);
    using var dbContext = CreateDbContextBuilder()
      //.WithDatabaseName(seedData.DatabaseName)
      .WithDatabaseName(_dbContainer.GetConnectionString())
      .Build();
    await dbContext.Database.EnsureCreatedAsync();
    var repository = new RequestToBeReviewedRepository(dbContext);

    // Act
    var entities = await repository.GetAllAsync();

    // Assert
    Assert.NotNull(entities);
  }

  //[Theory]
  //[ClassData(typeof(SeedDataTheoryData))]
  [Fact]
  public async Task CreateAsync_create_an_entity_into_database(/*SeedDataBuilder seedDataBuilder*/)
  {
    // Arrange
    //var seedData = DoSeedData(seedDataBuilder);
    using var dbContext = CreateDbContextBuilder()
      //.WithDatabaseName(seedData.DatabaseName)
      .WithDatabaseName(_dbContainer.GetConnectionString())
      .Build();
    //var repository = new RequestToBeReviewedRepository(dbContext);
    await dbContext.Database.EnsureCreatedAsync();

    // Act
    var newEntity = new RequestToBeReviewedMsSql
    {
      Id = Guid.NewGuid(),
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow,
      Requesteds =
      [
        new ServiceRequestMsSql
        {
          Id = Guid.NewGuid(),
          Identifiers =
          [
            new IdentifierMsSql() { Id = Guid.NewGuid(), System = "ServiceRequest_01_Identifier_System_01", Type = "ServiceRequest_01_Identifier_Type_01", Value = "ServiceRequest_01_Identifier_Value_01"},
            new IdentifierMsSql() { Id = Guid.NewGuid(), System = "ServiceRequest_01_Identifier_System_02", Type = "ServiceRequest_01_Identifier_Type_02", Value = "ServiceRequest_01_Identifier_Value_02"}
          ],
          Codes =
          [
            new CodeableConceptMsSql()
            {
              Id = Guid.NewGuid(),
              Codings =
              [
                new CodingMsSql() { Id = Guid.NewGuid(), Code = "ServiceRequest_01_Codeable_01_Code_01", System = "ServiceRequest_01_Codeable_01_System_01" },
                new CodingMsSql() { Id = Guid.NewGuid(), Code = "ServiceRequest_01_Codeable_01_Code_02", System = "ServiceRequest_01_Codeable_01_System_02" }
                ]
            },
            new CodeableConceptMsSql()
            {
              Id = Guid.NewGuid(),
              Codings =
              [
                new CodingMsSql() { Id = Guid.NewGuid(), Code = "ServiceRequest_01_Codeable_02_Code_01", System = "ServiceRequest_01_Codeable_02_System_01" },
                new CodingMsSql() { Id = Guid.NewGuid(), Code = "ServiceRequest_01_Codeable_02_Code_02", System = "ServiceRequest_01_Codeable_02_System_02" }
                ]
            },
          ],
          Observations =
          [
            new ObservationMsSql()
            {
              Id = Guid.NewGuid(),
              Codes =
              [
                new CodeableConceptMsSql()
                {
                  Id = Guid.NewGuid(),
                  Codings =
                  [
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_01_Codeable_01_Code_01", System = "Observation_01_Codeable_01_System_01" },
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_01_Codeable_01_Code_02", System = "Observation_01_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMsSql()
                {
                  Id = Guid.NewGuid(),
                  Codings =
                  [
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_01_Codeable_02_Code_01", System = "Observation_01_Codeable_02_System_01" },
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_01_Codeable_02_Code_02", System = "Observation_01_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMsSql()
              {
                Id = Guid.NewGuid(),
                Identifiers =
                [
                  new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Specimen_01_Identifier_System_01", Type = "Specimen_01_Identifier_Type_01", Value = "Specimen_01_Identifier_Value_01" },
                  new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Specimen_01_Identifier_System_02", Type = "Specimen_01_Identifier_Type_02", Value = "Specimen_01_Identifier_Value_02" }
                ],
                Notes =
                [
                  "Specimen 01 Note 01",
                  "Specimen 01 Note 02",
                ]
              },
              Status = ObservationStatusMsSql.Preliminary
            },
            new ObservationMsSql()
            {
              Id = Guid.NewGuid(),
              Codes =
              [
                new CodeableConceptMsSql()
                {
                  Id = Guid.NewGuid(),
                  Codings =
                  [
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_02_Codeable_01_Code_01", System = "Observation_02_Codeable_01_System_01" },
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_02_Codeable_01_Code_02", System = "Observation_02_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMsSql()
                {
                  Id = Guid.NewGuid(),
                  Codings =
                  [
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_02_Codeable_02_Code_01", System = "Observation_02_Codeable_02_System_01" },
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_02_Codeable_02_Code_02", System = "Observation_02_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMsSql()
              {
                Id = Guid.NewGuid(),
                Identifiers =
                [
                  new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Specimen_02_Identifier_System_01", Type = "Specimen_02_Identifier_Type_01", Value = "Specimen_02_Identifier_Value_01" },
                  new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Specimen_02_Identifier_System_02", Type = "Specimen_02_Identifier_Type_02", Value = "Specimen_02_Identifier_Value_02" }
                ],
                Notes =
                [
                  "Specimen 02 Note 01",
                  "Specimen 02 Note 02",
                ]
              },
              Status = ObservationStatusMsSql.Preliminary
            }
          ],
          Patient = new PatientMsSql
          {
            Id = Guid.NewGuid(),
            Identifiers =
            [
              new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Patient_01_Identifier_System_01", Type = "Patient_01_Identifier_Type_01", Value = "Patient_01_Identifier_Value_01" },
              new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Patient_01_Identifier_System_02", Type = "Patient_01_Identifier_Type_02", Value = "Patient_01_Identifier_Value_02" }
            ],

          }
        },
        new ServiceRequestMsSql
        {
          Id = Guid.NewGuid(),
          Identifiers =
          [
            new IdentifierMsSql() { Id = Guid.NewGuid(), System = "ServiceRequest_02_Identifier_System_01", Type = "ServiceRequest_02_Identifier_Type_01", Value = "ServiceRequest_02_Identifier_Value_01"},
            new IdentifierMsSql() { Id = Guid.NewGuid(), System = "ServiceRequest_02_Identifier_System_02", Type = "ServiceRequest_02_Identifier_Type_02", Value = "ServiceRequest_02_Identifier_Value_02"}
          ],
          Codes =
          [
            new CodeableConceptMsSql()
            {
              Id = Guid.NewGuid(),
              Codings =
              [
                new CodingMsSql() { Id = Guid.NewGuid(), Code = "ServiceRequest_02_Codeable_01_Code_01", System = "ServiceRequest_02_Codeable_01_System_01" },
                new CodingMsSql() { Id = Guid.NewGuid(), Code = "ServiceRequest_02_Codeable_01_Code_02", System = "ServiceRequest_02_Codeable_01_System_02" }
                ]
            },
            new CodeableConceptMsSql()
            {
              Id = Guid.NewGuid(),
              Codings =
              [
                new CodingMsSql() { Id = Guid.NewGuid(), Code = "ServiceRequest_02_Codeable_02_Code_01", System = "ServiceRequest_02_Codeable_02_System_01" },
                new CodingMsSql() { Id = Guid.NewGuid(), Code = "ServiceRequest_02_Codeable_02_Code_02", System = "ServiceRequest_02_Codeable_02_System_02" }
                ]
            },
          ],
          Observations =
          [
            new ObservationMsSql()
            {
              Id = Guid.NewGuid(),
              Codes =
              [
                new CodeableConceptMsSql()
                {
                  Id = Guid.NewGuid(),
                  Codings =
                  [
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_01_Codeable_01_Code_01", System = "Observation_01_Codeable_01_System_01" },
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_01_Codeable_01_Code_02", System = "Observation_01_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMsSql()
                {
                  Id = Guid.NewGuid(),
                  Codings =
                  [
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_01_Codeable_02_Code_01", System = "Observation_01_Codeable_02_System_01" },
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_01_Codeable_02_Code_02", System = "Observation_01_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMsSql()
              {
                Id = Guid.NewGuid(),
                Identifiers =
                [
                  new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Specimen_01_Identifier_System_01", Type = "Specimen_01_Identifier_Type_01", Value = "Specimen_01_Identifier_Value_01" },
                  new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Specimen_01_Identifier_System_02", Type = "Specimen_01_Identifier_Type_02", Value = "Specimen_01_Identifier_Value_02" }
                ],
                Notes =
                [
                  "Specimen 01 Note 01",
                  "Specimen 01 Note 02",
                ]
              },
              Status = ObservationStatusMsSql.Preliminary
            },
            new ObservationMsSql()
            {
              Id = Guid.NewGuid(),
              Codes =
              [
                new CodeableConceptMsSql()
                {
                  Id = Guid.NewGuid(),
                  Codings =
                  [
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_02_Codeable_01_Code_01", System = "Observation_02_Codeable_01_System_01" },
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_02_Codeable_01_Code_02", System = "Observation_02_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMsSql()
                {
                  Id = Guid.NewGuid(),
                  Codings =
                  [
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_02_Codeable_02_Code_01", System = "Observation_02_Codeable_02_System_01" },
                    new CodingMsSql() { Id = Guid.NewGuid(), Code = "Observation_02_Codeable_02_Code_02", System = "Observation_02_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMsSql()
              {
                Id = Guid.NewGuid(),
                Identifiers =
                [
                  new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Specimen_02_Identifier_System_01", Type = "Specimen_02_Identifier_Type_01", Value = "Specimen_02_Identifier_Value_01" },
                  new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Specimen_02_Identifier_System_02", Type = "Specimen_02_Identifier_Type_02", Value = "Specimen_02_Identifier_Value_02" }
                ],
                Notes =
                [
                  "Specimen 02 Note 01",
                  "Specimen 02 Note 02",
                ]
              },
              Status = ObservationStatusMsSql.Preliminary
            }
          ],
          Patient = new PatientMsSql
          {
            Id = Guid.NewGuid(),
            Identifiers =
            [
              new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Patient_02_Identifier_System_01", Type = "Patient_02_Identifier_Type_01", Value = "Patient_02_Identifier_Value_01" },
              new IdentifierMsSql() { Id = Guid.NewGuid(), System = "Patient_02_Identifier_System_02", Type = "Patient_02_Identifier_Type_02", Value = "Patient_02_Identifier_Value_02" }
            ],

          }
        }
      ],
      Requisition = "Requisition_01"
    };

    var requestToBeRevieweds = dbContext.RequestToBeRevieweds;
    await requestToBeRevieweds.AddAsync(newEntity);

    await dbContext.SaveChangesAsync();

    var entities = await requestToBeRevieweds.ToListAsync();

    // Assert
    Assert.NotNull(entities);
    Assert.NotEmpty(entities);
  }
}