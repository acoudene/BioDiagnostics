namespace BioDiagnostics.Data.Entities;

/// <summary>
/// A concept that may be defined by a formal reference to a terminology or ontology or may be provided by text.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.CodeableConcept"/>
public record CodeableConcept
{
  // TODO - Use records instead of class
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions
  // TODO - Use plural for collection properties (e.g., Codings instead of Coding)

  /// <summary>
  /// Coded representations of the concept (e.g., LOINC, SNOMED).
  /// </summary>
  public List<Coding> Codings { get; set; } = [];

  /// <summary>
  /// Plain text representation of the concept.
  /// </summary>
  public string? Text { get; set; }
}