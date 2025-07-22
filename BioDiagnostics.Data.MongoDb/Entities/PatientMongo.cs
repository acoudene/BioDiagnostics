namespace BioDiagnostics.Data.MongoDb.Entities;

/// <summary>
/// Demographics and other administrative information about an individual or animal receiving care or other health-related services.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.Patient"/>
public record PatientMongo /*: ISubject*/ 
{
  // TODO - Use records instead of class
  // TODO - Don't use Interface for persistance entity for the moment
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions

  /// <summary>
  /// The patient's name(s).
  /// </summary>
  public List<HumanNameMongo> Name { get; set; } = [];

  /// <summary>
  /// A list of identifiers for the patient (e.g., MRN, national ID).
  /// </summary>
  public List<IdentifierMongo> Identifier { get; set; } = [];
}