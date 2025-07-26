using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

/// <summary>
/// A human's name with the ability to identify parts and usage.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.HumanName"/>
public record HumanNameSql
{
  // TODO - Use records instead of class
  // TODO - Use Enumerable<T>.Empty<T>() or [] instead of new List<T>() for empty lists
  // TODO - Always initialize lists to avoid null reference exceptions
  // TODO - Use plural for collection properties (e.g., Codings instead of Coding)

  /// <summary>
  /// The family name (last name).
  /// </summary>
  [Column("family")]
  public string? Family { get; set; }

  /// <summary>
  /// The given names (first name, middle name, etc.).
  /// </summary>
  [Column("givens")]
  public List<string> Givens { get; set; } = [];

  /// <summary>
  /// The use of the name (usual, official, temp, nickname, anonymous, old, maiden).
  /// </summary>
  [Column("use")]
  public NameUseSql? Use { get; set; }
}