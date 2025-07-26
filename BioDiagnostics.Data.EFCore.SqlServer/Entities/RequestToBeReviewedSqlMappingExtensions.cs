// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.Data.EFCore.MongoDb.Entities;
using BioDiagnostics.Data.Entities;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

public static class RequestToBeReviewedSqlMappingExtensions
{
  // This commented part could be used to have benefits of mongo entity typing
  //public static RequestToBeReviewedMongoBase ToInheritedMongo(this RequestToBeReviewedBase entity)
  //{
  //  switch (entity)
  //  {
  //    case RequestToBeReviewedInherited inheritedEntity: return inheritedEntity.ToMongo();
  //    default:
  //      throw new NotImplementedException();
  //  }
  //}

  // This commented part could be used to have benefits of mongo entity typing
  //public static RequestToBeReviewedBase ToInheritedEntity(this RequestToBeReviewedMongoBase mongoEntity)
  //{
  //  switch (mongoEntity)
  //  {
  //    case RequestToBeReviewedInheritedMongo inheritedMongo: return inheritedMongo.ToEntity();
  //    default:
  //      throw new NotImplementedException();
  //  }
  //}

  public static RequestToBeReviewedSql ToMongo(this RequestToBeReviewed entity)
  {
    return new RequestToBeReviewedSql()
    {
      Id = entity.Id,
      CreatedAt = entity.CreatedAt.UtcDateTime,
      UpdatedAt = entity.UpdatedAt.UtcDateTime,

      // TODO - EntityMapping - Business Entity to Mongo Entity to complete



    };
  }

  public static RequestToBeReviewedMongoV02 ToMongoV02(this RequestToBeReviewed entity)
  {
    return new RequestToBeReviewedMongoV02()
    {
      Id = entity.Id,
      CreatedAt = entity.CreatedAt.UtcDateTime,
      UpdatedAt = entity.UpdatedAt.UtcDateTime,

      // TODO - EntityMapping - Business Entity to Mongo Entity to complete



    };
  }

  public static RequestToBeReviewed ToEntity(this RequestToBeReviewedSql mongoEntity)
  {
    return new RequestToBeReviewed()
    {
      Id = mongoEntity.Id,
      CreatedAt = mongoEntity.CreatedAt,
      UpdatedAt = mongoEntity.UpdatedAt,

      // TODO - EntityMapping - Mongo Entity to Business Entity to complete
      Requesteds = mongoEntity.Requesteds.Select(r => r.ToEntity()).ToList(),
      Requisition = mongoEntity.Requisition
    };
  }
  public static ServiceRequest ToEntity(this ServiceRequestSql mongoEntity)
  {
    return new ServiceRequest()
    {
      Codes = mongoEntity.Codes.Select(c => c.ToEntity()).ToList(),
      Identifiers = mongoEntity.Identifiers.Select(i => i.ToEntity()).ToList(),
      Patient = mongoEntity.Patient?.ToEntity(),
      Observations = mongoEntity.Observations.Select(o => o.ToEntity()).ToList()
    };
  }

  public static CodeableConcept ToEntity(this CodeableConceptSql mongoEntity)
  {
    return new CodeableConcept()
    {
      Codings = mongoEntity.Codings.Select(c => c.ToEntity()).ToList(),
      Text = mongoEntity.Text
    };
  }

  public static Coding ToEntity(this CodingSql mongoEntity)
  {
    return new Coding()
    {
      Code = mongoEntity.Code,
      Display = mongoEntity.Display,
      System = mongoEntity.System
    };
  }

  public static Identifier ToEntity(this IdentifierSql mongoEntity)
  {
    return new Identifier()
    {
      Value = mongoEntity.Value,
      System = mongoEntity.System,
      Type = mongoEntity.Type
    };
  }

  public static Patient ToEntity(this PatientSql mongoEntity)
  {
    return new Patient()
    {
      Names = mongoEntity.Names.Select(n => n.ToEntity()).ToList(),
      Identifiers = mongoEntity.Identifiers.Select(i => i.ToEntity()).ToList()
    };
  }

  public static HumanName ToEntity(this HumanNameSql mongoEntity)
  {
    return new HumanName()
    {
      Family = mongoEntity.Family,
      Givens = mongoEntity.Givens.ToList(),
      Use = mongoEntity.Use?.ToEntity()
    };
  }

  public static NameUse ToEntity(this NameUseSql mongoEntity)
  {
    return mongoEntity switch
    {
      NameUseSql.Usual => NameUse.Usual,
      NameUseSql.Official => NameUse.Official,
      NameUseSql.Temp => NameUse.Temp,
      NameUseSql.Nickname => NameUse.Nickname,
      NameUseSql.Anonymous => NameUse.Anonymous,
      NameUseSql.Old => NameUse.Old,
      NameUseSql.Maiden => NameUse.Maiden,
      _ => throw new NotImplementedException($"Unknown NameUse: {mongoEntity}")
    };
  }

  public static Observation ToEntity(this ObservationSql mongoEntity)
  {
    return new Observation()
    {
      Codes = mongoEntity.Codes.Select(c => c.ToEntity()).ToList(),
      Specimen = mongoEntity.Specimen?.ToEntity(),
      Status = mongoEntity.Status?.ToEntity(),
    };
  }

  public static Specimen ToEntity(this SpecimenSql mongoEntity)
  {
    return new Specimen()
    {
      Identifiers = mongoEntity.Identifiers.Select(i => i.ToEntity()).ToList(),
      Notes = mongoEntity.Notes
    };
  }

  public static ObservationStatus ToEntity(this ObservationStatusSql mongoEntity)
  {
    return mongoEntity switch
    {
      ObservationStatusSql.Amended => ObservationStatus.Amended,
      ObservationStatusSql.Cancelled => ObservationStatus.Cancelled,
      ObservationStatusSql.Corrected => ObservationStatus.Corrected,
      ObservationStatusSql.EnteredInError => ObservationStatus.EnteredInError,
      ObservationStatusSql.Final => ObservationStatus.Final,
      ObservationStatusSql.Preliminary => ObservationStatus.Preliminary,
      ObservationStatusSql.Registered => ObservationStatus.Registered,
      ObservationStatusSql.Unknown => ObservationStatus.Unknown,
      _ => throw new NotImplementedException($"Unknown ObservationStatus: {mongoEntity}")
    };
  }

  public static RequestToBeReviewed ToEntity(this RequestToBeReviewedMongoV02 mongoEntity)
  {
    return (mongoEntity as RequestToBeReviewedSql).ToEntity();
  }
}
