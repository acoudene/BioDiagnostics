using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

/// <summary>
/// An identifier - identifies some entity uniquely and unambiguously.
/// </summary>
/// <seealso cref="Hl7.Fhir.Model.Identifier"/>
[Table("Identifier")]
public record IdentifierMsSql 
{
  // TODO - Use records instead of class

  [Key]
  public required Guid Id { get; set; }

  /// <summary>
  /// The value that is unique.
  /// </summary>
  [Column("Value")]
  public string? Value { get; set; }

  /// <summary>
  /// The system that issues the identifier (e.g., hospital, government).
  /// </summary>
  [Column("System")]
  public string? System { get; set; }

  /// <summary>
  /// The type of identifier (e.g., MRN, SSN).
  /// </summary>
  [Column("Type")]
  public string? Type { get; set; }
}