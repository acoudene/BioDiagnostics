// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.Localization;
using BioDiagnostics.RazorComponents;
using BioDiagnostics.ViewModels;
using BioDiagnostics.ViewObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace BioDiagnostics.WebApp.Client.Pages;

public partial class RequestToBeReviewedPage : ComponentBase
{
  private RequestToBeReviewedForm? _form;

  [Inject]
  public required ISnackbar Snackbar { private get; init; }

  [Inject]
  public required ILogger<RequestToBeReviewedPage> Logger { private get; init; }

  [Inject]
  public required IRequestToBeReviewedViewModel ViewModel { private get; init; }

  [Inject]
  public required NavigationManager Navigation { private get; init; }

  [Inject]
  public required IStringLocalizer<BioDiagnosticsResource> Localizer { private get; init; }

  [Parameter]
  public string? Id { get; set; } = null;

  protected override void OnInitialized()
  {
    if (Localizer is null)
      throw new InvalidOperationException($"Missing {nameof(Localizer)}");

    if (ViewModel is null)
      throw new InvalidOperationException($"Missing {nameof(ViewModel)}");

  }

  protected async Task InitByIdAsync()
  {
    ViewModel.SelectedItem = new RequestToBeReviewedVo() { Id = Guid.NewGuid(), CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow };

    if (!string.IsNullOrWhiteSpace(Id) && Guid.TryParse(Id, out Guid guid))
    {
      ViewModel.SelectedItem = await ViewModel.GetByIdAsync(guid);
    }

    if (ViewModel.SelectedItem is null)
    {
      throw new InvalidOperationException($"Missing {nameof(ViewModel.SelectedItem)}");
    }
  }

  private async Task ValidateSubmitAsync()
  {
    if (_form is null)
      return;

    if (ViewModel.SelectedItem is null)
      return;

    await _form.Validate();

    if (!_form.IsValid)
      return;

    await ViewModel.CreateOrUpdateAsync(ViewModel.SelectedItem);
    Snackbar.Add(Localizer["Saved!"]);
    Navigation.NavigateTo("/requestToBeRevieweds");
  }
}
