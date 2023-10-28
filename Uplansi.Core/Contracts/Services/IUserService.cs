using Uplansi.Core.DTOs;
using Uplansi.Core.DTOs.User;

namespace Uplansi.Core.Contracts.Services;

public interface IUserService 
{
    Task<ApplicationResult> GetAll(PaginationOptions pagination);
    Task<ApplicationResult?> GetById(Guid id);
    Task<ApplicationResult> Add(UserAddModel item, Guid? authenticatedUserId);
    Task<ApplicationResult> Update(Guid id, UserUpdateModel item, Guid authenticatedUserId);
    Task<ApplicationResult> Remove(Guid id);
}