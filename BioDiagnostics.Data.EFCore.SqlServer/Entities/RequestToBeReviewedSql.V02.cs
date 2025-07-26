using BioDiagnostics.Data.EFCore.SqlServer.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.MongoDb.Entities;

public record RequestToBeReviewedMongoV02 : RequestToBeReviewedSql
{
  [Column("version")]
  public string Version { get; } = "V02";
}