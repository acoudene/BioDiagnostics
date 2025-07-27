using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

/// <summary>
/// A reference to a code defined by a terminology system.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.Coding"/>
[Table("Coding")]
public record CodingMsSql
{
  // TODO - Use records instead of class

  [Key]
  public required Guid Id { get; set; }

  /// <summary>
  /// The identification of the code system that defines the meaning of the symbol.
  /// </summary>
  [Column("System")]
  public string? System { get; set; }

  /// <summary>
  /// The symbol in the code system.
  /// </summary>
  [Column("Code")]
  public string? Code { get; set; }

  /// <summary>
  /// A representation of the meaning of the code in the system, if available.
  /// </summary>
  [Column("Display")]
  public string? Display { get; set; }
}