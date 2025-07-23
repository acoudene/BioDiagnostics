// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using MongoDB.Driver;
using System.Linq.Expressions;

namespace BioDiagnostics.Data.MongoDb.Repositories;

public class RequestToBeReviewedRepositoryV02 : IRequestToBeReviewedRepository
{
  public const string CollectionName = "requestToBeReviewed";

internal protected TimeStampedMongoRepositoryBehavior<RequestToBeReviewed, RequestToBeReviewedMongoV02> Behavior { get => _behavior; }
private readonly TimeStampedMongoRepositoryBehavior<RequestToBeReviewed, RequestToBeReviewedMongoV02> _behavior;

public RequestToBeReviewedRepositoryV02(IMongoContext mongoContext)
{
  _behavior = new TimeStampedMongoRepositoryBehavior<RequestToBeReviewed, RequestToBeReviewedMongoV02>(mongoContext, CollectionName);
  _behavior.SetUniqueIndex(entity => entity.Id);
}

// This commented part could be used to have benefits of mongo entity typing
//protected virtual RequestToBeReviewedBase ToEntity(RequestToBeReviewedMongoBase mongoEntity)
//{
//  return mongoEntity.ToInheritedEntity();
//}

// This commented part could be used to have benefits of mongo entity typing
//protected virtual RequestToBeReviewedMongoBase ToMongoEntity(RequestToBeReviewedBase entity)
//{
//  return entity.ToInheritedMongo();
//}

protected virtual RequestToBeReviewed ToEntity(RequestToBeReviewedMongoV02 mongoEntity)
{
  return mongoEntity.ToEntity();
}

protected virtual RequestToBeReviewedMongoV02 ToMongoEntity(RequestToBeReviewed entity)
{
  return entity.ToMongoV02();
}

public virtual async Task<List<RequestToBeReviewed>> GetAllAsync(CancellationToken cancellationToken = default)
  => await _behavior.GetAllAsync(ToEntity, cancellationToken);

public virtual async Task<RequestToBeReviewed?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  => await _behavior.GetByIdAsync(id, ToEntity, cancellationToken);

public virtual async Task<List<RequestToBeReviewed>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
  => await _behavior.GetByIdsAsync(ids, ToEntity, cancellationToken);

public virtual async Task CreateAsync(RequestToBeReviewed newItem, CancellationToken cancellationToken = default)
  => await _behavior.CreateAsync(newItem, ToMongoEntity, cancellationToken);

public virtual async Task UpdateAsync(RequestToBeReviewed updatedItem, CancellationToken cancellationToken = default)
  => await _behavior.UpdateAsync(updatedItem, ToMongoEntity, cancellationToken);

public virtual async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
  => await _behavior.RemoveAsync(id, cancellationToken);

public virtual void SetUniqueIndex(params Expression<Func<RequestToBeReviewedMongoV02, object>>[] fields)
  => _behavior.SetUniqueIndex(fields);

public virtual void SetUniqueIndex(params string[] fields)
  => _behavior.SetUniqueIndex(fields);

public virtual void SetUniqueIndex(params IndexKeysDefinition<RequestToBeReviewedMongoV02>[] fields)
  => _behavior.SetUniqueIndex(fields);
}
