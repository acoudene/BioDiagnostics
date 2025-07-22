namespace BioDiagnostics.Data.MongoDb.Entities;

/// <summary>
/// An identifier - identifies some entity uniquely and unambiguously.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.Identifier"/>
public record IdentifierMongo 
{
  // TODO - Use records instead of class

  /// <summary>
  /// The value that is unique.
  /// </summary>
  public string? Value { get; set; }

  /// <summary>
  /// The system that issues the identifier (e.g., hospital, government).
  /// </summary>
  public string? System { get; set; }

  /// <summary>
  /// The type of identifier (e.g., MRN, SSN).
  /// </summary>
  public string? Type { get; set; }
}