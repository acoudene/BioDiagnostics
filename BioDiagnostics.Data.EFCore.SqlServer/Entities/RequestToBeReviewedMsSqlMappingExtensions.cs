// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.Data.Entities;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

public static class RequestToBeReviewedMsSqlMappingExtensions
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

  public static RequestToBeReviewedMsSql ToDbEntity(this RequestToBeReviewed entity)
  {
    return new RequestToBeReviewedMsSql()
    {
      Id = entity.Id,
      CreatedAt = entity.CreatedAt.UtcDateTime,
      UpdatedAt = entity.UpdatedAt.UtcDateTime,

      // TODO - EntityMapping - Business Entity to Mongo Entity to complete



    };
  }

  public static RequestToBeReviewed ToEntity(this RequestToBeReviewedMsSql mongoEntity)
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
  public static ServiceRequest ToEntity(this ServiceRequestMsSql mongoEntity)
  {
    return new ServiceRequest()
    {
      Codes = mongoEntity.Codes.Select(c => c.ToEntity()).ToList(),
      Identifiers = mongoEntity.Identifiers.Select(i => i.ToEntity()).ToList(),
      Patient = mongoEntity.Patient?.ToEntity(),
      Observations = mongoEntity.Observations.Select(o => o.ToEntity()).ToList()
    };
  }

  public static CodeableConcept ToEntity(this CodeableConceptMsSql mongoEntity)
  {
    return new CodeableConcept()
    {
      Codings = mongoEntity.Codings.Select(c => c.ToEntity()).ToList(),
      Text = mongoEntity.Text
    };
  }

  public static Coding ToEntity(this CodingMsSql mongoEntity)
  {
    return new Coding()
    {
      Code = mongoEntity.Code,
      Display = mongoEntity.Display,
      System = mongoEntity.System
    };
  }

  public static Identifier ToEntity(this IdentifierMsSql mongoEntity)
  {
    return new Identifier()
    {
      Value = mongoEntity.Value,
      System = mongoEntity.System,
      Type = mongoEntity.Type
    };
  }

  public static Patient ToEntity(this PatientMsSql mongoEntity)
  {
    return new Patient()
    {
      Names = mongoEntity.Names.Select(n => n.ToEntity()).ToList(),
      Identifiers = mongoEntity.Identifiers.Select(i => i.ToEntity()).ToList()
    };
  }

  public static HumanName ToEntity(this HumanNameMsSql mongoEntity)
  {
    return new HumanName()
    {
      Family = mongoEntity.Family,
      Givens = mongoEntity.Givens.ToList(),
      Use = mongoEntity.Use?.ToEntity()
    };
  }

  public static NameUse ToEntity(this NameUseMsSql mongoEntity)
  {
    return mongoEntity switch
    {
      NameUseMsSql.Usual => NameUse.Usual,
      NameUseMsSql.Official => NameUse.Official,
      NameUseMsSql.Temp => NameUse.Temp,
      NameUseMsSql.Nickname => NameUse.Nickname,
      NameUseMsSql.Anonymous => NameUse.Anonymous,
      NameUseMsSql.Old => NameUse.Old,
      NameUseMsSql.Maiden => NameUse.Maiden,
      _ => throw new NotImplementedException($"Unknown NameUse: {mongoEntity}")
    };
  }

  public static Observation ToEntity(this ObservationMsSql mongoEntity)
  {
    return new Observation()
    {
      Codes = mongoEntity.Codes.Select(c => c.ToEntity()).ToList(),
      Specimen = mongoEntity.Specimen?.ToEntity(),
      Status = mongoEntity.Status?.ToEntity(),
    };
  }

  public static Specimen ToEntity(this SpecimenMsSql mongoEntity)
  {
    return new Specimen()
    {
      Identifiers = mongoEntity.Identifiers.Select(i => i.ToEntity()).ToList(),
      Notes = mongoEntity.Notes
    };
  }

  public static ObservationStatus ToEntity(this ObservationStatusMsSql mongoEntity)
  {
    return mongoEntity switch
    {
      ObservationStatusMsSql.Amended => ObservationStatus.Amended,
      ObservationStatusMsSql.Cancelled => ObservationStatus.Cancelled,
      ObservationStatusMsSql.Corrected => ObservationStatus.Corrected,
      ObservationStatusMsSql.EnteredInError => ObservationStatus.EnteredInError,
      ObservationStatusMsSql.Final => ObservationStatus.Final,
      ObservationStatusMsSql.Preliminary => ObservationStatus.Preliminary,
      ObservationStatusMsSql.Registered => ObservationStatus.Registered,
      ObservationStatusMsSql.Unknown => ObservationStatus.Unknown,
      _ => throw new NotImplementedException($"Unknown ObservationStatus: {mongoEntity}")
    };
  }
}
