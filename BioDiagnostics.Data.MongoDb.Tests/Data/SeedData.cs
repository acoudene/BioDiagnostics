// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

namespace BioDiagnostics.Data.MongoDb.Tests;

public record SeedData(
  string ConnectionString, 
  string DatabaseName, 
  string CollectionName, 
  string FileName);
