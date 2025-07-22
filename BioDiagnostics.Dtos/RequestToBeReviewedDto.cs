// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

namespace BioDiagnostics.Dtos;

// This commented part could be used to have benefits of json entity typing
//[JsonPolymorphic]
//[JsonDerivedType(typeof(RequestToBeReviewedInheritedDto), RequestToBeReviewedInheritedDto.TypeId)]
public record RequestToBeReviewedDto : IIdentifierDto, ITimestampedDto
{
  public required Guid Id { get; set; }

  public DateTimeOffset CreatedAt { get; init; }

  public DateTimeOffset UpdatedAt { get; init; }

  // TODO - EntityProperties - Fields to complete

  public string? Metadata { get; set; } // Example, to remove if needed
}

// This commented part could be used to have benefits of json entity typing
// Example of inherited class
//[JsonDerivedType(typeof(RequestToBeReviewedInheritedDto), RequestToBeReviewedInheritedDto.TypeId)]
//public record RequestToBeReviewedInheritedDto : RequestToBeReviewedDtoBase
//{
//  public const string TypeId = "requestToBeReviewed.requestToBeReviewedInherited";
//}