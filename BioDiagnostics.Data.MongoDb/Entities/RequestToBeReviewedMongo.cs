// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Core.Data;

namespace BioDiagnostics.Data.MongoDb.Entities;

// This commented part could be used to have benefits of mongo entity typing
//[BsonIgnoreExtraElements]
//[BsonDiscriminator("requestToBeReviewed", Required = true, RootClass = true)]
//[BsonKnownTypes(typeof(RequestToBeReviewedInheritedMongo))]
//public record RequestToBeReviewedMongoBase : IIdentifierMongoEntity

[BsonIgnoreExtraElements]
[BsonDiscriminator("requestToBeReviewed", Required = true)]
public record RequestToBeReviewedMongo : IIdentifierMongoEntity, ITimestampedMongoEntity
{
  [BsonId]
  [BsonElement("_id")]
  [BsonRepresentation(representation: BsonType.ObjectId)]
  [BsonIgnoreIfDefault]
  public ObjectId ObjectId { get; set; }

  [BsonElement("uuid")]
  [BsonGuidRepresentation(GuidRepresentation.Standard)]
  public required Guid Id { get; set; }

  [BsonElement("createdAt")]
  [BsonRepresentation(representation: BsonType.DateTime)]
  public DateTimeOffset CreatedAt { get; set; }

  [BsonElement("updatedAt")]
  [BsonRepresentation(representation: BsonType.DateTime)]
  public DateTimeOffset UpdatedAt { get; set; }

  // TODO - EntityProperties - Fields to complete

  [BsonElement("metadata")]
  public string? Metadata { get; set; } // Example, to remove if needed
}

// This commented part could be used to have benefits of mongo entity typing
// Example of inherited class
//[BsonIgnoreExtraElements]
//[BsonDiscriminator("requestToBeReviewedInherited", Required = true)]
//public record RequestToBeReviewedInheritedMongo : RequestToBeReviewedMongoBase
//{
//}