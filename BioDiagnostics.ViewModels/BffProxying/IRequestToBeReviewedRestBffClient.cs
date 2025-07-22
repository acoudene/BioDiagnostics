// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Core.ViewModels.BffProxying;
using BioDiagnostics.ViewObjects;

namespace BioDiagnostics.ViewModels.BffProxying;

/// <summary>
/// Interface to manage client/server interaction as a REST proxy
/// </summary>
public interface IRequestToBeReviewedRestBffClient : IRestBffClient<RequestToBeReviewedVo>
{
}
