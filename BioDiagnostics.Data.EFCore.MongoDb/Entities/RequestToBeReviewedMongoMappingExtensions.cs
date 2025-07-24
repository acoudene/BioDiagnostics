// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.Data.Entities;

namespace BioDiagnostics.Data.EFCore.MongoDb.Entities;

public static class RequestToBeReviewedMongoMappingExtensions
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

  public static RequestToBeReviewedMongo ToMongo(this RequestToBeReviewed entity)
  {
    return new RequestToBeReviewedMongo()
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

  public static RequestToBeReviewed ToEntity(this RequestToBeReviewedMongo mongoEntity)
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
  public static ServiceRequest ToEntity(this ServiceRequestMongo mongoEntity)
  {
    return new ServiceRequest()
    {
      Codes = mongoEntity.Codes.Select(c => c.ToEntity()).ToList(),
      Identifiers = mongoEntity.Identifiers.Select(i => i.ToEntity()).ToList(),
      Patient = mongoEntity.Patient?.ToEntity(),
      Observations = mongoEntity.Observations.Select(o => o.ToEntity()).ToList()
    };
  }

  public static CodeableConcept ToEntity(this CodeableConceptMongo mongoEntity)
  {
    return new CodeableConcept()
    {
      Codings = mongoEntity.Codings.Select(c => c.ToEntity()).ToList(),
      Text = mongoEntity.Text
    };
  }

  public static Coding ToEntity(this CodingMongo mongoEntity)
  {
    return new Coding()
    {
      Code = mongoEntity.Code,
      Display = mongoEntity.Display,
      System = mongoEntity.System
    };
  }

  public static Identifier ToEntity(this IdentifierMongo mongoEntity)
  {
    return new Identifier()
    {
      Value = mongoEntity.Value,
      System = mongoEntity.System,
      Type = mongoEntity.Type
    };
  }

  public static Patient ToEntity(this PatientMongo mongoEntity)
  {
    return new Patient()
    {
      Names = mongoEntity.Names.Select(n => n.ToEntity()).ToList(),
      Identifiers = mongoEntity.Identifiers.Select(i => i.ToEntity()).ToList()
    };
  }

  public static HumanName ToEntity(this HumanNameMongo mongoEntity)
  {
    return new HumanName()
    {
      Family = mongoEntity.Family,
      Givens = mongoEntity.Givens.ToList(),
      Use = mongoEntity.Use?.ToEntity()
    };
  }

  public static NameUse ToEntity(this NameUseMongo mongoEntity)
  {
    return mongoEntity switch
    {
      NameUseMongo.Usual => NameUse.Usual,
      NameUseMongo.Official => NameUse.Official,
      NameUseMongo.Temp => NameUse.Temp,
      NameUseMongo.Nickname => NameUse.Nickname,
      NameUseMongo.Anonymous => NameUse.Anonymous,
      NameUseMongo.Old => NameUse.Old,
      NameUseMongo.Maiden => NameUse.Maiden,
      _ => throw new NotImplementedException($"Unknown NameUse: {mongoEntity}")
    };
  }

  public static Observation ToEntity(this ObservationMongo mongoEntity)
  {
    return new Observation()
    {
      Codes = mongoEntity.Codes.Select(c => c.ToEntity()).ToList(),
      Specimen = mongoEntity.Specimen?.ToEntity(),
      Status = mongoEntity.Status?.ToEntity(),
    };
  }

  public static Specimen ToEntity(this SpecimenMongo mongoEntity)
  {
    return new Specimen()
    {
      Identifiers = mongoEntity.Identifiers.Select(i => i.ToEntity()).ToList(),
      Notes = mongoEntity.Notes
    };
  }

  public static ObservationStatus ToEntity(this ObservationStatusMongo mongoEntity)
  {
    return mongoEntity switch
    {
      ObservationStatusMongo.Amended => ObservationStatus.Amended,
      ObservationStatusMongo.Cancelled => ObservationStatus.Cancelled,
      ObservationStatusMongo.Corrected => ObservationStatus.Corrected,
      ObservationStatusMongo.EnteredInError => ObservationStatus.EnteredInError,
      ObservationStatusMongo.Final => ObservationStatus.Final,
      ObservationStatusMongo.Preliminary => ObservationStatus.Preliminary,
      ObservationStatusMongo.Registered => ObservationStatus.Registered,
      ObservationStatusMongo.Unknown => ObservationStatus.Unknown,
      _ => throw new NotImplementedException($"Unknown ObservationStatus: {mongoEntity}")
    };
  }

  public static RequestToBeReviewed ToEntity(this RequestToBeReviewedMongoV02 mongoEntity)
  {
    return (mongoEntity as RequestToBeReviewedMongo).ToEntity();
  }
}
