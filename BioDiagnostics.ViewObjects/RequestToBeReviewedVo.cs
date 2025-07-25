﻿// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Core.ViewObjects;

namespace BioDiagnostics.ViewObjects;

// This commented part could be used to have benefits of json entity typing
//[JsonPolymorphic]
//[JsonDerivedType(typeof(EntityInheritedVo), EntityInheritedVo.TypeId)]
public record RequestToBeReviewedVo : IIdentifierViewObject, ITimestampedViewObject
{
  public required Guid Id { get; set; }
  public DisplayableDateTime CreatedAt { get; init; }
  public DisplayableDateTime UpdatedAt { get; init; }

  // TODO - EntityProperties - Fields to complete

}

// This commented part could be used to have benefits of json entity typing
// Example of inherited class
//[JsonDerivedType(typeof(EntityInheritedVo), EntityInheritedVo.TypeId)]
//public record EntityInheritedVo : EntityVoBase
//{
//  public const string TypeId = "article.articleInherited";
//}