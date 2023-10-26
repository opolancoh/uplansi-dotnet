using Uplansi.Core.DTOs;

namespace Uplansi.Core.Contracts.Services;

public interface IUserService
{
    Task<ApplicationResult> GetAll(PaginationOptions pagination);
    // Task<ApplicationResult?> GetById(Guid id);
    Task<ApplicationResult> Add(UserAddOrUpdate item);
}