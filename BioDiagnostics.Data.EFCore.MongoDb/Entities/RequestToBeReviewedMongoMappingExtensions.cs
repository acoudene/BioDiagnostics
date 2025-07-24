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

      
    };
  }

  public static RequestToBeReviewed ToEntity(this RequestToBeReviewedMongoV02 mongoEntity)
  {
    return new RequestToBeReviewed()
    {
      Id = mongoEntity.Id,
      CreatedAt = mongoEntity.CreatedAt,
      UpdatedAt = mongoEntity.UpdatedAt,

      // TODO - EntityMapping - Mongo Entity to Business Entity to complete


    };
  }
}
