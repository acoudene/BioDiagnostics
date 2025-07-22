// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Core.ViewModels.BffProxying;
using BioDiagnostics.ViewObjects;

namespace BioDiagnostics.ViewModels.BffProxying;

public class HttpRequestToBeReviewedRestBffClientBehavior : HttpRestBffClientBehavior<RequestToBeReviewedVo>
{
  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="httpClientFactory"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public HttpRequestToBeReviewedRestBffClientBehavior(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
  {
  }
}


