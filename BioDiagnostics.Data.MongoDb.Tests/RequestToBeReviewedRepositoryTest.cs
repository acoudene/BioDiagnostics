using BioDiagnostics.Data.MongoDb.Entities;
using BioDiagnostics.Data.MongoDb.Repositories;
using Core.Data.MongoDb;
using MongoDB.Bson;
using Testcontainers.MongoDb;

namespace BioDiagnostics.Data.MongoDb.Tests;

public class RequestToBeReviewedRepositoryTest : IAsyncLifetime
{
  private readonly MongoDbContainer _mongoDbContainer;
  private const string BaseDataPath = "Data";

  public RequestToBeReviewedRepositoryTest()
  {
    _mongoDbContainer = new MongoDbBuilder()
     .WithImage("mongo:latest")
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

  protected MongoContext GetMongoContext(SeedData seedData)
  {
    var databaseSettings = new DatabaseSettings()
    {
      ConnectionString = seedData.ConnectionString,
      DatabaseName = seedData.DatabaseName
    };
    return new MongoContext(databaseSettings);
  }

  [Theory]
  [ClassData(typeof(SeedDataTheoryData))]
  public async Task GetAllAsync_get_all_entities_from_database(SeedDataBuilder seedDataBuilder)
  {
    // Arrange
    var seedData = DoSeedData(seedDataBuilder);
    var mongoContext = GetMongoContext(seedData);
    var repository = new RequestToBeReviewedRepository(mongoContext);

    // Act
    var entities = await repository.GetAllAsync();

    // Assert
    Assert.NotNull(entities);
    Assert.NotEmpty(entities);
  }

  [Theory]
  [ClassData(typeof(SeedDataTheoryData))]
  public async Task CreateAsync_create_an_entity_into_database(SeedDataBuilder seedDataBuilder)
  {
    // Arrange
    var seedData = DoSeedData(seedDataBuilder);
    var mongoContext = GetMongoContext(seedData);
    var repository = new RequestToBeReviewedRepository(mongoContext);
    var mongoSet = repository.Behavior.MongoSet;

    // Act
    var newEntity = new RequestToBeReviewedMongo
    {
      Id = Guid.NewGuid(),
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow,
      Requested =
      [
        new ServiceRequestMongo
        {
          Identifier =
          [
            new IdentifierMongo() { System = "ServiceRequest_01_Identifier_System_01", Type = "ServiceRequest_01_Identifier_Type_01", Value = "ServiceRequest_01_Identifier_Value_01"},
            new IdentifierMongo() { System = "ServiceRequest_01_Identifier_System_02", Type = "ServiceRequest_01_Identifier_Type_02", Value = "ServiceRequest_01_Identifier_Value_02"}
          ],
          Codes =
          [
            new CodeableConceptMongo()
            {
              Coding =
              [
                new CodingMongo() { Code = "ServiceRequest_01_Codeable_01_Code_01", System = "ServiceRequest_01_Codeable_01_System_01" },
                new CodingMongo() { Code = "ServiceRequest_01_Codeable_01_Code_02", System = "ServiceRequest_01_Codeable_01_System_02" }
                ]
            },
            new CodeableConceptMongo()
            {
              Coding =
              [
                new CodingMongo() { Code = "ServiceRequest_01_Codeable_02_Code_01", System = "ServiceRequest_01_Codeable_02_System_01" },
                new CodingMongo() { Code = "ServiceRequest_01_Codeable_02_Code_02", System = "ServiceRequest_01_Codeable_02_System_02" }
                ]
            },
          ],
          Observations =
          [
            new ObservationMongo()
            {
              Codes =
              [
                new CodeableConceptMongo()
                {
                  Coding =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_01", System = "Observation_01_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_02", System = "Observation_01_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Coding =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_01", System = "Observation_01_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_02", System = "Observation_01_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifier =
                [
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_01", Type = "Specimen_01_Identifier_Type_01", Value = "Specimen_01_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_02", Type = "Specimen_01_Identifier_Type_02", Value = "Specimen_01_Identifier_Value_02" }
                ],
                Note =
                [
                  "Specimen 01 Note 01",
                  "Specimen 01 Note 02",
                ]
              },
              Status = ObservationStatusMongo.Preliminary
            },
            new ObservationMongo()
            {
              Codes =
              [
                new CodeableConceptMongo()
                {
                  Coding =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_01", System = "Observation_02_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_02", System = "Observation_02_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Coding =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_01", System = "Observation_02_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_02", System = "Observation_02_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifier =
                [
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_01", Type = "Specimen_02_Identifier_Type_01", Value = "Specimen_02_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_02", Type = "Specimen_02_Identifier_Type_02", Value = "Specimen_02_Identifier_Value_02" }
                ],
                Note =
                [
                  "Specimen 02 Note 01",
                  "Specimen 02 Note 02",
                ]
              },
              Status = ObservationStatusMongo.Preliminary
            }
          ],
          Patient = new PatientMongo
          {
            Identifier =
            [
              new IdentifierMongo() { System = "Patient_01_Identifier_System_01", Type = "Patient_01_Identifier_Type_01", Value = "Patient_01_Identifier_Value_01" },
              new IdentifierMongo() { System = "Patient_01_Identifier_System_02", Type = "Patient_01_Identifier_Type_02", Value = "Patient_01_Identifier_Value_02" }
            ],

          }
        },
        new ServiceRequestMongo
        {
          Identifier =
          [
            new IdentifierMongo() { System = "ServiceRequest_02_Identifier_System_01", Type = "ServiceRequest_02_Identifier_Type_01", Value = "ServiceRequest_02_Identifier_Value_01"},
            new IdentifierMongo() { System = "ServiceRequest_02_Identifier_System_02", Type = "ServiceRequest_02_Identifier_Type_02", Value = "ServiceRequest_02_Identifier_Value_02"}
          ],
          Codes =
          [
            new CodeableConceptMongo()
            {
              Coding =
              [
                new CodingMongo() { Code = "ServiceRequest_02_Codeable_01_Code_01", System = "ServiceRequest_02_Codeable_01_System_01" },
                new CodingMongo() { Code = "ServiceRequest_02_Codeable_01_Code_02", System = "ServiceRequest_02_Codeable_01_System_02" }
                ]
            },
            new CodeableConceptMongo()
            {
              Coding =
              [
                new CodingMongo() { Code = "ServiceRequest_02_Codeable_02_Code_01", System = "ServiceRequest_02_Codeable_02_System_01" },
                new CodingMongo() { Code = "ServiceRequest_02_Codeable_02_Code_02", System = "ServiceRequest_02_Codeable_02_System_02" }
                ]
            },
          ],
          Observations =
          [
            new ObservationMongo()
            {
              Codes =
              [
                new CodeableConceptMongo()
                {
                  Coding =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_01", System = "Observation_01_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_02", System = "Observation_01_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Coding =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_01", System = "Observation_01_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_02", System = "Observation_01_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifier =
                [
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_01", Type = "Specimen_01_Identifier_Type_01", Value = "Specimen_01_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_02", Type = "Specimen_01_Identifier_Type_02", Value = "Specimen_01_Identifier_Value_02" }
                ],
                Note =
                [
                  "Specimen 01 Note 01",
                  "Specimen 01 Note 02",
                ]
              },
              Status = ObservationStatusMongo.Preliminary
            },
            new ObservationMongo()
            {
              Codes =
              [
                new CodeableConceptMongo()
                {
                  Coding =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_01", System = "Observation_02_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_02", System = "Observation_02_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Coding =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_01", System = "Observation_02_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_02", System = "Observation_02_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifier =
                [
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_01", Type = "Specimen_02_Identifier_Type_01", Value = "Specimen_02_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_02", Type = "Specimen_02_Identifier_Type_02", Value = "Specimen_02_Identifier_Value_02" }
                ],
                Note =
                [
                  "Specimen 02 Note 01",
                  "Specimen 02 Note 02",
                ]
              },
              Status = ObservationStatusMongo.Preliminary
            }
          ],
          Patient = new PatientMongo
          {
            Identifier =
            [
              new IdentifierMongo() { System = "Patient_02_Identifier_System_01", Type = "Patient_02_Identifier_Type_01", Value = "Patient_02_Identifier_Value_01" },
              new IdentifierMongo() { System = "Patient_02_Identifier_System_02", Type = "Patient_02_Identifier_Type_02", Value = "Patient_02_Identifier_Value_02" }
            ],

          }
        }
      ],
      Requisition = "Requisition_01"
    };

    await mongoSet.CreateAsync(newEntity);

    // Assert
    //Assert.NotNull(entities);
    //Assert.NotEmpty(entities);
  }
}