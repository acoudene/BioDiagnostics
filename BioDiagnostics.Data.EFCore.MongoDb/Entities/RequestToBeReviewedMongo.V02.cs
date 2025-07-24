using System.ComponentModel.DataAnnotations.Schema;

namespace BioDiagnostics.Data.EFCore.MongoDb.Entities;

public record RequestToBeReviewedMongoV02 : RequestToBeReviewedMongo
{
  [Column("version")]
  public string Version { get; } = "V02";
}