// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.Proxies;
using BioDiagnostics.WebApp.Client.Extensions;

namespace BioDiagnostics.WebApp.Extensions;

public static class ServiceCollectionsExtensions
{
  public static void AddRequestToBeReviewedApiClient(this IServiceCollection serviceCollection, Uri apiUri)
    => serviceCollection
    .AddClientsWithUri<IRequestToBeReviewedClient, HttpRequestToBeReviewedClient>(
      HttpRequestToBeReviewedClient.ConfigurationName,
      apiUri);
}
