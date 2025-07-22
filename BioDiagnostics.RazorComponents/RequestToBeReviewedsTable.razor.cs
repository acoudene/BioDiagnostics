// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.Localization;
using BioDiagnostics.ViewObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BioDiagnostics.RazorComponents;

public partial class RequestToBeReviewedsTable
{
  private string _searchString = string.Empty;

  /// Use it if needed for row edition template: private RequestToBeReviewedVoFluentValidator _requestToBeReviewedValidator = new RequestToBeReviewedVoFluentValidator();

  [Inject]
  public required IStringLocalizer<BioDiagnosticsResource> Localizer { private get; init; }

  [Parameter, EditorRequired]
  public required IEnumerable<RequestToBeReviewedVo> ViewObjects { get; set; }

  [Parameter]
  public EventCallback<IEnumerable<RequestToBeReviewedVo>?> ViewObjectsChanged { get; set; }

  [Parameter]
  public HashSet<RequestToBeReviewedVo>? SelectedViewObjects { get; set; }

  [Parameter]
  public RequestToBeReviewedVo? SelectedViewObject { get; set; }

  [Parameter]
  public EventCallback<HashSet<RequestToBeReviewedVo>> OnSelectedItemsChanged { get; set; }

  protected override void OnInitialized()
  {
    if (Localizer is null)
      throw new InvalidOperationException($"Misssing {nameof(Localizer)}");

    if (ViewObjects is null)
      throw new InvalidOperationException($"Missing {nameof(ViewObjects)}");
  }

  private bool FilterFunc(RequestToBeReviewedVo vo)
  {
    return vo switch
    {
      RequestToBeReviewedVo x when x.Id.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) => true,
      RequestToBeReviewedVo x when x.CreatedAt.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) => true,
      RequestToBeReviewedVo x when x.UpdatedAt.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) => true,

      // TODO - Complete with search filter in grid

      RequestToBeReviewedVo x when x.Metadata is not null && x.Metadata.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) => true,

      null => false,
      _ => false
    };
  }

  private ElementComparer RequestToBeReviewedVoComparer = new();

  class ElementComparer : IEqualityComparer<RequestToBeReviewedVo>
  {
    public bool Equals(RequestToBeReviewedVo? a, RequestToBeReviewedVo? b) => a?.Id == b?.Id;
    public int GetHashCode(RequestToBeReviewedVo x) => HashCode.Combine(x?.Id);
  }
}
