// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

namespace BioDiagnostics.Data.Entities;

// This commented part could be used to have benefits of entity typing
//public abstract record RequestToBeReviewedBase : IIdentifierEntity

public record RequestToBeReviewed : IIdentifierEntity, ITimestampedEntity
{
  public required Guid Id { get; init; }

  public DateTimeOffset CreatedAt { get; init; }

  public DateTimeOffset UpdatedAt { get; init; }

  // TODO - EntityProperties - Fields to complete
}

// This commented part could be used to have benefits of entity typing
// Example of inherited class
//public record RequestToBeReviewedInherited : RequestToBeReviewedBase
//{
//}