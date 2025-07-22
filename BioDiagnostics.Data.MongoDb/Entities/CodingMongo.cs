namespace BioDiagnostics.Data.MongoDb.Entities;

/// <summary>
/// A reference to a code defined by a terminology system.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.Coding"/>
public record CodingMongo
{
  // TODO - Use records instead of class

  /// <summary>
  /// The identification of the code system that defines the meaning of the symbol.
  /// </summary>
  [BsonElement("system")]
  public string? System { get; set; }

  /// <summary>
  /// The symbol in the code system.
  /// </summary>
  [BsonElement("code")]
  public string? Code { get; set; }

  /// <summary>
  /// A representation of the meaning of the code in the system, if available.
  /// </summary>
  [BsonElement("display")]
  public string? Display { get; set; }
}