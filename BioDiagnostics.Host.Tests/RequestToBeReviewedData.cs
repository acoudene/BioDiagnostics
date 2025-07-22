// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

namespace BioDiagnostics.Host.Tests;

internal class RequestToBeReviewedData : TheoryData<RequestToBeReviewedDto>
{
  public RequestToBeReviewedData()
  {
    Add(new RequestToBeReviewedDto()
    {
      Id = Guid.NewGuid()
      // TODO - EntityProperties - Fields to complete
    });

    Add(new RequestToBeReviewedDto()
    {
      Id = Guid.NewGuid()
      // TODO - EntityProperties - Fields to complete
    });

    Add(new RequestToBeReviewedDto()
    {
      Id = Guid.NewGuid()
      // TODO - EntityProperties - Fields to complete
    });
  }
}

