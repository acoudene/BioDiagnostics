// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

namespace BioDiagnostics.Data.MongoDb.Tests;

public class SeedDataTheoryData : TheoryData<SeedDataBuilder>
{
  public SeedDataTheoryData()
  {
    Add(new SeedDataBuilder(databaseName: "biodiagnostics", collectionName: "requestToBeReviewed"));
  }
}
