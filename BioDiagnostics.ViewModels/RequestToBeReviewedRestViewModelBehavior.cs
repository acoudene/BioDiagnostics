// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Core.ViewModels;
using BioDiagnostics.ViewModels.BffProxying;
using BioDiagnostics.ViewObjects;

namespace BioDiagnostics.ViewModels;

public class RequestToBeReviewedRestViewModelBehavior : RestViewModelBehavior<RequestToBeReviewedVo, IRequestToBeReviewedRestBffClient>
{
  public RequestToBeReviewedRestViewModelBehavior(IRequestToBeReviewedRestBffClient client) : base(client)
  {
  }
}
