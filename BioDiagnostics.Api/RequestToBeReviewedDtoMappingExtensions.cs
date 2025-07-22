// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

namespace BioDiagnostics.Api;

public static class RequestToBeReviewedDtoMappingExtensions
{
  // This commented part could be used to have benefits of json entity typing
  //public static RequestToBeReviewedDtoBase ToInheritedDto(this RequestToBeReviewedBase entity)
  //{
  //  switch (entity)
  //  {
  //    case RequestToBeReviewedInherited inheritedEntity: return inheritedEntity.ToDto();
  //    default:
  //      throw new NotImplementedException();
  //  }
  //}

  // This commented part could be used to have benefits of json entity typing
  //public static RequestToBeReviewedBase ToInheritedEntity(this RequestToBeReviewedDtoBase dto)
  //{
  //  switch (dto)
  //  {
  //    case RequestToBeReviewedInheritedDto inheritedDto: return inheritedDto.ToEntity();
  //    default:
  //      throw new NotImplementedException();
  //  }
  //}

  public static RequestToBeReviewedDto ToDto(this RequestToBeReviewed entity)
  {
    return new RequestToBeReviewedDto()
    {
      Id = entity.Id,
      CreatedAt = entity.CreatedAt,
      UpdatedAt = entity.UpdatedAt,

      // TODO - EntityMapping - Business Entity to Dto to complete

      Metadata = entity.Metadata,
    };
  }

  public static RequestToBeReviewed ToEntity(this RequestToBeReviewedDto dto)
  {
    return new RequestToBeReviewed()
    {
      Id = dto.Id,
      CreatedAt = dto.CreatedAt,
      UpdatedAt = dto.UpdatedAt,

      // TODO - EntityMapping - Dto to Business Entity to complete

      Metadata = dto.Metadata,
    };
  }
}
