using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

/// <summary>
/// Demographics and other administrative information about an individual or animal receiving care or other health-related services.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.Patient"/>
[Table("Patient")]
public record PatientMsSql /*: ISubject*/ 
{
  // TODO - Use records instead of class
  // TODO - Don't use Interface for persistance entity for the moment
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions

  [Key]
  public required Guid Id { get; set; }

  /// <summary>
  /// The patient's name(s).
  /// </summary>
  [Column("Names")]
  public List<HumanNameMsSql> Names { get; set; } = [];

  /// <summary>
  /// A list of identifiers for the patient (e.g., MRN, national ID).
  /// </summary>
  [Column("Identifiers")]
  public List<IdentifierMsSql> Identifiers { get; set; } = [];
}