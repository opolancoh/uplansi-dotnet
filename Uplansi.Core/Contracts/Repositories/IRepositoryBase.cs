using Uplansi.Core.DTOs;

namespace Uplansi.Core.Contracts.Repositories;

public interface IRepositoryBase<in TKey, in TEntity, TListResult, TDetailResult>
{
    Task<TListResult> GetAll(PaginationOptions pagination);
    // Task<TDetailResult?> GetById(TKey id);
    Task<int> Add(TEntity item);
    // Task<int> Update(TEntity item);
    // Task<int> Remove(TKey id);
}