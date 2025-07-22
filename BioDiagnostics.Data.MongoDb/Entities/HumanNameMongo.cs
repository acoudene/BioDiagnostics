namespace BioDiagnostics.Data.MongoDb.Entities;

/// <summary>
/// A human's name with the ability to identify parts and usage.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.HumanName"/>
public record HumanNameMongo
{
  // TODO - Use records instead of class
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions

  /// <summary>
  /// The family name (last name).
  /// </summary>
  public string? Family { get; set; }

  /// <summary>
  /// The given names (first name, middle name, etc.).
  /// </summary>
  public List<string> Given { get; set; } = [];

  /// <summary>
  /// The use of the name (usual, official, temp, nickname, anonymous, old, maiden).
  /// </summary>
  public NameUseMongo? Use { get; set; }
}