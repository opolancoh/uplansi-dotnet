using Uplansi.Core.DTOs;

namespace Uplansi.Core.Contracts.Services;

public interface IDataServiceBase<in TKey, in TAddOrUpdate, TResult>
{
    Task<TResult> GetAll(PaginationOptions pagination);
    Task<TResult?> GetById(TKey id);
    Task<TResult> Add(TAddOrUpdate item, Guid? authenticatedUserId);
    Task<TResult> Update(TKey id, TAddOrUpdate item, Guid authenticatedUserId);
    Task<TResult> Remove(TKey id);
}