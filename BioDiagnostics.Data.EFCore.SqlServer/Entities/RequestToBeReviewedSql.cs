// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

public record RequestToBeReviewedSql /*: IIdentifierMongoEntity, ITimestampedMongoEntity*/
{

  // TODO - Use records instead of class
  // TODO - Don't use Interface for persistance entity for the moment
  // TODO - Use disciminator for inheritance or versioning
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists

  //[Key]
  //[Column("_id")]
  //public ObjectId ObjectId { get; set; }

  [Column("uuid")]
  public Guid Uuid { get; set; }

  [NotMapped]
  public required Guid Id { get => Uuid; set { Uuid = value; } }

  [Column("createdAt")]
  public DateTimeOffset CreatedAt { get; set; }

  [Column("updatedAt")]
  public DateTimeOffset UpdatedAt { get; set; }

  // TODO - EntityProperties - Fields to complete

  /// <summary>
  /// A shared identifier common to all service requests that were authorized more or less simultaneously by a single author, 
  /// representing the composite or group identifier.
  /// </summary>
  /// <see cref="https://hl7.org/fhir/servicerequest-definitions.html#ServiceRequest.requisition"/>"/>
  [Column("requisition")]
  public string? Requisition { get; set; }

  [Column("requesteds")]
  public List<ServiceRequestSql> Requesteds { get; set; } = [];
}

// This commented part could be used to have benefits of mongo entity typing
// Example of inherited class
//[BsonIgnoreExtraElements]
//[BsonDiscriminator("requestToBeReviewedInherited", Required = true)]
//public record RequestToBeReviewedInheritedMongo : RequestToBeReviewedMongoBase
//{
//}