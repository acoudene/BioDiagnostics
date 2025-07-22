// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.Dtos;
using BioDiagnostics.ViewObjects;

namespace BioDiagnostics.Api.BackendForFrontend;

public static class RequestToBeReviewedVoExtensions
{
  public static RequestToBeReviewedVo ToViewObject(this RequestToBeReviewedDto dto)
  {
    if (dto is null)
      return null!;

    switch (dto)
    {
      case RequestToBeReviewedDto:
        {
          return new RequestToBeReviewedVo()
          {
            Id = dto.Id,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,

            // TODO - EntityMapping - Dto to ViewObject to complete

            Metadata = dto.Metadata,
          };
        }

      default:
        throw new NotImplementedException();
    }
  }

  public static RequestToBeReviewedDto ToDto(this RequestToBeReviewedVo viewObject)
  {
    if (viewObject is null)
      return null!;

    switch (viewObject)
    {
      case RequestToBeReviewedVo:
        {
          return new RequestToBeReviewedDto()
          {
            Id = viewObject.Id,
            CreatedAt = viewObject.CreatedAt,
            UpdatedAt = viewObject.UpdatedAt,

            // TODO - EntityMapping - ViewObject to Dto to complete

            Metadata = viewObject.Metadata,
          };
        }

      default:
        throw new NotImplementedException();
    }
  }
}
