using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

/// <summary>
/// Demographics and other administrative information about an individual or animal receiving care or other health-related services.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.Patient"/>
public record PatientSql /*: ISubject*/ 
{
  // TODO - Use records instead of class
  // TODO - Don't use Interface for persistance entity for the moment
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions

  /// <summary>
  /// The patient's name(s).
  /// </summary>
  [Column("names")]
  public List<HumanNameSql> Names { get; set; } = [];

  /// <summary>
  /// A list of identifiers for the patient (e.g., MRN, national ID).
  /// </summary>
  [Column("identifiers")]
  public List<IdentifierSql> Identifiers { get; set; } = [];
}