namespace Uplansi.Core.Contracts.Services;

public interface IDataServiceBase<in TKey, in TCreateOrUpdate, TResult>
{
    // Task<TResult> GetAll();
    // Task<TResult> GetById(TKey id);
    Task<TResult> Add(TCreateOrUpdate item, TKey authenticatedUserId);
    // Task<TResult> Update(TKey id, TCreateOrUpdate item, TKey authenticatedUserId);
    // Task<TResult> Remove(TKey id, TKey authenticatedUserId);
}