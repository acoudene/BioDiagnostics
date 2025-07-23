namespace BioDiagnostics.Data.MongoDb.Entities;

[BsonIgnoreExtraElements]
[BsonDiscriminator("V02", Required = true)]
public record RequestToBeReviewedMongoV02 : RequestToBeReviewedMongo
{
  [BsonElement("version")]
  public string Version { get; } = "V02";
}