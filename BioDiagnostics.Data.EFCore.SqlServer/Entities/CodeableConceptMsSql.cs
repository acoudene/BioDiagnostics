using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

/// <summary>
/// A concept that may be defined by a formal reference to a terminology or ontology or may be provided by text.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.CodeableConcept"/>
[Table("CodeableConcepts")]
public record CodeableConceptMsSql
{
  // TODO - Use records instead of class
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions
  // TODO - Use plural for collection properties (e.g., Codings instead of Coding)

  [Key]
  public required Guid Id { get; set; }

  /// <summary>
  /// Coded representations of the concept (e.g., LOINC, SNOMED).
  /// </summary>
  [Column("Codings")]
  public List<CodingMsSql> Codings { get; set; } = [];

  /// <summary>
  /// Plain text representation of the concept.
  /// </summary>
  [Column("Text")]
  public string? Text { get; set; }
}