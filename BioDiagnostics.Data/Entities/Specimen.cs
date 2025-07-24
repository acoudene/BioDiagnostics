namespace BioDiagnostics.Data.Entities;

/// <summary>
/// A sample to be used for analysis.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.Specimen"/>
public record Specimen /*: ISpecimen*/
{
  // TODO - Use records instead of class
  // TODO - Don't use Interface for persistance entity for the moment
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions

  /// <summary>
  /// A list of identifiers for the specimen
  /// </summary>
  public List<Identifier> Identifiers { get; set; } = [];

  /// <summary>
  /// Comments or notes about the specimen.
  /// </summary>
  /// <see cref="https://hl7.org/fhir/specimen-definitions.html#Specimen.note"/>
  public List<string> Notes { get; set; } = [];
}
