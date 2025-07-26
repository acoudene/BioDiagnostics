using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

/// <summary>
/// A concept that may be defined by a formal reference to a terminology or ontology or may be provided by text.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.CodeableConcept"/>
public record CodeableConceptSql
{
  // TODO - Use records instead of class
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions
  // TODO - Use plural for collection properties (e.g., Codings instead of Coding)

  /// <summary>
  /// Coded representations of the concept (e.g., LOINC, SNOMED).
  /// </summary>
  [Column("codings")]
  public List<CodingSql> Codings { get; set; } = [];

  /// <summary>
  /// Plain text representation of the concept.
  /// </summary>
  [Column("text")]
  public string? Text { get; set; }
}