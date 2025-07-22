// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.ViewObjects;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace BioDiagnostics.ViewModels.BffProxying;

public class HttpRequestToBeReviewedRestBffClient : IRequestToBeReviewedRestBffClient
{
  private readonly ILogger<HttpRequestToBeReviewedRestBffClient> _logger;
  private readonly HttpRequestToBeReviewedRestBffClientBehavior _behavior;

  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="httpClientFactory"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public HttpRequestToBeReviewedRestBffClient(ILogger<HttpRequestToBeReviewedRestBffClient> logger, IHttpClientFactory httpClientFactory)
    : this(logger, new HttpRequestToBeReviewedRestBffClientBehavior(httpClientFactory))
  {
  }

  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="httpClientFactory"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public HttpRequestToBeReviewedRestBffClient(ILogger<HttpRequestToBeReviewedRestBffClient> logger, HttpRequestToBeReviewedRestBffClientBehavior behavior)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    _behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
  }

  public const string ConfigurationName = nameof(HttpRequestToBeReviewedRestBffClient);
  public virtual string GetConfigurationName() => ConfigurationName;

  public virtual async Task<List<RequestToBeReviewedVo>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing call to {Method}...", nameof(GetAllAsync));
    return await _behavior.GetAllAsync(GetConfigurationName(), cancellationToken);
  }

  public virtual async Task<RequestToBeReviewedVo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing call to {Method}({Id})...", nameof(GetByIdAsync), id);
    return await _behavior.GetByIdAsync(id, GetConfigurationName(), cancellationToken);
  }

  public virtual async Task<List<RequestToBeReviewedVo>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing call to {Method}({Ids})...", nameof(GetByIdsAsync), string.Join(',', ids));
    return await _behavior.GetByIdsAsync(ids, GetConfigurationName(), cancellationToken);
  }

  public virtual async Task CreateAsync(
      RequestToBeReviewedVo vo,
      CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing call to {Method}({Vo})...", nameof(CreateAsync), vo);

    await _behavior.CreateAsync(vo, GetConfigurationName(), true, cancellationToken);
  }

  public virtual async Task CreateOrUpdateAsync(
      RequestToBeReviewedVo vo,
      CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing call to {Method}({Vo})...", nameof(CreateOrUpdateAsync), vo);

    await _behavior.CreateOrUpdateAsync(vo, GetConfigurationName(), true, cancellationToken);
  }

  public virtual async Task UpdateAsync(
    Guid id,
    RequestToBeReviewedVo vo,
    CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing call to {Method}({Id},{Vo})...", nameof(UpdateAsync), id, vo);

    await _behavior.UpdateAsync(id, vo, GetConfigurationName(), true, cancellationToken);
  }

  public virtual async Task<RequestToBeReviewedVo?> DeleteAsync(
    Guid id,
    CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing call to {Method}({Id})...", nameof(DeleteAsync), id);
    return await _behavior.DeleteAsync(id, GetConfigurationName(), cancellationToken);
  }

  public virtual async Task PatchAsync(
    Guid id,
    JsonPatchDocument<RequestToBeReviewedVo> patch,
    CancellationToken cancellationToken = default)
  {
    _logger.LogDebug("Processing call to {Method}({Id},{Patch})...", nameof(PatchAsync), id, patch);

    await _behavior.PatchAsync(id, patch, GetConfigurationName(), true, cancellationToken);
  }
}


