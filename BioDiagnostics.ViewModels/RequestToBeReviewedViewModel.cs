// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.ViewModels.BffProxying;
using BioDiagnostics.ViewObjects;

namespace BioDiagnostics.ViewModels;

/// <summary>
/// ViewModel associated to a dedicated entity
/// </summary>
public class RequestToBeReviewedViewModel : IRequestToBeReviewedViewModel
{
  private readonly RequestToBeReviewedRestViewModelBehavior _behavior;
  public RequestToBeReviewedViewModel(IRequestToBeReviewedRestBffClient client)
  {
    ArgumentNullException.ThrowIfNull(client);

    _behavior = new RequestToBeReviewedRestViewModelBehavior(client);
    Items = Enumerable.Empty<RequestToBeReviewedVo>().ToList();
    SelectedItems = Enumerable.Empty<RequestToBeReviewedVo>().ToHashSet();
  }

  public IEnumerable<RequestToBeReviewedVo> Items { get; set; }
  public HashSet<RequestToBeReviewedVo> SelectedItems { get; set; }
  public RequestToBeReviewedVo? SelectedItem { get; set; }

  public virtual async Task CreateAsync(RequestToBeReviewedVo newItem, CancellationToken cancellationToken = default)
    => await _behavior.CreateAsync(newItem, cancellationToken);

  public virtual async Task CreateOrUpdateAsync(RequestToBeReviewedVo newOrToUpdateVo, CancellationToken cancellationToken = default)
    => await _behavior.CreateOrUpdateAsync(newOrToUpdateVo, cancellationToken);

  public virtual async Task<List<RequestToBeReviewedVo>> GetAllAsync(CancellationToken cancellationToken = default)
    => await _behavior.GetAllAsync(cancellationToken);

  public virtual async Task<RequestToBeReviewedVo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => await _behavior.GetByIdAsync(id, cancellationToken);

  public virtual async Task<List<RequestToBeReviewedVo>?> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    => await _behavior.GetByIdsAsync(ids, cancellationToken);

  public virtual async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    => await _behavior.RemoveAsync(id, cancellationToken);

  public virtual async Task UpdateAsync(Guid id, RequestToBeReviewedVo updatedItem, CancellationToken cancellationToken = default)
   => await _behavior.UpdateAsync(id, updatedItem, cancellationToken);
}
