// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.Localization;
using BioDiagnostics.ViewObjects;
using BioDiagnostics.ViewObjects.Validation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BioDiagnostics.RazorComponents;

public partial class RequestToBeReviewedForm
{
  [Inject]
  public required IStringLocalizer<BioDiagnosticsResource> Localizer { private get; init; }

  [Parameter, EditorRequired]
  public required RequestToBeReviewedVo ViewObject { get; set; }

  private RequestToBeReviewedVoFluentValidator _requestToBeReviewedValidator = new RequestToBeReviewedVoFluentValidator();

  protected override void OnInitialized()
  {
    if (Localizer is null)
      throw new InvalidOperationException($"Misssing {nameof(Localizer)}");

    if (ViewObject is null)
      throw new InvalidOperationException($"Missing {nameof(ViewObject)}");
  }
}
