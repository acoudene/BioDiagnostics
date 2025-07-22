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
  // TODO - Use records instead of class
  // TODO - Don't use Interface for persistance entity for the moment
  // TODO - Use disciminator for inheritance or versioning
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists


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

  /// <summary>
  /// A shared identifier common to all service requests that were authorized more or less simultaneously by a single author, 
  /// representing the composite or group identifier.
  /// </summary>
  /// <see cref="https://hl7.org/fhir/servicerequest-definitions.html#ServiceRequest.requisition"/>"/>
  public string? Requisition { get; set; }

  public List<ServiceRequestMongo> Requested { get; set; } = []; 
}

// This commented part could be used to have benefits of mongo entity typing
// Example of inherited class
//[BsonIgnoreExtraElements]
//[BsonDiscriminator("requestToBeReviewedInherited", Required = true)]
//public record RequestToBeReviewedInheritedMongo : RequestToBeReviewedMongoBase
//{
//}