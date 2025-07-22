// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace BioDiagnostics.Proxies;

public class HttpRequestToBeReviewedClient : IRequestToBeReviewedClient
{
  private readonly ILogger<HttpRequestToBeReviewedClient> _logger;

  protected HttpRestClientBehavior<RequestToBeReviewedDto> HttpRestClientComponent { get => _httpRestClientComponent; }
  private readonly HttpRestClientBehavior<RequestToBeReviewedDto> _httpRestClientComponent;

  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="logger"></param>
  /// <param name="httpClientFactory"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public HttpRequestToBeReviewedClient(ILogger<HttpRequestToBeReviewedClient> logger, IHttpClientFactory httpClientFactory)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    _httpRestClientComponent = new HttpRestClientBehavior<RequestToBeReviewedDto>(httpClientFactory);
  }

  public const string ConfigurationName = nameof(HttpRequestToBeReviewedClient);

  public virtual string GetConfigurationName() => ConfigurationName;

  public virtual async Task<List<RequestToBeReviewedDto>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing remote call to {Method}...", nameof(GetAllAsync));

    return await _httpRestClientComponent.GetAllAsync(GetConfigurationName(), cancellationToken);
  }

  public virtual async Task<RequestToBeReviewedDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing remote call to {Method}({Id})...", nameof(GetByIdAsync), id);

    return await _httpRestClientComponent.GetByIdAsync(id, GetConfigurationName(), cancellationToken);
  }

  public virtual async Task<List<RequestToBeReviewedDto>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing remote call to {Method}({Ids})...", nameof(GetByIdsAsync), string.Join(',', ids));

    return await _httpRestClientComponent.GetByIdsAsync(ids, GetConfigurationName(), cancellationToken);
  }

  public virtual async Task CreateAsync(
    RequestToBeReviewedDto dto,
    CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing remote call to {Method}({Dto})...", nameof(CreateAsync), dto);

        await _httpRestClientComponent.CreateAsync(dto, GetConfigurationName(), true, cancellationToken);
  }

  public virtual async Task CreateOrUpdateAsync(
      RequestToBeReviewedDto dto,
      CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing call to {Method}({dto})...", nameof(CreateOrUpdateAsync), dto);

        await _httpRestClientComponent.CreateOrUpdateAsync(dto, GetConfigurationName(), true, cancellationToken);
  }

  public virtual async Task UpdateAsync(
    Guid id,
    RequestToBeReviewedDto dto,
    CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing remote call to {Method}({Id},{Dto})...", nameof(UpdateAsync), id, dto);

        await _httpRestClientComponent.UpdateAsync(id, dto, GetConfigurationName(), true, cancellationToken);
  }

  public virtual async Task<RequestToBeReviewedDto?> DeleteAsync(
    Guid id,
    CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing remote call to {Method}({Id})...", nameof(DeleteAsync), id);

    return await _httpRestClientComponent.DeleteAsync(id, GetConfigurationName(), cancellationToken);
  }

  public virtual async Task PatchAsync(
    Guid id,
    JsonPatchDocument<RequestToBeReviewedDto> patch,
    CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing remote call to {Method}({Id},{Patch})...", nameof(PatchAsync), id, patch);

        await _httpRestClientComponent.PatchAsync(id, patch, GetConfigurationName(), true, cancellationToken);
  }
}
