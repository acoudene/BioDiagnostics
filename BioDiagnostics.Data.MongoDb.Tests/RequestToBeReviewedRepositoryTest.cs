using BioDiagnostics.Data.MongoDb.Entities;
using BioDiagnostics.Data.MongoDb.Repositories;
using Core.Data.MongoDb;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Testcontainers.MongoDb;
using Xunit.Abstractions;

namespace BioDiagnostics.Data.MongoDb.Tests;

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
     .WithPortBinding(27917, 27017)
     .WithCleanUp(true)
     .Build();
  }

  public async Task InitializeAsync()
  {
    await _mongoDbContainer.StartAsync();

    BsonSerializer.TryRegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    BsonSerializer.TryRegisterSerializer(typeof(DateTimeOffset), new DateTimeOffsetSerializer(BsonType.DateTime));
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
    string connectionString = seedData.ConnectionString;
    string databaseName = seedData.DatabaseName;
    
    var settings = MongoClientSettings.FromConnectionString(connectionString);
    settings.ClusterConfigurator = cb =>
    {
      cb.Subscribe<CommandStartedEvent>(e =>
      {
        _output.WriteLine($"MongoDB Command: {e.CommandName} - {e.Command.ToJson()}");
      });
    };
    var client = new MongoClient(settings);
    return new MongoContext(databaseName, client);
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
      Requesteds =
      [
        new ServiceRequestMongo
        {
          Identifiers =
          [
            new IdentifierMongo() { System = "ServiceRequest_01_Identifier_System_01", Type = "ServiceRequest_01_Identifier_Type_01", Value = "ServiceRequest_01_Identifier_Value_01"},
            new IdentifierMongo() { System = "ServiceRequest_01_Identifier_System_02", Type = "ServiceRequest_01_Identifier_Type_02", Value = "ServiceRequest_01_Identifier_Value_02"}
          ],
          Codes =
          [
            new CodeableConceptMongo()
            {
              Codings =
              [
                new CodingMongo() { Code = "ServiceRequest_01_Codeable_01_Code_01", System = "ServiceRequest_01_Codeable_01_System_01" },
                new CodingMongo() { Code = "ServiceRequest_01_Codeable_01_Code_02", System = "ServiceRequest_01_Codeable_01_System_02" }
                ]
            },
            new CodeableConceptMongo()
            {
              Codings =
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
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_01", System = "Observation_01_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_02", System = "Observation_01_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_01", System = "Observation_01_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_02", System = "Observation_01_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifiers =
                [
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_01", Type = "Specimen_01_Identifier_Type_01", Value = "Specimen_01_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_02", Type = "Specimen_01_Identifier_Type_02", Value = "Specimen_01_Identifier_Value_02" }
                ],
                Notes =
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
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_01", System = "Observation_02_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_02", System = "Observation_02_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_01", System = "Observation_02_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_02", System = "Observation_02_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifiers =
                [
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_01", Type = "Specimen_02_Identifier_Type_01", Value = "Specimen_02_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_02", Type = "Specimen_02_Identifier_Type_02", Value = "Specimen_02_Identifier_Value_02" }
                ],
                Notes =
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
            Identifiers =
            [
              new IdentifierMongo() { System = "Patient_01_Identifier_System_01", Type = "Patient_01_Identifier_Type_01", Value = "Patient_01_Identifier_Value_01" },
              new IdentifierMongo() { System = "Patient_01_Identifier_System_02", Type = "Patient_01_Identifier_Type_02", Value = "Patient_01_Identifier_Value_02" }
            ],

          }
        },
        new ServiceRequestMongo
        {
          Identifiers =
          [
            new IdentifierMongo() { System = "ServiceRequest_02_Identifier_System_01", Type = "ServiceRequest_02_Identifier_Type_01", Value = "ServiceRequest_02_Identifier_Value_01"},
            new IdentifierMongo() { System = "ServiceRequest_02_Identifier_System_02", Type = "ServiceRequest_02_Identifier_Type_02", Value = "ServiceRequest_02_Identifier_Value_02"}
          ],
          Codes =
          [
            new CodeableConceptMongo()
            {
              Codings =
              [
                new CodingMongo() { Code = "ServiceRequest_02_Codeable_01_Code_01", System = "ServiceRequest_02_Codeable_01_System_01" },
                new CodingMongo() { Code = "ServiceRequest_02_Codeable_01_Code_02", System = "ServiceRequest_02_Codeable_01_System_02" }
                ]
            },
            new CodeableConceptMongo()
            {
              Codings =
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
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_01", System = "Observation_01_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_02", System = "Observation_01_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_01", System = "Observation_01_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_02", System = "Observation_01_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifiers =
                [
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_01", Type = "Specimen_01_Identifier_Type_01", Value = "Specimen_01_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_02", Type = "Specimen_01_Identifier_Type_02", Value = "Specimen_01_Identifier_Value_02" }
                ],
                Notes =
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
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_01", System = "Observation_02_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_02", System = "Observation_02_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_01", System = "Observation_02_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_02", System = "Observation_02_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifiers =
                [
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_01", Type = "Specimen_02_Identifier_Type_01", Value = "Specimen_02_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_02", Type = "Specimen_02_Identifier_Type_02", Value = "Specimen_02_Identifier_Value_02" }
                ],
                Notes =
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
            Identifiers =
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
    var entities = await mongoSet.GetAllAsync();

    // Assert
    Assert.NotNull(entities);
    Assert.True(entities.Count == 2);
  }

  [Theory]
  [ClassData(typeof(SeedDataTheoryData))]
  public async Task CreateAsync_create_a_V02_entity_into_database(SeedDataBuilder seedDataBuilder)
  {
    // Arrange
    var seedData = DoSeedData(seedDataBuilder);
    var mongoContext = GetMongoContext(seedData);
    var repository = new RequestToBeReviewedRepositoryV02(mongoContext);
    var mongoSet = repository.Behavior.MongoSet;

    // Act
    var newEntity = new RequestToBeReviewedMongoV02
    {
      Id = Guid.NewGuid(),
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow,
      Requesteds =
      [
        new ServiceRequestMongo
        {
          Identifiers =
          [
            new IdentifierMongo() { System = "ServiceRequest_01_Identifier_System_01", Type = "ServiceRequest_01_Identifier_Type_01", Value = "ServiceRequest_01_Identifier_Value_01"},
            new IdentifierMongo() { System = "ServiceRequest_01_Identifier_System_02", Type = "ServiceRequest_01_Identifier_Type_02", Value = "ServiceRequest_01_Identifier_Value_02"}
          ],
          Codes =
          [
            new CodeableConceptMongo()
            {
              Codings =
              [
                new CodingMongo() { Code = "ServiceRequest_01_Codeable_01_Code_01", System = "ServiceRequest_01_Codeable_01_System_01" },
                new CodingMongo() { Code = "ServiceRequest_01_Codeable_01_Code_02", System = "ServiceRequest_01_Codeable_01_System_02" }
                ]
            },
            new CodeableConceptMongo()
            {
              Codings =
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
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_01", System = "Observation_01_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_02", System = "Observation_01_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_01", System = "Observation_01_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_02", System = "Observation_01_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifiers =
                [
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_01", Type = "Specimen_01_Identifier_Type_01", Value = "Specimen_01_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_02", Type = "Specimen_01_Identifier_Type_02", Value = "Specimen_01_Identifier_Value_02" }
                ],
                Notes =
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
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_01", System = "Observation_02_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_02", System = "Observation_02_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_01", System = "Observation_02_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_02", System = "Observation_02_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifiers =
                [
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_01", Type = "Specimen_02_Identifier_Type_01", Value = "Specimen_02_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_02", Type = "Specimen_02_Identifier_Type_02", Value = "Specimen_02_Identifier_Value_02" }
                ],
                Notes =
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
            Identifiers =
            [
              new IdentifierMongo() { System = "Patient_01_Identifier_System_01", Type = "Patient_01_Identifier_Type_01", Value = "Patient_01_Identifier_Value_01" },
              new IdentifierMongo() { System = "Patient_01_Identifier_System_02", Type = "Patient_01_Identifier_Type_02", Value = "Patient_01_Identifier_Value_02" }
            ],

          }
        },
        new ServiceRequestMongo
        {
          Identifiers =
          [
            new IdentifierMongo() { System = "ServiceRequest_02_Identifier_System_01", Type = "ServiceRequest_02_Identifier_Type_01", Value = "ServiceRequest_02_Identifier_Value_01"},
            new IdentifierMongo() { System = "ServiceRequest_02_Identifier_System_02", Type = "ServiceRequest_02_Identifier_Type_02", Value = "ServiceRequest_02_Identifier_Value_02"}
          ],
          Codes =
          [
            new CodeableConceptMongo()
            {
              Codings =
              [
                new CodingMongo() { Code = "ServiceRequest_02_Codeable_01_Code_01", System = "ServiceRequest_02_Codeable_01_System_01" },
                new CodingMongo() { Code = "ServiceRequest_02_Codeable_01_Code_02", System = "ServiceRequest_02_Codeable_01_System_02" }
                ]
            },
            new CodeableConceptMongo()
            {
              Codings =
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
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_01", System = "Observation_01_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_01_Code_02", System = "Observation_01_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_01", System = "Observation_01_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_01_Codeable_02_Code_02", System = "Observation_01_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifiers =
                [
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_01", Type = "Specimen_01_Identifier_Type_01", Value = "Specimen_01_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_01_Identifier_System_02", Type = "Specimen_01_Identifier_Type_02", Value = "Specimen_01_Identifier_Value_02" }
                ],
                Notes =
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
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_01", System = "Observation_02_Codeable_01_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_01_Code_02", System = "Observation_02_Codeable_01_System_02" }
                    ]
                },
                new CodeableConceptMongo()
                {
                  Codings =
                  [
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_01", System = "Observation_02_Codeable_02_System_01" },
                    new CodingMongo() { Code = "Observation_02_Codeable_02_Code_02", System = "Observation_02_Codeable_02_System_02" }
                    ]
                },
              ],
              Specimen = new SpecimenMongo()
              {
                Identifiers =
                [
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_01", Type = "Specimen_02_Identifier_Type_01", Value = "Specimen_02_Identifier_Value_01" },
                  new IdentifierMongo() { System = "Specimen_02_Identifier_System_02", Type = "Specimen_02_Identifier_Type_02", Value = "Specimen_02_Identifier_Value_02" }
                ],
                Notes =
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
            Identifiers =
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
    var entities = await mongoSet.GetAllAsync();

    // Assert
    Assert.NotNull(entities);
    Assert.True(entities.Count == 2);
  }
}