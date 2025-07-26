// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using BioDiagnostics.Data.EFCore.SqlServer.DbContexts;
using BioDiagnostics.Data.EFCore.SqlServer.Entities;
using BioDiagnostics.Data.Entities;
using BioDiagnostics.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BioDiagnostics.Data.EFCore.SqlServer.Repositories;

public class RequestToBeReviewedRepository : IRequestToBeReviewedRepository
{
  // public const string CollectionName = "requestToBeReviewed";

  //internal protected TimeStampedMongoRepositoryBehavior<RequestToBeReviewed, RequestToBeReviewedMongo> Behavior { get => _behavior; }
  //private readonly TimeStampedMongoRepositoryBehavior<RequestToBeReviewed, RequestToBeReviewedMongo> _behavior;

  //public RequestToBeReviewedRepository(IMongoContext mongoContext)
  //{
  //  _behavior = new TimeStampedMongoRepositoryBehavior<RequestToBeReviewed, RequestToBeReviewedMongo>(mongoContext, CollectionName);
  //  _behavior.SetUniqueIndex(entity => entity.Id);
  //}

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

  private readonly BioDiagnosticsDbContext _dbContext;

  public RequestToBeReviewedRepository(BioDiagnosticsDbContext dbContext)
  {
    _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
  }

  internal protected DbSet<RequestToBeReviewedSql> GetEntities() => _dbContext.RequestToBeRevieweds;


  protected virtual RequestToBeReviewed ToEntity(RequestToBeReviewedSql mongoEntity)
  {
    return mongoEntity.ToEntity();
  }

  protected virtual RequestToBeReviewedSql ToMongoEntity(RequestToBeReviewed entity)
  {
    return entity.ToMongo();
  }

  public virtual async Task<List<RequestToBeReviewed>> GetAllAsync(CancellationToken cancellationToken = default)
    => (await GetEntities()
    .ToListAsync(cancellationToken))
    .Select(mongoEntity => ToEntity(mongoEntity))
    .ToList();

  public virtual /*async*/ Task<RequestToBeReviewed?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    //=> await _behavior.GetByIdAsync(id, ToEntity, cancellationToken);
    => throw new NotImplementedException();

  public virtual /*async*/ Task<List<RequestToBeReviewed>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    //=> await _behavior.GetByIdsAsync(ids, ToEntity, cancellationToken);
    => throw new NotImplementedException();

  public virtual /*async*/ Task CreateAsync(RequestToBeReviewed newItem, CancellationToken cancellationToken = default)
    //=> await _behavior.CreateAsync(newItem, ToMongoEntity, cancellationToken);
    => throw new NotImplementedException();

  public virtual /*async*/ Task UpdateAsync(RequestToBeReviewed updatedItem, CancellationToken cancellationToken = default)
    //=> await _behavior.UpdateAsync(updatedItem, ToMongoEntity, cancellationToken);
    => throw new NotImplementedException();

  public virtual /*async*/ Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    //=> await _behavior.RemoveAsync(id, cancellationToken);
    => throw new NotImplementedException();

  public virtual void SetUniqueIndex(params Expression<Func<RequestToBeReviewedSql, object>>[] fields)
    //=> _behavior.SetUniqueIndex(fields);
    => throw new NotImplementedException();

  public virtual void SetUniqueIndex(params string[] fields)
    //=> _behavior.SetUniqueIndex(fields);
    => throw new NotImplementedException();

  //public virtual void SetUniqueIndex(params IndexKeysDefinition<RequestToBeReviewedSql>[] fields)
  //  //=> _behavior.SetUniqueIndex(fields);
  //  => throw new NotImplementedException();
}
