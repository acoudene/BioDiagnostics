// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Core.Data.MongoDb;
using BioDiagnostics.Data.MongoDb.Repositories;
using BioDiagnostics.Data.Repositories;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace BioDiagnostics.Host;

public static class ServiceCollectionsExtensions
{
  public static void AddDataAdapters(this IServiceCollection serviceCollection)
  {
    try
    {
      BsonSerializer.TryRegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
      BsonSerializer.TryRegisterSerializer(typeof(DateTimeOffset), new DateTimeOffsetSerializer(BsonType.DateTime));
    }
    catch (BsonSerializationException ex)
    {
      // Just to let integration tests work
      Console.WriteLine(ex);
    }

    serviceCollection.AddScoped<IMongoContext, MongoContext>();
    serviceCollection.AddScoped<IRequestToBeReviewedRepository, RequestToBeReviewedRepository>();
  }

  public static void ConfigureDataAdapters(this IServiceCollection serviceCollection, IConfiguration configuration, string? forcedConnectionString)
  {
    /// Connexion strings
    serviceCollection.Configure<DatabaseSettings>(configuration);
    serviceCollection.Configure<DatabaseSettings>(options => options.ConnectionString = forcedConnectionString ?? options.ConnectionString);

    AddDataAdapters(serviceCollection);
  }
}
