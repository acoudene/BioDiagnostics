namespace BioDiagnostics.Data.MongoDb.Entities;

/// <summary>
/// Codes providing the status of an observation.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.ObservationStatus"/>
public enum ObservationStatusMongo
{
  Registered,
  Preliminary,
  Final,
  Amended,
  Corrected,
  Cancelled,
  EnteredInError,
  Unknown
}