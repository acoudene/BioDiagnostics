namespace BioDiagnostics.Data.MongoDb.Entities;

/// <summary>
/// A record of a request for service such as diagnostic investigations, treatments, or operations to be performed.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.ServiceRequest"/>
public record ServiceRequestMongo /*: IFulfilledBy*/
{
  // TODO - Use records instead of class
  // TODO - Don't use Interface for persistance entity for the moment
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions

  /// <summary>
  /// A list of identifiers for the service request (e.g., order number, external ID).
  /// </summary>
  public List<IdentifierMongo> Identifier { get; set; } = [];

  /// <summary>
  /// Plans/proposals/orders that fulfill the request.
  /// </summary>
  //public List<IFulfilledBy>? FulfilledBy { get; set; }
  public List<ObservationMongo> Observations { get; set; } = [];

  /// <summary>
  /// Individual or Entity the service is ordered for.
  /// <para>On whom or what the service is to be performed. This is usually a human patient, 
  /// but can also be requested on animals, groups of humans or animals, devices such as dialysis machines, 
  /// or even locations (typically for environmental scans).</para>
  /// </summary>
  /// <see cref="https://hl7.org/fhir/servicerequest-definitions.html#ServiceRequest.subject"/>
  // FHIR Cardinality: 1..1
  //public ISubject? Subject { get; set; }
  public PatientMongo? Patient { get; set; }

  /// <summary>
  /// A code that identifies what is being requested (e.g., procedure, diagnostic test).
  /// </summary>
  /// <see cref="https://hl7.org/fhir/servicerequest-definitions.html#ServiceRequest.code"/>
  public List<CodeableConceptMongo> Codes { get; set; } = [];
}
