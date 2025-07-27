using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

/// <summary>
/// Measurements and simple assertions made about a patient, device or other subject.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.Observation"/>
[Table("Observation")]
public record ObservationMsSql /*: IFulfilledBy*/
{
  // TODO - Use records instead of class
  // TODO - Don't use Interface for persistance entity for the moment
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions

  [Key]
  public required Guid Id { get; set; }

  /// <summary>
  /// Type of observation (code / type)
  /// </summary>
  [Column("Codes")]
  public List<CodeableConceptMsSql> Codes { get; set; } = [];

  /// <summary>
  /// Specimen used for this observation.
  /// <para>Specimen used for this observation.</para>
  /// </summary>
  /// <see cref="https://www.hl7.org/fhir/observation-definitions.html#Observation.specimen"/>
  // FHIR Cardinality: 0..1  
  [Column("Specimen")]
  public SpecimenMsSql? Specimen { get; set; }
  //public ISpecimen? Specimen { get; set; }

  /// <summary>
  /// The status of the result value (e.g., registered, preliminary, final, amended).
  /// </summary>
  /// <see cref="https://hl7.org/fhir/observation-definitions.html#Observation.status"/>
  [Column("Status")]
  public ObservationStatusMsSql? Status { get; set; }
}
