// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Core.Host.Testing;
using Microsoft.AspNetCore.JsonPatch;

namespace BioDiagnostics.Host.Tests;

/// WARNING - for the moment, I don't have found a solution to reset settings like connexion string on a static test server
/// So be careful when changing settings, the same first settings will remain for server for all tests in this class even if this container is reset.
/// For example, don't change default port to reuse the same.
public class GivenRequestToBeReviewedApi : HostApiMongoTestBase<Program>
{
  public GivenRequestToBeReviewedApi(
    WebApplicationFactory<Program> webApplicationFactory,
    ITestOutputHelper output)
    : base("requestToBeReviewed", webApplicationFactory, output)
  {
  }

  private const string ApiPath = "/api";
  private const string ApiRelativePath = $"{ApiPath}/RequestToBeReviewed/"; // Warning, this ending slash is important in HttpClientFactory... :(

  [Theory]
  [ClassData(typeof(RequestToBeReviewedData))]
  public async Task WhenCreatingItem_ThenSingleItemIsCreated_Async(RequestToBeReviewedDto item)
  {
    // Arrange
    var logger = CreateLogger<HttpRequestToBeReviewedClient>();
    var httpClientFactory = CreateHttpClientFactory(ApiRelativePath);
    var client = new HttpRequestToBeReviewedClient(logger, httpClientFactory);

    // Act
    await client.CreateAsync(item);

    // Assert      
    var foundItem = await client.GetByIdAsync(item.Id);
    Assert.NotNull(foundItem);
  }

  [Theory]
  [ClassData(typeof(RequestToBeReviewedData))]
  public async Task WhenCreatingOrUpdatingItem_ThenSingleItemIsCreatedOrUpdated_Async(RequestToBeReviewedDto item)
  {
    // Arrange
    var logger = CreateLogger<HttpRequestToBeReviewedClient>();
    var httpClientFactory = CreateHttpClientFactory(ApiRelativePath);
    var client = new HttpRequestToBeReviewedClient(logger, httpClientFactory);

    // Act
    await client.CreateOrUpdateAsync(item);

    // Assert      
    var foundItem = await client.GetByIdAsync(item.Id);
    Assert.NotNull(foundItem);
  }

  [Theory]
  [ClassData(typeof(RequestToBeReviewedsData))]
  public async Task WhenCreatingItems_ThenAllItemsAreGot_Async(List<RequestToBeReviewedDto> items)
  {
    // Arrange
    var logger = CreateLogger<HttpRequestToBeReviewedClient>();
    var httpClientFactory = CreateHttpClientFactory(ApiRelativePath);
    var client = new HttpRequestToBeReviewedClient(logger, httpClientFactory);
    foreach (var item in items)
      await WhenCreatingItem_ThenSingleItemIsCreated_Async(item);
    var ids = items.Select(item => item.Id).ToList();
    int expectedCount = items.Count;

    // Act
    var gotItems = (await client.GetByIdsAsync(ids));

    // Assert
    Assert.True(expectedCount == gotItems.Count);
    Assert.Equivalent(items.Select(item => item.Id), gotItems.Select(item => item.Id));
  }

  [Theory]
  [ClassData(typeof(RequestToBeReviewedsData))]
  public async Task WhenCreatingOrUpdatingItems_ThenAllItemsAreGot_Async(List<RequestToBeReviewedDto> items)
  {
    // Arrange
    var logger = CreateLogger<HttpRequestToBeReviewedClient>();
    var httpClientFactory = CreateHttpClientFactory(ApiRelativePath);
    var client = new HttpRequestToBeReviewedClient(logger, httpClientFactory);
    foreach (var item in items)
      await WhenCreatingOrUpdatingItem_ThenSingleItemIsCreatedOrUpdated_Async(item);
    var ids = items.Select(item => item.Id).ToList();
    int expectedCount = items.Count;

    // Act
    foreach (var item in items)
      await WhenCreatingOrUpdatingItem_ThenSingleItemIsCreatedOrUpdated_Async(item);
    var gotItems = (await client.GetByIdsAsync(ids));

    // Assert
    Assert.True(expectedCount == gotItems.Count);
    Assert.Equivalent(items.Select(item => item.Id), gotItems.Select(item => item.Id));
  }

  [Theory]
  [ClassData(typeof(RequestToBeReviewedsData))]
  public async Task WhenDeletingItems_ThenItemsAreDeleted_Async(List<RequestToBeReviewedDto> items)
  {
    // Arrange
    var logger = CreateLogger<HttpRequestToBeReviewedClient>();
    var httpClientFactory = CreateHttpClientFactory(ApiRelativePath);
    var client = new HttpRequestToBeReviewedClient(logger, httpClientFactory);
    foreach (var item in items)
      await WhenCreatingItem_ThenSingleItemIsCreated_Async(item);
    var ids = items.Select(item => item.Id).ToList();

    // Act
    foreach (Guid id in ids)
      await client.DeleteAsync(id);

    var gotItems = (await client.GetByIdsAsync(ids));

    // Assert    
    Assert.Empty(gotItems);
  }

  //[Theory]
  //[ClassData(typeof(RequestToBeReviewedData))]
  //public async Task WhenPatchingItem_ThenItemIsPatched_Async(RequestToBeReviewedDto item)
  //{
  //  // Arrange
  //  var logger = CreateLogger<HttpRequestToBeReviewedClient>();
  //  var httpClientFactory = CreateHttpClientFactory(ApiRelativePath, new TestWebApplicationFactoryClientOptions(logger));
  //  var client = new HttpRequestToBeReviewedClient(logger, httpClientFactory);
  //  await client.CreateOrUpdateAsync(item); // Just to setup with an existing item
  //  Guid id = item.Id;
  //  string metadata = Guid.NewGuid().ToString();

  //  // Act
  //  var patch = new JsonPatchDocument<RequestToBeReviewedDto>();
  //  patch.Replace(dto => dto.Metadata, metadata);
  //  await client.PatchAsync(id, patch);

  //  // Assert
  //  var foundItem = await client.GetByIdAsync(id);
  //  Assert.NotNull(foundItem);
  //  Assert.Equal(metadata, foundItem.Metadata);
  //}
}
