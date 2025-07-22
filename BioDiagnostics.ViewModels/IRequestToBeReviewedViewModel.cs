// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Core.ViewModels;
using BioDiagnostics.ViewObjects;

namespace BioDiagnostics.ViewModels;

/// <summary>
/// Dedicated interface to manage ViewModel for a dedicated entity
/// </summary>
public interface IRequestToBeReviewedViewModel : IViewModel<RequestToBeReviewedVo>
{
  IEnumerable<RequestToBeReviewedVo> Items { get; set; }
  HashSet<RequestToBeReviewedVo> SelectedItems { get; set; }
  RequestToBeReviewedVo? SelectedItem { get; set; }
}