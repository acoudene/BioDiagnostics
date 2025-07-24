// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

namespace BioDiagnostics.Data.Entities;

public record RequestToBeReviewed : IIdentifierEntity, ITimestampedEntity
{
  public required Guid Id { get; init; }

  public DateTimeOffset CreatedAt { get; init; }

  public DateTimeOffset UpdatedAt { get; init; }

  // TODO - EntityProperties - Fields to complete

  /// <summary>
  /// A shared identifier common to all service requests that were authorized more or less simultaneously by a single author, 
  /// representing the composite or group identifier.
  /// </summary>
  /// <see cref="https://hl7.org/fhir/servicerequest-definitions.html#ServiceRequest.requisition"/>"/>
  public string? Requisition { get; set; }

  public List<ServiceRequest> Requesteds { get; set; } = [];

}
