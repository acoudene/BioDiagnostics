namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

/// <summary>
/// Codes providing the status of an observation.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.ObservationStatus"/>
public enum ObservationStatusSql
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